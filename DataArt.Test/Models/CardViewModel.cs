using System.ComponentModel.DataAnnotations;

namespace DataArt.Test.Models
{
    public class CardViewModel
    {
        private string _cardNumber;
        [Required]
        public string CardNumber {
            get { return _cardNumber; } 
            set { _cardNumber = value.Replace("-",""); } }
        [Required]
        [StringLength(4)]
        public string Pin { get; set; }
    }
}