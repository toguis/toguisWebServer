using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ToguisModel;

namespace ToguisWebServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ToguisSecurity" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ToguisSecurity.svc o ToguisSecurity.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ToguisSecurity : IToguisSecurity
    {

        public TG_USER GetUser(string login)
        {
            TG_USER loResult = null; 
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.Configuration.ProxyCreationEnabled = false;
                    loResult = (from item in loContext.TG_USER
                                where item.USR_ID.ToLower().Equals(login)
                                select item).FirstOrDefault();
                }
                catch (Exception ex)
                {                    
                }

            }
            return loResult;
        }



        public int CreateUser(TG_USER user)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.TG_USER.Add(user);
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 1;
                }

            }
            return 0;            
        }




        public int test(Test tt)
        {
            return 0;
        }
    }



    [DataContract]
    public class Test
    {
        [DataMember]
        public string OrderID { get; set; }

        [DataMember]
        public string OrderDate { get; set; }

    }
}
