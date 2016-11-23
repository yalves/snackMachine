using FluentAssertions;
using SnackMachine.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SnackMachine.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money sum = money1 + money2;

            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void Two_money_instances_equal_if_contain_the_same_amount()
        {
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void Two_money_instances_not_equal_if_contain_different_amount()
        {
            Money dollar = new Money(0, 0, 0, 1, 0, 0);
            Money hundredCents = new Money(100, 0, 0, 0, 0, 0);

            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value(
            int OneCentCount,
            int TenCentCount,
            int QuarterCount,
            int OneDollarCount,
            int FiveDollarCount,
            int TwentyDollarCount)
        {
            Action action = () => new Money(
                    OneCentCount,
                    TenCentCount,
                    QuarterCount,
                    OneDollarCount,
                    FiveDollarCount,
                    TwentyDollarCount);

            action.ShouldThrow<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        [InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void Amount_is_calculated_correctly(
            int OneCentCount,
            int TenCentCount,
            int QuarterCount,
            int OneDollarCount,
            int FiveDollarCount,
            int TwentyDollarCount,
            int expectedAmount)
        {
            Money money = new Money(
                    OneCentCount,
                    TenCentCount,
                    QuarterCount,
                    OneDollarCount,
                    FiveDollarCount,
                    TwentyDollarCount);

            money.Amount.ShouldBeEquivalentTo(expectedAmount);
        }

        [Fact]
        public void Subtraction_of_two_moneys_produces_correct_result()
        {
            Money money1 = new Money(10, 10, 10, 10, 10, 10);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            Money sum = money1 - money2;

            sum.OneCentCount.Should().Be(9);
            sum.TenCentCount.Should().Be(8);
            sum.QuarterCount.Should().Be(7);
            sum.OneDollarCount.Should().Be(6);
            sum.FiveDollarCount.Should().Be(5);
            sum.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void Cannot_subtract_more_than_exists()
        {
            Money money1 = new Money(0, 1, 0, 0, 0, 0);
            Money money2 = new Money(1, 0, 0, 0, 0, 0);

            Action action = () =>
            {
                Money money = money1 - money2;
            };

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
