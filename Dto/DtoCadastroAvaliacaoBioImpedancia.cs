using NutriSoftwareV1.Models;

namespace NutriSoftwareV1.Dto
{
    public class DtoCadastroAvaliacaoBioImpedancia
    {
        public long? PacienteId { get; set; }
        public DateTime Data { get; set; }
        public string? TituloCadastarEditar { get; set; }
        public AvalicaoBioImpedancia? CriarAtulizar { get; set; }
        public AvalicaoBioImpedancia? UltimaAvalicao { get; set; }
    }
}
