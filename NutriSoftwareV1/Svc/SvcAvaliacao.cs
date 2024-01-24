using NutriSoftwareV1.Models;

namespace NutriSoftwareV1.Svc
{
    public class SvcAvaliacao
    {
        public static float? CalculularDensidade(float? SomatoriaDc, EN_Sexo? pSexo, int? pIdade)
        {
            if (SomatoriaDc.HasValue && pSexo.HasValue && pIdade.HasValue)
            {
                switch (pSexo)
                {
                    case EN_Sexo.Masculino:
                        return ((float?)(1.11200000 -
                               (0.00043499 * (SomatoriaDc.Value)) +
                               (0.00000055 * (Math.Pow(SomatoriaDc.Value, 2))) -
                               (0.0002882 * (pIdade.Value))));

                    case EN_Sexo.Feminino:
                        return ((float?)(1.097 -
                              (0.00046971 * (SomatoriaDc.Value)) +
                              (0.00000056 * (Math.Pow(SomatoriaDc.Value, 2))) -
                              (0.00012828 * (pIdade.Value))));
                }

            }
            return null;
        }
        public static float CalcularPercentualGordura(float pDensidade)
        {
            return (float)(((4.95 / pDensidade) - 4.5) * 100);
        }
        public static float CalcularMassaMuscular(float pPercentualGordura, float pPeso)
        {
            return pPeso - ((pPercentualGordura / 100) * pPeso);
        }
        public static float CalcularGordura(float pPercentualGordura, float pPeso)
        {
            return (pPercentualGordura / 100) * pPeso;
        }
    }
}
