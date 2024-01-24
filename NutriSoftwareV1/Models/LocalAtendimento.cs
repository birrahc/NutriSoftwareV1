using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("local_atendimento")]
    public class LocalAtendimento
    {
        [Key]
        [Column("id_local")]
        public int Id { get; set; }

        [Column("nome")]
        public string? Descricao { get; set; }

        public string? Endereco { get; set; }
        public string Telefone { get; set; }
    }
}
