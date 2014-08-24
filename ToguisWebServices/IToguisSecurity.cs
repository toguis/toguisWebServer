using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ToguisModel;

namespace ToguisWebServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IToguisSecurity" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IToguisSecurity
    {

        [OperationContract]
        [WebGet(UriTemplate = "getuser?login={login}", ResponseFormat = WebMessageFormat.Json)]
        TG_USER getUser(String login);
      
    }
}
