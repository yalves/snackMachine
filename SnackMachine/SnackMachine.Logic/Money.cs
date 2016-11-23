using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public sealed class Money : ValueObject<Money>
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCents = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
            OneCentCount * 0.01m +
            TenCentCount * 0.10m +
            QuarterCount * 0.25m +
            OneDollarCount +
            FiveDollarCount * 5 +
            TwentyDollarCount * 20;

        public Money(
            int OneCentCount,
            int TenCentCount,
            int QuarterCount,
            int OneDollarCount,
            int FiveDollarCount,
            int TwentyDollarCount)
        {
            if (OneCentCount < 0)
                throw new InvalidOperationException();
            if (TenCentCount < 0)
                throw new InvalidOperationException();
            if (QuarterCount < 0)
                throw new InvalidOperationException();
            if (OneDollarCount < 0)
                throw new InvalidOperationException();
            if (FiveDollarCount < 0)
                throw new InvalidOperationException();
            if (TwentyDollarCount < 0)
                throw new InvalidOperationException();

            this.OneCentCount = OneCentCount;
            this.TenCentCount = TenCentCount;
            this.QuarterCount = QuarterCount;
            this.OneDollarCount = OneDollarCount;
            this.FiveDollarCount = FiveDollarCount;
            this.TwentyDollarCount = TwentyDollarCount;
        }

        public static Money operator +(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount);
        }

        public static Money operator -(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount);
        }
        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount
               && TenCentCount == other.TenCentCount
               && QuarterCount == other.QuarterCount
               && OneDollarCount == other.OneDollarCount
               && FiveDollarCount == other.FiveDollarCount
               && TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashcode = OneCentCount;
                hashcode = (hashcode * 397) ^ TenCentCount;
                hashcode = (hashcode * 397) ^ QuarterCount;
                hashcode = (hashcode * 397) ^ OneDollarCount;
                hashcode = (hashcode * 397) ^ FiveDollarCount;
                hashcode = (hashcode * 397) ^ TwentyDollarCount;
                return hashcode;
            }
        }
    }
}
