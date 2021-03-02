using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Models
{
    public enum EtatEchange
    {
        Propose,
        Accepte
    }

    public class EchangeLivre : BaseEntity
    {
        [Required]
        public EtatEchange EtatEchange{ get; set; }


        public int? LivreEchangeId{ get; set; }

        [ForeignKey("LivreEchangeId")]
        public Livre LivreEchange { get; set; }

        [Display(Name = "Date")]
        public DateTime DateEchangeCreation { get; set; }

        [Display(Name = "Date")]
        public DateTime DateEchangeEffectue { get; set; }

        public int? ClientPropId { get; set; }

        [ForeignKey("ClientPropId")]
        public Client ClientProp { get; set; }


    }
}
