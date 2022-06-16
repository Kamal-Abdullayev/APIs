using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ProjectX.Data.Entities;

namespace ProjectX.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Image { get; set; }

    }
}
