using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;
using System.Data.Entity;

namespace TrocCommunity.DataAccess.SQL.DAO
{
    public class SQLRepositoryLivreEchange:SQLRepository<EchangeLivre>
    {
        public SQLRepositoryLivreEchange(MyContext DataContext) : base(DataContext)
        {

        }

        public int NbNotif()
        {



            return 0;
        }

        public void EchangeBookProposition(EchangeLivre ech)
        {
            // Creation

            Livre livre = dataContext.Livres.Find(ech.LivreEchangeId);
            Client ClientProp = dataContext.Clients.Find(ech.ClientPropId);

            if (ClientProp.SoldeCompte >= livre.PointDuLivre)
            {
                dataContext.EchangeLivres.Add(ech);
                ClientProp.SoldeCompte -= livre.PointDuLivre;
                dataContext.Entry(ClientProp).State = EntityState.Modified;
            }

            
            
        }
        public EchangeLivre enCoursEchange(int idLivre, int idClient)
        {

            EchangeLivre echLivre =  dataContext.EchangeLivres.Include(ech => ech.LivreEchange).Include(ech => ech.LivreEchange.Client).Include(ech => ech.ClientProp).SingleOrDefault(ech => ech.LivreEchange.Id == idLivre &&
                                                                                                                    (ech.ClientProp.Id == idClient || ech.LivreEchange.Client.Id == idClient));

            return echLivre;
        }

        public void EchangeBookAccept(int idEch)
        {
            EchangeLivre ech = dataContext.EchangeLivres.Include(ec => ec.ClientProp).Include(ec => ec.LivreEchange).Include(l => l.LivreEchange.Client).SingleOrDefault(ec => ec.Id== idEch);
            Livre livre = ech.LivreEchange;

            Client clientProp = ech.ClientProp;

                // poursuite de l'échange


                //Echange des points

            livre.Client.SoldeCompte += livre.PointDuLivre;

            dataContext.Entry(livre.Client).State = EntityState.Modified;
                
            // Changement Etat du Livre

            ech.EtatEchange = EtatEchange.Accepte;
            ech.DateEchangeEffectue = DateTime.Now;
            dataContext.Entry(ech).State = EntityState.Modified;

            

            

            //Changement Livre disponibilité

            livre.Disponible = false;
            dataContext.Entry(livre).State = EntityState.Modified;

            // on efface l'ensemble des EchangeLivre avec ce livre car il passe en indisponible

            List < EchangeLivre > listRemove= dataContext.EchangeLivres.Include(echL => echL.ClientProp).Include(echL => echL.LivreEchange).Where(echL => echL.LivreEchangeId == livre.Id && echL.ClientProp.Id != clientProp.Id).ToList();

            foreach (var echLivre in listRemove)
            {
                echLivre.ClientProp.SoldeCompte += echLivre.LivreEchange.PointDuLivre;
                dataContext.Entry(echLivre.ClientProp).State = EntityState.Modified;

                dataContext.EchangeLivres.Remove(echLivre);
                
            }
            

        }
        
        public List<EchangeLivre> ShowAll(EtatEchange etEch,int idEch)
        {


            return dataContext.EchangeLivres.Include(ech => ech.ClientProp).Include(ec => ec.LivreEchange).Include(l => l.LivreEchange.Client).Where(echLivre => echLivre.EtatEchange == etEch &&
                                                                                  (echLivre.ClientProp.Id == idEch || echLivre.LivreEchange.Client.Id == idEch)).ToList();
        }

        public void CancelOperation(int idEch)
        {
            // Le Client reprends ses points tout de même
            EchangeLivre echLivre = dataContext.EchangeLivres.Include(ech => ech.ClientProp).Include(ech => ech.LivreEchange).SingleOrDefault(ech => ech.Id == idEch);
            echLivre.ClientProp.SoldeCompte += echLivre.LivreEchange.PointDuLivre;
            dataContext.Entry(echLivre.ClientProp).State = EntityState.Modified;


            // Suppression de ech
            Delete(idEch);

        }

    }
}
