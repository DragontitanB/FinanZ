using System;
using System.Collections.Generic;

namespace MyFi.Models
{
    public partial class Gasto
    {
        public int IdGastos { get; set; }
        public int? IdUsuario { get; set; }
        public float Monto { get; set; }
        public int? IdTipo { get; set; }
        public DateTime? Fecha { get; set; }
        public bool Recurrencia { get; set; }
        public bool? Notificacion { get; set; }
        public bool? Automatico { get; set; }

        public virtual TipoGasto? IdTipoNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
