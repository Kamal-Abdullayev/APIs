using ProjectX.Entities.Models;
using System.Collections.Generic;

namespace ProjectX.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Product> Products { get; set; }
    }
}
