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
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_user?login={login}", ResponseFormat = WebMessageFormat.Json)]
        TG_USER GetUser(String login);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "create_user", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")] 
        int CreateUser(TG_USER user);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "update_user", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int UpdateUser(TG_USER user);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "recover_user/{email}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int RecoverUser(String email);


    }
}
