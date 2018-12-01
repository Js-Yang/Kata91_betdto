using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kata91_betdto
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Convert_To_BetDto()
        {
            var betConverter = new BetConverter();
            var bet = new Bet() { Id = 123, CreatedTime = new DateTime(2018, 12, 1), Stake = 100 };

            var actual = betConverter.Convert(bet, ConvertRule());

            Assert.AreEqual(100, actual.Amount);
            Assert.AreEqual(123, actual.BetId);
            Assert.AreEqual("20181201", actual.Date);
        }

        private static Func<Bet, BetDto> ConvertRule()
        {
            return (b) => new BetDto() { Amount = (int)b.Stake, BetId = b.Id, Date = b.CreatedTime.ToString("yyyyMMdd") };
        }
    }

    public class Bet
    {
        public int Id { get; set; }
        public decimal Stake { get; set; }
        public DateTime CreatedTime { get; set; }
    }

    public class BetConverter
    {
        public TResult Convert<TSource, TResult>(TSource bet, Func<TSource, TResult> convertRule)
        {
            return convertRule(bet);
        }
    }

    public class BetDto
    {
        public int BetId { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
    }
}
