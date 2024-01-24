using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("local_almoco")]
    public class LocalAlmoco
    {
        [Key]
        [Column("id_local_alm")]
        public int Id { get; set; }

        [Column("local_almoco")]
        public string Descricao { get; set; }
    }
}
