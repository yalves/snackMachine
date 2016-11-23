using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SnackMachine.Logic;
using FluentAssertions;
using static SnackMachine.Logic.Money;

namespace SnackMachine.Tests
{
    public class MachineSpecs
    {
        [Fact]
        public void Return_money_empties_money_in_transaction()
        {
            var machine = new Machine();
            machine.InsertMoney(Dollar);

            machine.ReturnMoney();

            machine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            var machine = new Machine();

            machine.InsertMoney(Cent);
            machine.InsertMoney(Dollar);

            machine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void Cannot_insert_more_than_one_coin_or_note_at_a_time()
        {
            var machine = new Machine();
            var twoDollar = Dollar + Dollar;

            Action action = () => machine.InsertMoney(twoDollar);

            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void Money_in_transaction_goes_to_money_inside_after_purchase()
        {
            var machine = new Machine();

            machine.InsertMoney(Dollar);
            machine.InsertMoney(Dollar);

            machine.BuySnack();

            machine.MoneyInTransaction.Should().Be(None);
            machine.MoneyInside.Amount.Should().Be(2m);
        }

        [Fact]
        public void New_machine_doesnt_contains_any_money()
        {
            var machine = new Machine();

            machine.MoneyInTransaction.Should().Be(None);
            machine.MoneyInside.Amount.Should().Be(0m);
        }
    }
}
