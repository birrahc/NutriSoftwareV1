using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriSoftwareV1.Models
{
    [Serializable]
    public class Anotacao
    {
        public long Id_Anotacao { get; set; }
        public DateTime Data_Anotacao { get; set; }
        public long Paciente_Anot { get; set; }
        public virtual Paciente Paciente { get; set; }
        public string Decricao { get; set; }
    }
}
