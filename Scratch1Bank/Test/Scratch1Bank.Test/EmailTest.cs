using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch1Bank.Test
{
    public class EmailTest
    {
        [Theory]
        [InlineData("JJ@gmail.com",true)]
        [InlineData("JJ@yahoo.com", true)]
        [InlineData("JJ@gmail.123", true)]
        [InlineData("JJ.com", false)]
        [InlineData("JJ@gmail", false)]

        public void EmailTest_ValidInput(string input, bool expected)
        {
            var actual = Validate.Input(input, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Assert.Equal(expected, actual);
        }
    }
}
