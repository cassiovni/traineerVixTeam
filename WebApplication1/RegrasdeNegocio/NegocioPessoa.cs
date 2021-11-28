namespace WebApplication1.RegrasdeNegocio
{
    public static class NegocioPessoa
    {

        



        public static bool ExclusaoPessoaAtiva(string Situacao)
        {
            if (Situacao.Equals("Ativo"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool EditarPessoaInativa(string Situacao)
        {
            if (Situacao.Equals("Inativo"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DataNascimentoSuperior(DateTime DataNascimento)
        {
            if (DataNascimento >= DateTime.Parse("1/1/1990").Date)
            {
                return true;
            }
            else
            { 
                return false;
            }
        }

        public static bool QuantidadeFilhosPositiva(int QuantidadeFilhos)
        {
            if (QuantidadeFilhos >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static bool VerificaSalarioMenor(int Salario)
        {
            if (Salario >= 1200)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static bool VerificaSalarioMaior(int Salario)
        {
            if (Salario <= 13000)
            {
                return false;
            }
            else
            {
                return true;
            }

        }




    }








}
