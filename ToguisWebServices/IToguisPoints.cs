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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IToguisPoints" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IToguisPoints
    {
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                UriTemplate = "get_points?login={login}&cityid={cityId}" +
                                         "&getmonument={getMonument}&getmuseum={getMuseum}&gethotel={getHotel}&getrestaurant={getRestaurant}" +
                                         "&getInterest={getinterest}&getbuilding={getBuilding}&gettransport={getTransport}&getevent={getEvent}&language={language}",
                ResponseFormat = WebMessageFormat.Json)]
        List<TG_INTEREST_POINT> GetPoints(String login,
                                            int cityId,
                                            bool getMonument,
                                            bool getMuseum,
                                            bool getHotel,
                                            bool getRestaurant,
                                            bool getInterest,
                                            bool getBuilding,
                                            bool getTransport,
                                            bool getEvent,
                                            int language);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_point?login={login}&poiid={poiId}&language={language}", ResponseFormat = WebMessageFormat.Json)]
        TG_INTEREST_POINT GetPoint(String login, int poiId, int language);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_comments?poiid={poiId}}", ResponseFormat = WebMessageFormat.Json)]
        List<TG_COMMENTS> GetComments(int poiId);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_comment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetComment(TG_COMMENTS comment);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_rating/{login}/{poiId}/{rating}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetRating(String login, int poiId, float rating);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_rating?login={login}&poiid={poiId}", ResponseFormat = WebMessageFormat.Json)]
        int GetRating(String login, int poiId);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_favorite/{login}/{poiId}/{value}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetFavorite(String login, int poiId, bool value);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_visited/{login}/{poiId}/{value}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetVisited(String login, int poiId, bool value);
    }
}
