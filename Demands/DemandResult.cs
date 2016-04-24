using System;

namespace Demands
{
    public class DemandResult
    {
        private readonly Func<bool> _predicate;

        public DemandResult(Func<bool> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Evaluates a deferred demand.
        /// </summary>
        /// <param name="reason">A helpful message attached to the exception in case of failure.</param>
        public void Because(string reason)
        {
            if (!_predicate()) throw new DemandUnmetException(reason);
        }

        /// <summary>
        /// Evaluates a deferred demand, throwing a user-specified exception type on failure.
        /// </summary>
        /// <typeparam name="T">Type of exception to throw</typeparam>
        /// <param name="reason">A helpful message attached to the exception in case of failure.</param>
        public void OrThrow<T>(string reason) where T : Exception
        {
            T ex = (T) Activator.CreateInstance(typeof (T), reason);
            if (!_predicate()) throw ex;
        }
    }
}