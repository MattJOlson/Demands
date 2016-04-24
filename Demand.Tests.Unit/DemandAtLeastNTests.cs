using System;
using System.Linq;
using FluentAssertions;
using MattUtils.Demands;
using NUnit.Framework;

namespace Demands.Tests.Unit
{
    [TestFixture]
    public class DemandAtLeastNTests
    {
        [TestCase(0, 0, false)]
        [TestCase(1, 0, true)]
        [TestCase(2, 1, true)]
        [TestCase(2, 2, false)]
        [TestCase(2, 3, false)]
        public void demanding_at_least_k_of_a_list_with_n_matches_fails_when_n_is_less_than_k(int k, int n, bool shouldThrow)
        {
            var matches = Enumerable.Repeat(0, n);
            var others = Enumerable.Range(1, 10);
            var list = matches.Concat(others).OrderBy(e => Guid.NewGuid()).ToList();
            Action vrfy = () => Demand.ThatFor(list).AtLeast(k).Match(x => x == 0).Because("I said so");

            if (shouldThrow)
                vrfy.ShouldThrow<DemandUnmetException>();
            else {
                vrfy.ShouldNotThrow();
            }
        }
    }
}