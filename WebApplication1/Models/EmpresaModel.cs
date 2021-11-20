namespace WebApplication1.Models
{
    public class EmpresaModel
    {
        private int codigo { get; set; }
        private string nome { get; set; }
        private string nomeFantasia { get; set; }
        private string CNPJ { get; set; }

        public EmpresaModel()
        {
            this.codigo = codigo;
            this.nome = nome;
            this.nomeFantasia = nomeFantasia;
            this.CNPJ = CNPJ;
        }
    }
}