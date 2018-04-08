﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSample.DA.Tables
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime ListingStartTime { get; set; }

        public DateTime ListingEndTime { get; set; }

        public DateTime SellingStartTime { get; set; }

        public DateTime SellingEndTime { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
