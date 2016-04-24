using System;

namespace MattUtils.Demands
{
    public class DemandUnmetException : Exception
    {
        public DemandUnmetException() : base("Constraint not satisfied") { }
        public DemandUnmetException(string msg) : base(msg) { }
    }
}