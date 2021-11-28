using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.RegrasdeNegocio;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PessoaModelsController : Controller
    {
        private readonly WebApplication1Context _context;
        

        public PessoaModelsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: PessoaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: PessoaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: PessoaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Email,DataNascimento,QuantidadeFilhos,Salario")] PessoaModel pessoaModel)
        {
            //if (ModelState.IsValid)
            //{
            pessoaModel.Situacao = "Ativo";

            if (!NegocioPessoa.DataNascimentoSuperior(pessoaModel.DataNascimento))
            {
                ModelState.AddModelError("", "Data de nascimento deve ser superior 01/01/1990");
                return View();
            }

            var pessoaEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);
            if (pessoaEmail.Count() > 0)
            {
                ModelState.AddModelError("", "E-mail ja cadastrado");
                return View();
            }

            _context.Add(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(pessoaModel);
        }

        // GET: PessoaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //NegocioPessoa teste = new NegocioPessoa();

            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }


            if (NegocioPessoa.EditarPessoaInativa(pessoaModel.Situacao))
            {
                ModelState.AddModelError("Regra de Negócio", "Pessoa INATIVO, não é possivel editar");
                return RedirectToAction(nameof(Index));
            }

            

            return View(pessoaModel);
        }

        // POST: PessoaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Email,DataNascimento,QuantidadeFilhos,Salario,Situacao")] PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{

            if (NegocioPessoa.QuantidadeFilhosPositiva(pessoaModel.QuantidadeFilhos))
            {
                ModelState.AddModelError("", "A quantidade de filhos tem que ser positiva");
                return View(pessoaModel);
            }

            if (!NegocioPessoa.DataNascimentoSuperior(pessoaModel.DataNascimento))
            {
                ModelState.AddModelError("", "Data de nascimento deve ser superior 01/01/1990");
                return View();
            }

            if (NegocioPessoa.VerificaSalarioMenor(pessoaModel.Salario))
            {
                ModelState.AddModelError("", "Salario não pode ser menor que 1.200");
                return View();
            }

            if (NegocioPessoa.VerificaSalarioMaior(pessoaModel.Salario))
            {
                ModelState.AddModelError("", "Salario não pode ser maior que 13.000");
                return View();
            }

            /*var pessoaEmail = _context.PessoaModel.Where(x => x.Email.Equals(pessoaModel.Email) && x.Codigo != pessoaModel.Codigo);
            if (pessoaEmail.Count() > 0)
            {
                ModelState.AddModelError("", "E-mail ja cadastrado");
                return View();
            }*/


            try
            {
                _context.Update(pessoaModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaModelExists(pessoaModel.Codigo))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            //return View(pessoaModel);
        }

        // GET: PessoaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            else
            {
                if (NegocioPessoa.ExclusaoPessoaAtiva(pessoaModel.Situacao))
                {
                    ModelState.AddModelError("Regra de Negócio", "Pessoa ATIVO, não é possivel excluir");
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(pessoaModel);
        }

        // POST: PessoaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            _context.PessoaModel.Remove(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AlterarStatus(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel.Situacao.Equals("Ativo"))
            {
                pessoaModel.Situacao = "Inativo";
            }
            else
            {
                pessoaModel.Situacao = "Ativo";
            }
            _context.Update(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }





        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Codigo == id);
        }
    }
}
