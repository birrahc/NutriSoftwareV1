using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("atividades_fisicas")]
    public class AtividadesFisicas
    {
        [Key]
        [Column("Id_Atividade")]
        public int Id { get; set; }

        [Column("atividade")]
        public string? Descricao { get; set; }
    }
}
