﻿using System.ComponentModel.DataAnnotations;

namespace TrocCommunity.Core.Models
{
    public class Livre
    {
        [Required]
        public string Author { get; set; }

        [Required]
        public string Editor { get; set; }

        [Required]
        public string Edition { get; set; }

        [Required]
        public long  BarCode { get; set; }

        [Required]
        public int Volume { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public Category Category { get; set; }


    }
}