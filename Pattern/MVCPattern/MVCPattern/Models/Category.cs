using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCPattern.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}