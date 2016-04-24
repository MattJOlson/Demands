using System;

namespace Demands
{
    public class DemandAggregate
    {
        private readonly Func<bool> _predicate;

        public DemandAggregate(Func<bool> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Adds another predicate to a chain of demands.
        /// </summary>
        /// <param name="nextPredicate">An arbitrary boolean-valued expression</param>
        public DemandAggregate AndThat(bool nextPredicate)
        {
            return new DemandAggregate(() => _predicate() & nextPredicate);
        }

        /// <summary>
        /// Evaluates all predicates in a chain of demands and throws an exception if one returns false.
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