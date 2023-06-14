using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch1Bank.Test
{
    public class AccountNumberTest
    {
        [Theory]
        [InlineData("0123456789", true)]
        [InlineData("012345678", false)]
        [InlineData("", false)]
        [InlineData("abc1234567", false)]
        [InlineData("1234567xyz", false)]
        [InlineData("12345.6789", false)]
        [InlineData("01234567898", false)]

        public void AccountNumberTest_ValidInput(string input, bool expected)
        {
            // Act
            var actual = Validate.Input(input, @"^\d{10}$");

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
