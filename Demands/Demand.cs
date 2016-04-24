using System;
using System.Collections.Generic;

namespace Demands
{
    public static class Demand
    {
        /// <summary>
        /// Returns a deferred demand that the predicate passed in must be true.
        /// </summary>
        /// <param name="predicate">Arbitrary boolean-valued function, evaluated every time the demand is tested.</param>
        public static DemandResult That(Func<bool> predicate) => new DemandResult(predicate);

        /// <summary>
        /// Returns an aggregator of demands that will resolve immediately when a <see cref="DemandAggregate.Because">Because</see> call is made.
        /// </summary>
        /// <param name="predicate">An arbitrary boolean-valued expression.</param>
        public static DemandAggregate That(bool predicate)
        {
            Func<bool> demand = () => predicate;
            return new DemandAggregate(demand);
        }

        /// <summary>
        /// Returns a wrapper on an enumerable that can receive demands.
        /// </summary>
        /// <typeparam name="T">Enumerable type param.</typeparam>
        /// <param name="seq">Enumerable to be demanded of.</param>
        /// <returns></returns>
        public static DemandEnumerable<T> ThatFor<T>(IEnumerable<T> seq)
        {
            return new DemandEnumerable<T>(seq);
        }
    }
}
