using System;

namespace Demands
{
    public class DemandUnmetException : Exception
    {
        public DemandUnmetException() : base("Constraint not satisfied") { }
        public DemandUnmetException(string msg) : base(msg) { }
    }
}