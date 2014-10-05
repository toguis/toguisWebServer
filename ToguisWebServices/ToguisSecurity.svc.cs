using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ToguisController.Security;
using ToguisModel;

namespace ToguisWebServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ToguisSecurity" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ToguisSecurity.svc o ToguisSecurity.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ToguisSecurity : IToguisSecurity
    {

        public TG_USER GetUser(string login)
        {
            SecurityController loController = new SecurityController();
            return loController.GetUser(login);
        }

        public int CreateUser(TG_USER user)
        {
            SecurityController loController = new SecurityController();
            return loController.CreateUser(user);           
        }

        public int UpdateUser(TG_USER user)
        {
            SecurityController loController = new SecurityController();
            return loController.UpdateUser(user);    
        }

        public int RecoverUser(String email)
        {
            SecurityController loController = new SecurityController();
            return loController.RecoverUser(email);     
        }
    }
}
