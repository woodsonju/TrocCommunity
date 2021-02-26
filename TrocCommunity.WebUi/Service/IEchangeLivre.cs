using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.WebUi.Service
{

    public interface IEchangeLivre
    {
        int NbNotif();

        void EchangeBookProposition(EchangeLivre ech);

        void EchangeBookAccept(int Id);

        List<EchangeLivre> ShowAll(EtatEchange etEch,int idC);

        void CancelOperation(int Id);

        EchangeLivre enCoursEchange(int idLivre, int idClient);


    }
}
