namespace DataArt.Test.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public bool Blocked { get; set; }
    }
}