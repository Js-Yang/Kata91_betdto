using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kata91_betdto
{
    [TestClass]
    public class UnitTest1
    {
        protected internal BetConverter BetConverter = new BetConverter();

        [TestMethod]
        public void Convert_To_BetDto()
        {
            var actual = BetConverter.Convert(GenerateBet(), ConvertRule());

            Assert.AreEqual(100, actual.Amount);
            Assert.AreEqual(123, actual.BetId);
            Assert.AreEqual("20181201", actual.Date);
        }

        [TestMethod]
        public void Convert_To_BetDto_With_Interface()
        {
            var actual = BetConverter.ConvertFromMap<Bet, BetDto>(GenerateBet(), new BetMapper());

            Assert.AreEqual(100, actual.Amount);
            Assert.AreEqual(123, actual.BetId);
            Assert.AreEqual("20181201", actual.Date);
        }

        [TestMethod]
        public void Convert_To_BetDto_Check_Field_Name()
        {
            var actual = BetConverter.ConvertFromFieldName(GenerateBet());
            
            Assert.AreEqual("JE", actual.Status);
        }

        private static Bet GenerateBet()
        {
            return new Bet() { Id = 123, CreatedTime = new DateTime(2018, 12, 1), Stake = 100,Status = "JE"};
        }

        private static Func<Bet, BetDto> ConvertRule()
        {
            return (b) => new BetDto() { Amount = (int)b.Stake, BetId = b.Id, Date = b.CreatedTime.ToString("yyyyMMdd") };
        }
    }

    public class BetMapper : IBetMapper<Bet, BetDto>
    {
        public BetDto Mapping(Bet bet)
        {
            return new BetDto() { Amount = (int)bet.Stake, BetId = bet.Id, Date = bet.CreatedTime.ToString("yyyyMMdd") };
        }
    }

    public class Bet
    {
        public int Id { get; set; }
        public decimal Stake { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Status { get; set; }
    }

    public class BetConverter
    {
        public TResult Convert<TSource, TResult>(TSource bet, Func<TSource, TResult> convertRule)
        {
            return convertRule(bet);
        }

        public TResult ConvertFromMap<TSource, TResult>(TSource bet, IBetMapper<TSource, TResult> mapper)
        {
            return mapper.Mapping(bet);
        }

        public BetDto ConvertFromFieldName(Bet generateBet)
        {
            throw new NotImplementedException();
        }
    }

    public interface IBetMapper<TSource, TResult>
    {
        TResult Mapping(TSource bet);
    }

    public class BetDto
    {
        public int BetId { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
    }
}
