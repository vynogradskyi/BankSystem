using System.ComponentModel.DataAnnotations;

namespace DataArt.Test.Models
{
    public class CardViewModel
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(4)]
        public string Pin { get; set; }
    }
}