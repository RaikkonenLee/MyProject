using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCPattern.Models
{
    public class ShopContext : DbContext
    {
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Product> Products { get; set; }
    }
}