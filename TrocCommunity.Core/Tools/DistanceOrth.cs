using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Tools
{
    public class DistanceOrth
    {

        public static double DegToRad(double deg)
        {
            return deg * Math.PI / 180.0;
        }
        public static double RadToDeg(double rad)
        {
            return rad * 180.0 / Math.PI;
        }


        public static double DistanceOrthodromique(double lon1, double lat1, double lon2, double lat2)
        {


            double dotProd = Math.Sin(DegToRad(lat1)) * Math.Sin(DegToRad(lat2)) + Math.Cos(DegToRad(lat1)) * Math.Cos(DegToRad(lat2)) * Math.Cos(DegToRad(lon2) - DegToRad(lon1));

            if (dotProd == 0)
            {
                return 0;
            }
            // conversion en degrée et en kilomètres
            double dist = 1.852*60 * RadToDeg( Math.Acos(dotProd));


            return dist;
        }

    }
}
