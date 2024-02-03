using Fiorello.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.ProductVM
{
    public class ProductReturnVM
    {
        public int Id { get; set; }
   
        public string Name { get; set; }

        public double Price { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

    }
}
