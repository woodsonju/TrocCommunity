using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.ViewModels
{
    public class AdresseViewModel
    {
        public string FullName { get; set; }
        public string NomDeVoie { get; set; }


        public int NumVoie { get; set; }


        public string CodePostale { get; set; }

        public string Ville { get; set; }

        public string Pays { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string PlaceId { get; set; }

    }
}
