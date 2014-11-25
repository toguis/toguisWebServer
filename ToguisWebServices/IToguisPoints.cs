using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ToguisModel;

namespace ToguisWebServices
{
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
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                UriTemplate = "get_pointsDistance?login={login}&cityid={cityId}" +
                                                 "&getmonument={getMonument}&getmuseum={getMuseum}&gethotel={getHotel}&getrestaurant={getRestaurant}" +
                                                 "&getinterest={getinterest}&getbuilding={getBuilding}&gettransport={getTransport}&getevent={getEvent}&language={language}" +
                                                 "&userlatitude={userLatitude}&userlongitude={userLongitude}&maxdistance={maxDistance}",
                ResponseFormat = WebMessageFormat.Json)]
        List<TG_INTEREST_POINT> GetPointsWithDistance(String login,
                                            int cityId,
                                            bool getMonument,
                                            bool getMuseum,
                                            bool getHotel,
                                            bool getRestaurant,
                                            bool getInterest,
                                            bool getBuilding,
                                            bool getTransport,
                                            bool getEvent,
                                            int language,
                                            double userLatitude,
                                            double userLongitude,
                                            double maxDistance);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare,
                UriTemplate = "search_points?login={login}&cityid={cityId}" +
                                                 "&getmonument={getMonument}&getmuseum={getMuseum}&gethotel={getHotel}&getrestaurant={getRestaurant}" +
                                                 "&getinterest={getinterest}&getbuilding={getBuilding}&gettransport={getTransport}&getevent={getEvent}&language={language}" +
                                                 "&userlatitude={userLatitude}&userlongitude={userLongitude}&maxdistance={maxDistance}&search={search}",
                ResponseFormat = WebMessageFormat.Json)]
        List<TG_INTEREST_POINT> SearchPoints(String login,
                                            int cityId,
                                            bool getMonument,
                                            bool getMuseum,
                                            bool getHotel,
                                            bool getRestaurant,
                                            bool getInterest,
                                            bool getBuilding,
                                            bool getTransport,
                                            bool getEvent,
                                            int language,
                                            double userLatitude,
                                            double userLongitude,
                                            double maxDistance,
                                            string search);
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_point?login={login}&poiid={poiId}&language={language}", ResponseFormat = WebMessageFormat.Json)]
        TG_INTEREST_POINT GetPoint(String login, string poiId, string language);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_comments?poiid={poiId}", ResponseFormat = WebMessageFormat.Json)]
        List<TG_COMMENTS> GetComments(string poiId);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_comment?commentid={commentId}", ResponseFormat = WebMessageFormat.Json)]
        TG_COMMENTS GetComment(int commentId);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_comment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetComment(TG_COMMENTS comment);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_rating/{login}/{poiId}/{rating}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetRating(String login, string poiId, string rating);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "get_rating?login={login}&poiid={poiId}", ResponseFormat = WebMessageFormat.Json)]
        float GetRating(String login, string poiId);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_favorite/{login}/{poiId}/{value}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetFavorite(String login, string poiId, string value);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "set_visited/{login}/{poiId}/{value}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        int SetVisited(String login, string poiId, string value);
    }
}