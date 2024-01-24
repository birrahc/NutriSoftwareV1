using NutriSoftwareV1.Svc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("avaliacao_antropometrica")]
    public class AvaliacaoFisica
    {
        [Key]
        [Column("id_avalicao")]
        public long? Id { get; set; }

        
        [Column("paciente")]
        public long PacienteId { get; set; }
        public virtual Paciente? Paciente { get; set; }

        public int Consulta { get; set; }

        [Required(ErrorMessage ="'Data avaliação' Preenchimento obrigatório.\n")]

        [Column("Data_Avalicao")]
        public DateTime DataAvaliacao { get; set; }

        [Required(ErrorMessage ="'Peso' Preenchimento obrigatório.\n")]
        public float Peso { get; set; }

        [Required(ErrorMessage ="'C.Cintura' Preenchimento obrigatório.\n")]
        public float? C_Cintura { get; set; }

        [Required(ErrorMessage = "'C.Abdominal' Preenchimento obrigatório.\n")]
        public float? C_Abdominal { get; set; }

        [Required(ErrorMessage = "'C.Qudril' Preenchimento obrigatório.\n")]
        public float? C_Quadril { get; set; }

        [Required(ErrorMessage = "'C.Peito' Preenchimento obrigatório.\n")]
        public float? C_Peito { get; set; }

        [Required(ErrorMessage = "'C.Braço D' Preenchimento obrigatório.\n")]
        public float? C_Braco_D { get; set; }

        [Required(ErrorMessage = "'C.Braço E' Preenchimento obrigatório.\n")]
        public float? C_Braco_E { get; set; }

        [Required(ErrorMessage = "'C.Coxa D' Preenchimento obrigatório.\n")]
        public float? C_Coxa_D { get; set; }

        [Required(ErrorMessage = "'C.Coxa E' Preenchimento obrigatório.\n")]
        public float? C_Coxa_E { get; set; }

        public float? Panturrilha_D { get; set; }
        public float? Panturrilha_E { get; set; }

        [Required(ErrorMessage = "'Dc.Triceps' Preenchimento obrigatório.\n")]
        public float? Dc_Triceps { get; set; }


        [Required(ErrorMessage = "'Dc.Escapular' Preenchimento obrigatório.\n")]
        public float? Dc_Escapular { get; set; }


        [Required(ErrorMessage = "'Dc.Supra Iliaca' Preenchimento obrigatório.\n")]
        public float? Dc_Supra_Iliaca { get; set; }

        [Required(ErrorMessage = "'Dc.Abdominal' Preenchimento obrigatório.\n")]
        public float ?Dc_Abdominal { get; set; }

        [Required(ErrorMessage = "'Dc.Axilar' Preenchimento obrigatório.\n")]
        public float? Dc_Axilar { get; set; }

        [Required(ErrorMessage = "'Dc.Peitoral' Preenchimento obrigatório.\n")]
        public float? Dc_Peitoral { get; set; }

        [Required(ErrorMessage = "'Dc.Coxa' Preenchimento obrigatório.\n")]
        public float? Dc_coxa { get; set; }


        [NotMapped]
        private float? SomatoriaDc
        {
            get
            {
                if (this.Dc_Triceps.HasValue &&
                      this.Dc_Escapular.HasValue &&
                      this.Dc_Supra_Iliaca.HasValue &&
                      this.Dc_Abdominal.HasValue &&
                      this.Dc_Axilar.HasValue &&
                      this.Dc_Peitoral.HasValue &&
                      this.Dc_coxa.HasValue)
                {
                    return(
                         this.Dc_Triceps +
                         this.Dc_Escapular +
                         this.Dc_Supra_Iliaca +
                         this.Dc_Abdominal +
                         this.Dc_Axilar +
                         this.Dc_Peitoral +
                         this.Dc_coxa
                         );

                }
                return null;
            }
        }


        [NotMapped]
        private float? Densidade
        {
            get
            {
                if (this.SomatoriaDc.HasValue && (this.Paciente != null && this.Paciente.Sexo.HasValue && this.Paciente?.Idade > 0))
                    return SvcAvaliacao.CalculularDensidade(this.SomatoriaDc.Value, this.Paciente.Sexo.Value, this.Paciente.Idade).Value;
                return null;
            }
        }

        [NotMapped]
        public float? PercentualGordura
        {
            get
            {
                if (this.Densidade.HasValue)
                    return (float)SvcAvaliacao.CalcularPercentualGordura(this.Densidade.Value);
                return null;
            }
        }
        [NotMapped]
        public double? MassaMuscular
        {
            get
            {
                if (this.PercentualGordura.HasValue && this.Peso>0)
                    return SvcAvaliacao.CalcularMassaMuscular(this.PercentualGordura.Value, this.Peso);
                return null;
            }
        }
        [NotMapped]
        public float? Gordura
        {
            get
            {
                if (this.PercentualGordura.HasValue && this.Peso>0)
                    return (float)SvcAvaliacao.CalcularGordura(this.PercentualGordura.Value, this.Peso);
                return null;
            }
        }

        public bool IsValid()
        {
            return (this.Gordura.HasValue && this.MassaMuscular.HasValue && PercentualGordura.HasValue);
        }
    }
}
