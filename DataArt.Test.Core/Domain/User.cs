using System.Collections.Generic;
using DataArt.Test.Core.Abstract;

namespace DataArt.Test.Core.Domain
{
    public class User : IHaveId
    {
        public User()
        {
            Operations = new List<Operation>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public double Balance { get; set; }
        public bool Blocked { get; set; }
        public List<Operation> Operations { get; set; }
    }
}