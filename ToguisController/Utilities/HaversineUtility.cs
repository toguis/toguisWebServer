using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToguisController.Utilities
{
    public class HaversineUtility
    {
        public const float EarthRadius = 6378.0F;

        public static double GetDistance(double pLatitudeA, double pLongitudeA, double pLatitudeB, double pLongitudeB)
        {
            double ldVarLatitude = ToRadians(pLatitudeB - pLatitudeA);
            double ldVarLongitude = ToRadians(pLongitudeB - pLongitudeA);
            double ldLatitudeA = ToRadians(pLatitudeA);
            double ldLatitudeB = ToRadians(pLatitudeB);

            double ldResultA = Math.Pow(Math.Sin(ldVarLongitude / 2d), 2) + (Math.Cos(ldLatitudeA) * Math.Cos(ldLatitudeB) * Math.Pow(Math.Sin(ldVarLongitude/2f),2));
            double ldResultC = 2 * Math.Atan2(Math.Sqrt(ldResultA), Math.Sqrt(1 - ldResultA));
            double ldResultD = EarthRadius * ldResultC;
            return ldResultD;
        }

        public static double ToRadians(double pValue)
        {
            return Convert.ToSingle(Math.PI / 180) * pValue;
        }
    }
}
