using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yachay.Entities;

namespace Yachay.DAL.Services
{
    public class FacebookApi
    {
        public List<NodoDTO> FindFacebookPlaces(double lat, double lng, string texto)
        {
            var client = new FacebookClient("1972166699717669|FgXgnxrz6jjq7yLQRNwwIsmor28");
            dynamic places = client.Get("search?type=place&q=" + texto + "&center=" + lat + "," + lng + "&distance=1500&fields=id,name,location") as IDictionary<string, object>;

            List<NodoDTO> lista = new List<NodoDTO>();
            foreach (var item in places["data"])
            {
                NodoDTO obj = new NodoDTO();
                obj.Nombre = item["name"];
                obj.Direccion_Latitud = Convert.ToString((item["location"])["latitude"]);
                obj.Direccion_Longitud = Convert.ToString((item["location"])["longitude"]);
                lista.Add(obj);
            }
            return lista;
        }

        public string IsAuth(string token)
        {
            try
            {
                var client = new FacebookClient(token);
                dynamic result = client.Get("me/email");
                var email = (string)result.email;
                return "true";
            }
            catch (FacebookOAuthException ex)
            {
                return ex.Message;
            }
        }

        private string GetExtendedAccessToken(string ShortLivedToken)
        {
            FacebookClient client = new FacebookClient();
            string extendedToken = "";
            string appId = "1972166699717669";
            string appSecret = "2f05e7d477d221712a413d8a2e5fe1f7";

            try
            {
                dynamic result = client.Get("/oauth/access_token", new
                {
                    grant_type = "fb_exchange_token",
                    client_id = appId,
                    client_secret = appSecret,
                    fb_exchange_token = ShortLivedToken
                });
                extendedToken = result.access_token;
            }
            catch
            {
                extendedToken = ShortLivedToken;
            }
            return extendedToken;
        }
    }
}
