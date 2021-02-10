using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrocCommunity.WebUi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Littérature",
            url: "categorie/Littérature",
            defaults: new { controller = "Categories", action = "Lit" }
        );

            routes.MapRoute(
            name: "BienEtre",
            url: "categorie/Bien-être-santé-et-vie-pratique",
            defaults: new { controller = "Categories", action = "Bie" }
        );

            routes.MapRoute(
            name: "Jeunesse",
            url: "categorie/Jeunesse",
            defaults: new { controller = "Categories", action = "Jeu" }
        );

            routes.MapRoute(
           name: "BandeDessinées",
           url: "categorie/Bande-dessinées-et-Mangas",
           defaults: new { controller = "Categories", action = "Ban" }
       );

            routes.MapRoute(
           name: "Art",
           url: "categorie/Art-et-Sciences-humaines",
           defaults: new { controller = "Categories", action = "Art" }
       );

            routes.MapRoute(
           name: "Scolaire",
           url: "categorie/Scolaire-et-Pédagogie",
           defaults: new { controller = "Categories", action = "Sco" }
       );

            routes.MapRoute(
           name: "Loisirs",
           url: "categorie/Loisirs-créatifs-nature-et-voyages",
           defaults: new { controller = "Categories", action = "Loi" }
       );

            routes.MapRoute(
           name: "Sciences",
           url: "categorie/Sciences-Techniques-et-Médecine",
           defaults: new { controller = "Categories", action = "Sci" }
       );

            routes.MapRoute(
           name: "Entreprise",
           url: "categorie/Entreprise-Droit-Economie",
           defaults: new { controller = "Categories", action = "Ent" }
       );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
