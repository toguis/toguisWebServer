using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
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

        public ToguisModel.TG_INTEREST_POINT GetPoint(string login, int poiId, int language)
        {
            PointOfInterestController loController = new PointOfInterestController();
            return loController.GetPoint(login, poiId, language);
        }


        public List<ToguisModel.TG_COMMENTS> GetComments(int poiId)
        {
            throw new NotImplementedException();
        }

        public int SetComment(ToguisModel.TG_COMMENTS comment)
        {
            throw new NotImplementedException();
        }

        public int SetRating(string login, int poiId, float rating)
        {
            throw new NotImplementedException();
        }

        public int GetRating(string login, int poiId)
        {
            throw new NotImplementedException();
        }

        public int SetFavorite(string login, int poiId, bool value)
        {
            throw new NotImplementedException();
        }

        public int SetVisited(string login, int poiId, bool value)
        {
            throw new NotImplementedException();
        }
    }
}
