using System;
using System.Collections.Generic;

namespace MyFi.Models
{
    public partial class Texto
    {
        public Texto()
        {
            TipoGastos = new HashSet<TipoGasto>();
            TipoIngresos = new HashSet<TipoIngreso>();
        }

        public int IdTexto { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<TipoGasto> TipoGastos { get; set; }
        public virtual ICollection<TipoIngreso> TipoIngresos { get; set; }
    }
}
