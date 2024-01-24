using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace NutriSoftwareV1.Models
{
    [Table("bioimp")]
    public class AvalicaoBioImpedancia
    {
        [Key]
        [Column("id_bio")]
        public long? Id { get; set; }

        [Column("data_bio")]
        public DateTime Data { get; set; }
        
        [ForeignKey("Paciente")]
        [Column("paciente_bio")]
        public long PacienteId { get; set; }
        public virtual Paciente? Paciente { get; set; }


        [Column("peso_bio")]
        public float Peso { get; set; }


        [Column("imc_bio")]
        public float IMC { get; set; }

        [Column("perc_gord_corp")]
        public float PercentualGorduraCorporal { get; set; }

        [Column("perc_musc_esq")]
        public float PercentualMusculoEsqueletico { get; set; }

        [Column("met_basal")]
        public float MetaBolismoBasal { get; set; }

        [Column("idade_corpoaral")]
        public int IdadeCorporal { get; set; }

        [Column("gord_viceral")]
        public float GorduraViceral { get; set; }


    }
}
