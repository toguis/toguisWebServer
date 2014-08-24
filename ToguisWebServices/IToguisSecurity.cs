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
    [ServiceContract]
    public interface IToguisSecurity
    {

        [OperationContract]
        [WebGet(UriTemplate = "getuser?login={login}", ResponseFormat = WebMessageFormat.Json)]
        TG_USER getUser(String login);
      
    }
}
