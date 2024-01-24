using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("doencas")]
    public class Doenca
    {
        [Key]
        [Column("Id_Doenca")]
        public int Id { get; set; }

        [Column("doenca")]
        public string? Descricao { get; set; }
    }
}
