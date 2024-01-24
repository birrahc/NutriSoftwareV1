using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("observacao")]
    public class Observacao
    {
        [Key]
        [Column("id_observacao")]
        public int Id { get; set; }

        [Column("data_obs")]
        public DateTime DataObservacao { get; set; }

        [Column("paciente_obs")]
        public long PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }

        public string? Obs { get; set; }
    }
}
