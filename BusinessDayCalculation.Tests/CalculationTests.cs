using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCalculation.Tests
{
    public class CalculationTests
    {
        [Theory]
        [MemberData(nameof(DateFormats))]
        public void Test_Different_Date_Formats_Are_Parsed(string dateFormat)
        {
            //arrange
            var sut = new BusinessDayCalculation();
            //act
            var date = sut.ParseDate(dateFormat);
            //assert
            Assert.IsType<DateTime>(date);
        }

        [Fact]
        public void Test_Business_Day_Calculation()
        {
            //arrange

            var sut = new BusinessDayCalculation();

            //act
            var businessDays = sut.GetBusinessDays("11/11/2017", "11/11/2018");

            //assert
            Assert.Equal(313, businessDays);
        }

        [Fact]
        public void Test_Business_Day_Calculation_Throws_Exception_When_Start_Date_is_Greater()
        {
            //arrange

            var sut = new BusinessDayCalculation();

            //assert
            Assert.Throws<Exception>(() =>
            {
                sut.GetBusinessDays("11/11/2018", "11/11/2017");
            });
        }

        public static IEnumerable<object[]> DateFormats => new List<object[]>
        {
            new object[]
            {
                "11/23/2017"
            },
            new object[]
            {
                "23.11.2017"
            },
            new object[]
            {
                "23/11/2017"
            }
        };
    }
}
