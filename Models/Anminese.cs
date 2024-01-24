using NutriSoftwareV1.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("anaminese")]
    public class Anminese
    {
        [Key]
        [Column("id_anaminese")]
        public int Id { get; set; }

        //[ForeignKey("Paciente")]
        [Column("paciente")]
        public long PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }


        public string? Objetivo { get; set; }

        [ForeignKey("Doenca")]
        [Column("diagnostico_medico")]
        public int? DiagnosticoMedicoId { get; set; }//fk
        public virtual Doenca? DiagnosticoMedico { get; set; }
        [Column("obs_diag_medico")]
        public string? ObsDiagnosticoMedico { get; set; }

        public EN_Afirmacao? Exames { get; set; }//fk ou enum
        [Column("obs_exames")]
        public string? ObsExames { get; set; }

        public EN_Afirmacao? Medicamentos { get; set; }//fk ou enum
        [Column("obs_medicamentos")]
        public string? ObsMedicamentos { get; set; }


        public EN_Afirmacao? Suplementos { get; set; }

        [Column("obs_suplementos")]
        public string? ObsSuplementos { get; set; }

        [ForeignKey("Doenca")]
        [Column("historico_familiar")]
        public int?  HistoricoFamiliarId { get; set; }
        public virtual Doenca? HistoricoFamiliar { get; set; }
        [Column("obs_hist_familiar")]
        public string? ObsHistoricoFamiliar { get; set; }

        [Column("sinais_sintomas")]
        public EN_Afirmacao? SinaisESintomas { get; set; }

        [Column("obs_sinais_sintomas")]
        public string? Obs_Sinais_Sintomas { get; set; }

        [Column("habito_intestinal")]
        public EN_HabitosIntestinais? HabitosIntestinais { get; set; }

        [Column("obs_hab_intestinal")]
        public string? Obs_Hab_Intestinal { get; set; }


        public EN_Afirmacao? Tabagismo { get; set; }

        [Column("obs_tabagismo")]
        public string? Obs_Tabagismo { get; set; }


        public EN_Afirmacao? Etilismo { get; set; }

        [Column("obs_etilismo")]
        public string? Obs_Etilismo { get; set; }

        [Column("atividades_fisicas")]
        public int? AtividadesFisciaId { get; set; }
        public virtual AtividadesFisicas? AtividadesFiscia { get; set; }

        [Column("obs_atividades")]
        public string? ObsAtividadesFisicas { get; set; }
    }
}
