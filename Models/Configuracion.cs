using System;
using System.Collections.Generic;

namespace MyFi.Models
{
    public partial class Configuracion
    {
        public int IdConfiguracion { get; set; }
        public bool? Tema { get; set; }
        public bool? Notificaciones { get; set; }

        public virtual Usuario IdConfiguracionNavigation { get; set; } = null!;
    }
}
