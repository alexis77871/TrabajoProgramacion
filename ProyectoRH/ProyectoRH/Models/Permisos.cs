//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoRH.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permisos
    {
        public int ID { get; set; }
        public string Empleado { get; set; }
        public int CodigoEmpleado { get; set; }
        public Nullable<System.DateTime> FechaInicial { get; set; }
        public Nullable<System.DateTime> FechaFinal { get; set; }
        public string Comentarios { get; set; }
    
        public virtual Empleados Empleados { get; set; }
    }
}
