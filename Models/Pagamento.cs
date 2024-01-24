using NutriSoftwareV1.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    [Table("pagamentos")]
    public class Pagamento
    {
        [Key]
        [Column("id_pagamento")]
        public long Id { get; set; }

        [Column("data_cons")]
        public DateTime DataConsulta { get; set; }

        [Column("referencia")]
        public long? AvaliacaoId { get; set; }
        public AvaliacaoFisica? Avaliacao { get; set; }

        public EN_TipoDeConsulta? Tipo { get; set; }

        public EN_Plano? Plano { get; set; }

        public decimal? Valor { get; set; }

        [Column("qtd_vezes")]
        public int? QtdVezesParcelado { get; set; }

        [Column("valor_parcelas")]
        public decimal? ValorParcelas { get; set; }

        public EN_SituacaoPagamento? Situacao { get; set; }


        [Column("l_atendimento")]
        public int LocalAtendimentoId { get; set; }
        public virtual LocalAtendimento LocalAtendimento { get; set; }

        public string? Observacao { get; set; }
    }
}
