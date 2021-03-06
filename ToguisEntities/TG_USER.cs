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
    
    public partial class TG_USER
    {
        public TG_USER()
        {
            this.TG_TEMPORAL_RECOVERY = new HashSet<TG_TEMPORAL_RECOVERY>();
            this.TG_POI_USER_DATA = new HashSet<TG_POI_USER_DATA>();
            this.TG_COMMENTS = new HashSet<TG_COMMENTS>();
        }
    
        public string USR_ID { get; set; }
        public short GND_ID { get; set; }
        public int AUTH_ID { get; set; }
        public short ROL_ID { get; set; }
        public System.DateTime USR_LAST_LOGIN { get; set; }
        public string USR_PASWORD { get; set; }
        public string USR_PHONE_NUMBER { get; set; }
        public string USR_EMAIL { get; set; }
        public string USR_IMAGE { get; set; }
        public string USR_NAME { get; set; }
    
        public virtual TG_AUTHENTICATION_TYPE TG_AUTHENTICATION_TYPE { get; set; }
        public virtual TG_GENDER TG_GENDER { get; set; }
        public virtual TG_ROLE TG_ROLE { get; set; }
        public virtual ICollection<TG_TEMPORAL_RECOVERY> TG_TEMPORAL_RECOVERY { get; set; }
        public virtual ICollection<TG_POI_USER_DATA> TG_POI_USER_DATA { get; set; }
        public virtual ICollection<TG_COMMENTS> TG_COMMENTS { get; set; }
    }
}
