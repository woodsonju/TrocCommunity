using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.Core.Tools
{
    public class UpdateDatabaseAdmin : BaseEntity
    {
        [Required]
        public TypeUtilisateur typeUtilisateur { get; set; }

        public UpdateDatabaseAdmin()
        {

        }

        public UpdateDatabaseAdmin(TypeUtilisateur typeUtilisateur)
        {
            this.typeUtilisateur = typeUtilisateur;
        }


    }
}
