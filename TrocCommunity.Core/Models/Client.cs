﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Models
{
    public class Client : Utilisateur
    {
        public TableauDeBord TableauDeBord { get; set; }
    }
}
