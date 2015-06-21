using System.ComponentModel.DataAnnotations;

namespace DataArt.Test.Models
{
    public class GetMoneyViewModel
    {
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter proper amount.")]
        public string Amount { get; set; } 
    }
}