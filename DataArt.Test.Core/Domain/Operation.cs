using System;
using DataArt.Test.Core.Abstract;

namespace DataArt.Test.Core.Domain
{
    public class Operation : IHaveId
    {
        public int Id { get; set; }
        public OperationType OperationType { get; set; }
        public DateTime PerformTime { get; set; }
        public string AdditionInformation { get; set; }
    }
}