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
    
    public partial class TG_LANGUAGE
    {
        public TG_LANGUAGE()
        {
            this.TG_POI_DESCRIPTION = new HashSet<TG_POI_DESCRIPTION>();
        }
    
        public int LNG_ID { get; set; }
        public string LNG_NAME { get; set; }
        public string LNG_ISOCODE { get; set; }
    
        public virtual ICollection<TG_POI_DESCRIPTION> TG_POI_DESCRIPTION { get; set; }
    }
}
