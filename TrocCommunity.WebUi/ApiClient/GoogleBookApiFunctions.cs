using Google.Apis.Books.v1.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace TrocCommunity.WebUi.ApiClient
{
    public class GoogleBookApiFunctions
    {
        public async Task<string>GetBookByISBN(string isbn)
        {
            string reponse = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage httpResponse = await client.GetAsync("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn + "&projection=full" + "&key=" + ConfigurationManager.AppSettings["GooglePlaceAPIKey"]);
     
                 reponse = await httpResponse.Content.ReadAsStringAsync();


/*                string response3 = await client.GetStringAsync(String.Format("https://maps.googleapis.com/maps/api/place/details/json?place_id=" + item.Item4 + "&fields=address_component&key=" + ConfigurationManager.AppSettings["GooglePlaceAPIKey"]));

                GET https://www.googleapis.com/books/v1/volumes/zyTCAlFPjgYC?projection=lite&key=yourAPIKey*/


            }



            /*string response = await client.GetStringAsync(String.Format("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn + "&key=" + ConfigurationManager.AppSettings["GooglePlaceAPIKey"]));*/
            /*}*/
            /* La clé se trouve dans le fichier Web.Config, pour l'utiliser : ConfigurationManager.AppSettings["GooglePlaceAPIKey"] (retourne la clé en string)
            
            Pour utiliser la fonction EssaiBookAPI : 
             await GooglePlaceApifunctions.EssaiBookAPI(ConfigurationManager.AppSettings["GooglePlaceAPIKey"]);

            Pour utiliser une API:
            - faire un compte google API
            - ajouter dans bibliothèque les API voulu
            - généré ue clé API 
            - voir la doc de l'API pour voir comment l'utiliser

            */
            return reponse;
        }


    }
}