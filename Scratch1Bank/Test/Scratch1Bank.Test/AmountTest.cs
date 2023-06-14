using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch1Bank.Test
{
    public class AmountTest
    {
        [Theory]
        [InlineData("1000",true)]
        [InlineData("889.90", true)]
        [InlineData("0", false)]
        [InlineData("-1000", false)]
        [InlineData("-889.90", false)]
        [InlineData("abc", false)]
        [InlineData("90.io", false)]
        [InlineData("", false)]


        public void AmountTest_validInput(string input, bool expected )
        {
            // Act 
            var actual = Validate.Amount(input);

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
