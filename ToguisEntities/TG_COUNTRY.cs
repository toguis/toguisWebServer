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
    
    public partial class TG_COUNTRY
    {
        public TG_COUNTRY()
        {
            this.TG_STATE = new HashSet<TG_STATE>();
        }
    
        public int CTR_ID { get; set; }
        public string CTR_NAME { get; set; }
        public string CTR_ISO_CODE { get; set; }
    
        public virtual ICollection<TG_STATE> TG_STATE { get; set; }
    }
}