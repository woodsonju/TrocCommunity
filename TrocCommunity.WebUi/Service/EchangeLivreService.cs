using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;

namespace TrocCommunity.WebUi.Service
{
    public class EchangeLivreService : IEchangeLivre
    {

        private SQLRepositoryLivreEchange repo;
        public EchangeLivreService(SQLRepositoryLivreEchange repo)
        {
            this.repo = repo;
        }

        public void CancelOperation(int Id)
        {
            repo.CancelOperation(Id);
            repo.SaveChanges();
        }

        public void EchangeBookAccept(int Id)
        {
            repo.EchangeBookAccept(Id);
            repo.SaveChanges();
        }

        public void EchangeBookProposition(EchangeLivre ech)
        {

            repo.EchangeBookProposition(ech);
            repo.SaveChanges();
        }

        public EchangeLivre enCoursEchange(int idLivre, int idClient)
        {
            EchangeLivre ech = repo.enCoursEchange(idLivre, idClient) ;

            return ech;

        }

        public int NbNotif()
        {
            return repo.NbNotif();
        }

        public List<EchangeLivre> ShowAll(EtatEchange etEch, int id)
        {
            return repo.ShowAll(etEch, id);
        }
    }
}