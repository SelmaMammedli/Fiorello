using Fiorello.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.BasketVM
{
    public class BasketProductVM
    {
        public int Id { get; set; }
     
        public string Name { get; set; }
       
        public double Price { get; set; }
        
       
        public string CategoryName { get; set; }
        
        public string MainImageUrl { get; set; }
        public int BasketCount { get; set; }
    }
}
