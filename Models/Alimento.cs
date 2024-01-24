using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("alimentos")]
    public class Alimento
    {
        [Key]
        [Column("id_alimento")]
        public int Id { get; set; }

        [Column("alimento")]
        public string? Descricao { get; set; }
    }
}
