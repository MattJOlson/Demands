using FluentAssertions;
using NUnit.Framework;
using System;
using MattUtils.Demands;

namespace Demands.Tests.Unit
{
    [TestFixture]
    public class DemandImmediateTests
    {
        [Test]
        public void demanding_a_true_expression_succeeds_right_away()
        {
            Action vrfy = () => Demand.That(true).Because("it's true!");

            vrfy.ShouldNotThrow();
        }

        [Test]
        public void demanding_a_false_expression_throws_right_away()
        {
            Action vrfy = () => Demand.That(false).Because("false throws right away");

            vrfy.ShouldThrow<DemandUnmetException>()
                .WithMessage("false throws right away");
        }

        [Test]
        public void aggregating_demands_that_all_succeed_silently_succeeds()
        {
            Action vrfy = () => Demand.That(true).AndThat(true).Because("they're both true!");

            vrfy.ShouldNotThrow();
        }

        [Test]
        public void aggregating_demands_that_all_fail_throws_right_away()
        {
            Action vrfy = () => Demand.That(false).AndThat(false).Because("we're making sure this works");

            vrfy.ShouldThrow<DemandUnmetException>()
                .WithMessage("we're making sure this works");
        }

        [Test]
        public void aggregating_a_mix_of_true_and_false_demands_still_throws()
        {
            Action vrfy = () => Demand.That(true).AndThat(false).Because("something failed");

            vrfy.ShouldThrow<DemandUnmetException>()
                .WithMessage("something failed");
        }

        [Test]
        public void aggregating_false_before_true_still_works()
        {
            Action vrfy = () => Demand.That(false).AndThat(true).Because("this is still valid");

            vrfy.ShouldThrow<DemandUnmetException>()
                .WithMessage("this is still valid");
        }

        public class SpecialSnowflakeException : Exception
        {
            public SpecialSnowflakeException(string message) : base(message) { }
        }

        [Test]
        public void demanding_true_with_a_specific_exception_does_not_throw()
        {
            Action vrfy = () => Demand.That(true).OrThrow<SpecialSnowflakeException>("True is still true");

            vrfy.ShouldNotThrow();
        }

        [Test]
        public void demanding_false_with_a_specific_exception_throws_the_right_thing()
        {
            Action vrfy = () => Demand.That(false).OrThrow<SpecialSnowflakeException>("Not special enough!");

            vrfy.ShouldThrow<SpecialSnowflakeException>()
                .WithMessage("Not special enough!");
        }
    }
}