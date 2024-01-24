using NutriSoftwareV1.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("consumos")]
    public class ConsumoAlimentar
    {
        [Key]
        [Column("id_consumo")]
        public int Id { get; set; }

        [Column("paciente_id")]
        public long PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }

        public EN_Afirmacao? Agua { get; set; }

        [Column("obs_agua")]
        public string? ObsAgua { get; set; }

        public EN_Afirmacao? Sucos { get; set; }

        [Column("obs_sucos")]
        public string? ObsSucos { get; set; }

        [Column("durante_refeicoes")]
        public EN_Afirmacao? DuranteAsRefeicoes { get; set; }

        [Column("obs_refeicoes")]
        public string? ObsDuranteAsRefeicoes { get; set; }

        public EN_Afirmacao Acucares { get; set; }

        [Column("obs_acucares")]
        public string ? ObsAcucares { get; set; }

        public EN_Afirmacao Sodio { get; set; }

        [Column("obs_sodio")]
        public string? ObsSodio { get; set; }

        public EN_Afirmacao Refrigerantes { get; set; }

        [Column("obs_refri")]
        public string? ObsRefrigerantes { get; set; }

        public EN_Afirmacao Cereais { get; set; }

        [Column("obs_cereais")]
        public string? ObsCereais { get; set; }

        public EN_Afirmacao Frutas { get; set; }

        [Column("obs_frutas")]
        public string? ObsFrutas { get; set; }

        public EN_Afirmacao Verduras { get; set; }

        [Column("obs_verduras")]
        public string? ObsVerduras { get; set; }

        [Column("local_almoco")]
        public int? LocalAlmocoId { get; set; }
        public virtual LocalAlmoco? LocalAlmoco { get; set; }

        public string? Preferencias { get; set; }
        public string? Aversoes { get; set; }
    }
}
