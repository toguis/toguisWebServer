using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using ToguisController.Points;

namespace ToguisWebServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ToguisPoints" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ToguisPoints.svc o ToguisPoints.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ToguisPoints : IToguisPoints
    {
        public List<ToguisModel.TG_INTEREST_POINT> GetPoints(string login, int cityId, bool getMonument, bool getMuseum, bool getHotel, bool getRestaurant, bool getInterest, bool getBuilding, bool getTransport, bool getEvent, int language)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.GetPoints(login, cityId, getMonument, getMuseum, getHotel, getRestaurant, getInterest, getBuilding, getTransport, getEvent, language);
        }

        public List<ToguisModel.TG_INTEREST_POINT> GetPointsWithDistance(string login, int cityId, bool getMonument, bool getMuseum, bool getHotel, bool getRestaurant, bool getInterest, bool getBuilding, bool getTransport, bool getEvent, int language, double userLatitude, double userLongitude, double maxDistance)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.GetPointsWithDistance(login, cityId, getMonument, getMuseum, getHotel, getRestaurant, getInterest, getBuilding, getTransport, getEvent, language, userLatitude, userLongitude, maxDistance);
        }

        public ToguisModel.TG_INTEREST_POINT GetPoint(string login, string poiId, string language)
        {
            string lsAppPath = HttpContext.Current.Request.ApplicationPath;
            string lsPhysicalPath = HttpContext.Current.Request.MapPath(lsAppPath);
            PointOfInterestController loController = new PointOfInterestController();
            return loController.GetPoint(login, poiId, language, lsPhysicalPath);
        }

        public List<ToguisModel.TG_COMMENTS> GetComments(string poiId)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.GetComments(poiId);
        }

        public ToguisModel.TG_COMMENTS GetComment(int commentId)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.GetComment(commentId);
        }

        public int SetComment(ToguisModel.TG_COMMENTS comment)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.SetComment(comment);
        }

        public int SetRating(string login, string poiId, string rating)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.SetRating(login, poiId, rating);
        }

        public float GetRating(string login, string poiId)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.GetRating(login, poiId);
        }

        public int SetFavorite(string login, string poiId, string value)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return SetFavorite(login, poiId, value);
        }

        public int SetVisited(string login, string poiId, string value)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return SetVisited(login, poiId, value);
        }





 
    }
}
