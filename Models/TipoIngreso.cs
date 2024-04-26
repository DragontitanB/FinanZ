using System;
using System.Collections.Generic;

namespace MyFi.Models
{
    public partial class TipoIngreso
    {
        public int IdIngreso { get; set; }
        public int Tipo { get; set; }

        public virtual Ingreso IdIngresoNavigation { get; set; } = null!;
        public virtual Texto TipoNavigation { get; set; } = null!;
    }
}
