//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToguisModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TG_TEMPORAL_RECOVERY
    {
        public int REC_ID { get; set; }
        public string USR_ID { get; set; }
        public string REC_GUID { get; set; }
        public System.DateTime REC_MAX_VALID_DATE { get; set; }
    
        public virtual TG_USER TG_USER { get; set; }
    }
}
