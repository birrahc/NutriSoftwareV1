using NutriSoftwareV1.Models;

namespace NutriSoftwareV1.Dto
{
    public class DtoCadastroAvaliacaoFisica
    {
        public int NumeroAvaliacao { get; set; }
        public long? PacienteId { get; set; }
        public DateTime Data { get; set; }
        public string? TituloCadastarEditar { get; set; }
        public AvaliacaoFisica? CriarAtulizar { get; set; }
        public AvaliacaoFisica? UltimaAvalicao { get; set; }
    }
}
