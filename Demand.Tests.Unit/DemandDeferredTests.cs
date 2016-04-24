using System;
using FluentAssertions;
using MattUtils.Demands;
using NUnit.Framework;

namespace Demands.Tests.Unit
{
    [TestFixture]
    public class DemandDeferredTests
    {
        [Test]
        public void demanding_a_true_predicate_silently_succeeds()
        {
            var trueIsSuccess = Demand.That(() => true);

            Action vrfy = () => trueIsSuccess.Because("We don't throw on success");
            vrfy.ShouldNotThrow();
        }

        [Test]
        public void demanding_a_false_predicate_throws()
        {
            var falseIsFailure = Demand.That(() => false);

            Action vrfy = () => falseIsFailure.Because("We throw on failure");

            vrfy.ShouldThrow<DemandUnmetException>()
                .WithMessage("We throw on failure");
        }

        public class SpecialSnowflakeException : Exception
        {
            public SpecialSnowflakeException(string message) : base(message) { }
        }

        [Test]
        public void demanding_a_special_exception_for_a_true_predicate_silently_succeeds()
        {
            var trueIsSuccess = Demand.That(() => true);

            Action vrfy = () => trueIsSuccess
                .OrThrow<SpecialSnowflakeException>("Throwing on success _would_ be special");

            vrfy.ShouldNotThrow();
        }

        [Test]
        public void demanding_a_special_exception_for_a_false_predicate_throws_the_right_thing()
        {
            var falseIsFailure = Demand.That(() => false);

            Action vrfy = () => falseIsFailure
                .OrThrow<SpecialSnowflakeException>("Snowflake specialness below threshold");

            vrfy.ShouldThrow<SpecialSnowflakeException>()
                .WithMessage("Snowflake specialness below threshold");
        }
    }
}