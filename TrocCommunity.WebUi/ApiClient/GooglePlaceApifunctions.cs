using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.WebUi.ApiClient
{
    public class GooglePlaceApifunctions
    {

        public async static Task<String> EssaiBookAPI(string isbn)
        {
            using (var client = new HttpClient())
            {

                string response = await client.GetStringAsync(String.Format("https://www.googleapis.com/books/v1/volumes?q=isbn:"+isbn+"&key=" + ConfigurationManager.AppSettings["GooglePlaceAPIKey"]));
             }
            /* La clé se trouve dans le fichier Web.Config, pour l'utiliser : ConfigurationManager.AppSettings["GooglePlaceAPIKey"] (retourne la clé en string)
            
            Pour utiliser la fonction EssaiBookAPI : 
             await GooglePlaceApifunctions.EssaiBookAPI(ConfigurationManager.AppSettings["GooglePlaceAPIKey"]);

            Pour utiliser une API:
            - faire un compte google API
            - ajouter dans bibliothèque les API voulu
            - généré ue clé API 
            - voir la doc de l'API pour voir comment l'utiliser

            */
            return "aa";
        }


        public async static Task<List<String>> AutoCompleteSearch( string search)
        {
            string researchedPlace = search.Replace(' ', '+');
            List<string> listPlace = null;
            // TODO gestion des tokens si temps
            using (var client = new HttpClient())
            {
                // key : ConfigurationManager.AppSettings["GooglePlaceAPIKey"]
                // Permet de compléter la suite de l'adresse saisie
                string response = await client.GetStringAsync(String.Format("https://maps.googleapis.com/maps/api/place/autocomplete/json?input="+ researchedPlace + "&key=" + ConfigurationManager.AppSettings["GooglePlaceAPIKey"]));


                JObject objectContainer = JObject.Parse(response);

                var AllDescription = from desc in objectContainer["predictions"] select (string)desc["description"];

                listPlace = new List<string>();
                foreach (var item in AllDescription)
                {
                    listPlace.Add(item);
                }
            }
            return listPlace;
        }

        public async static Task<Adresse> AdresseInformation( string search)
        {
            string researchedPlace = search.Replace(' ', '+');
            Adresse add = null;
            using (var client = new HttpClient())
            {


                string response2 = await client.GetStringAsync(String.Format("https://maps.googleapis.com/maps/api/place/findplacefromtext/json?input=" + researchedPlace + "&inputtype=textquery&fields=formatted_address,geometry,place_id&key=" + ConfigurationManager.AppSettings["GooglePlaceAPIKey"]));


                JObject objectContainer2 = JObject.Parse(response2);

                var adressLonLat = from place in objectContainer2["candidates"] select ((string)place["formatted_address"], Convert.ToDouble(place["geometry"]["location"]["lat"]), Convert.ToDouble(place["geometry"]["location"]["lng"]), (string)place["place_id"]);

                var item = adressLonLat.First();



                string response3 = await client.GetStringAsync(String.Format("https://maps.googleapis.com/maps/api/place/details/json?place_id=" + item.Item4 + "&fields=address_component&key=" + ConfigurationManager.AppSettings["GooglePlaceAPIKey"]));

                JObject objectContainer3 = JObject.Parse(response3);

                var requestPoste = from posteCode in objectContainer3["result"]["address_components"] where posteCode["types"].ToList().Contains("postal_code") select posteCode["long_name"];
                string poste = requestPoste.SingleOrDefault().ToString();

                var requestNum = from posteCode in objectContainer3["result"]["address_components"] where posteCode["types"].ToList().Contains("street_number") select posteCode["long_name"];
                int num = Convert.ToInt32(requestNum.First().ToString());

                var requestCity = from posteCode in objectContainer3["result"]["address_components"] where posteCode["types"].ToList().Contains("locality") select posteCode["long_name"];
                string city = requestCity.First().ToString();

                var requestCountry = from posteCode in objectContainer3["result"]["address_components"] where posteCode["types"].ToList().Contains("country") select posteCode["long_name"];
                string country = requestCountry.First().ToString();

                var requestRoute = from posteCode in objectContainer3["result"]["address_components"] where posteCode["types"].ToList().Contains("route") select posteCode["long_name"];
                string route = requestRoute.First().ToString();

                add = new Adresse()
                {
                    FullName = item.Item1,
                    PlaceId = item.Item4,
                    Longitude = item.Item3,
                    Latitude = item.Item2,
                    CodePostale = poste,
                    NomDeVoie = route,
                    NumVoie = num,
                    Pays = country,
                    Ville = city

                };
            }
            return add;
        }


    }
}
