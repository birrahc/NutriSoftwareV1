using NutriSoftwareV1.Svc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("pacientes")]
    public class Paciente
    {
        [Key]
        [Column("Id_Paciente")]
        public long? Id { get; set; }

        [Required(ErrorMessage ="'Nome' preenchimento obrigatório.\n")]
        public string? Nome { get; set; }

        [NotMapped]
        public int Idade { get => this.DataNascimento.HasValue ? SvcPessoa.CalcularIdade(this.DataNascimento.Value) : 0; }

        [Required (ErrorMessage ="'Sexo' Preenchento obrigatório.\n")]
        public EN_Sexo? Sexo { get; set; }
        public float? Altura { get; set; }

        [Required(ErrorMessage ="'Data de Nascimento' Preenchimento obrigatório.\n")]
        [Column("Data_Nascimento")]
        public DateTime? DataNascimento { get; set; }
        public string? Cpf { get; set; }
        public string? Profissao { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public virtual List<Observacao>? Anotacoes { get; set; }
        public virtual List<AvaliacaoFisica>? Avaliacoes {  get; set; }
        public virtual List<AvalicaoBioImpedancia>? AvaliacoesBioImpedancia {  get; set; }


    }
}
