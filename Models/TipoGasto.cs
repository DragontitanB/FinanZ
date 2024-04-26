using System;
using System.Collections.Generic;

namespace MyFi.Models
{
    public partial class TipoGasto
    {
        public TipoGasto()
        {
            Gastos = new HashSet<Gasto>();
        }

        public int IdTipo { get; set; }
        public int? Tipo { get; set; }

        public virtual Texto? TipoNavigation { get; set; }
        public virtual ICollection<Gasto> Gastos { get; set; }
    }
}
