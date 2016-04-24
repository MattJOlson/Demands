using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Demands.Tests.Unit
{
    [TestFixture]
    public class DemandAtMostNTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(4)]
        public void demanding_at_most_n_always_succeeds_on_empty_lists(int n)
        {
            var empty = new int[] {}.ToList();
            Action vrfy = () => Demand.ThatFor(empty).AtMost(n).Match(x => x != 0).Because("Trivial success is still success");

            vrfy.ShouldNotThrow();
        }

        [TestCase(1)]
        [TestCase(4)]
        public void demanding_at_most_n_succeeds_on_lists_containing_n_elements(int n)
        {
            var full = Enumerable.Repeat(0, n);
            Action vrfy = () => Demand.ThatFor(full).AtMost(n).Match(x => x != 0).Because("This is a slightly less trivial success");

            vrfy.ShouldNotThrow();
        }

        [TestCase(2, 1, false)]
        [TestCase(2, 2, false)]
        [TestCase(2, 3, true)]
        public void demanding_at_most_k_succeeds_on_lists_with_n_matching_elements_iff_n_is_not_larger_than_k(int k, int n, bool shouldThrow)
        {
            var matches = Enumerable.Repeat(0, n);
            var others = Enumerable.Range(1, 10);
            var list = matches.Concat(others).OrderBy(e => Guid.NewGuid()).ToList();
            Action vrfy = () => Demand.ThatFor(list).AtMost(k).Match(x => x == 0).Because("I said so");

            if (shouldThrow)
                vrfy.ShouldThrow<DemandUnmetException>();
            else {
                vrfy.ShouldNotThrow();
            }
        }

        [Test]
        public void demanding_at_most_one_of_a_failing_list_with_a_custom_msgfunc_throws_with_the_right_msg()
        {
            var several = new[] {0, 1, 0, 2, 0, 3, 0, 4, 0, 5}.ToList();
            Func<IEnumerable<int>, string> msgfunc = (matches) => {
                var firstThree = matches.Take(3).ToList();
                return $"A bunch failed, starting with {firstThree[0]}, {firstThree[1]}, and {firstThree[2]}";
            };

            Action vrfy = () => Demand.ThatFor(several).AtMost(1).Match(x => x != 0).Because(msgfunc);

            vrfy.ShouldThrow<DemandUnmetException>()
                .WithMessage("A bunch failed, starting with 1, 2, and 3");
        }
    }
}