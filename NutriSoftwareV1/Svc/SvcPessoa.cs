namespace NutriSoftwareV1.Svc
{
    public class SvcPessoa
    {
        public static int CalcularIdade(DateTime dataNascimento)
        {
            DateTime dataAtual = DateTime.Now;
            int idade = dataAtual.Year - dataNascimento.Year;

            // Ajusta a idade se o aniversário ainda não ocorreu neste ano
            if (dataNascimento > dataAtual.AddYears(-idade))
            {
                idade--;
            }

            return idade;
        }
    }
}
