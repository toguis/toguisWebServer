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
        [WebGet(UriTemplate = "get_user?login={login}", ResponseFormat = WebMessageFormat.Json)]
        TG_USER GetUser(String login);

        [OperationContract]
        [WebInvoke(UriTemplate = "create_user", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")] 
        int CreateUser(TG_USER user);

    }
}
