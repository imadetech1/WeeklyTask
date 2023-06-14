using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scratch1Bank.Test
{
    public class PasswordTest
    {
        [Theory]
        [InlineData("Password123&",true)]
        [InlineData("123Password$", true)]
        [InlineData("",false)]
        [InlineData("password",false)]
        [InlineData ("Password",false)]
        [InlineData("112345678",false)]
        [InlineData("Pass12!",false)]
        [InlineData("password2345&", false)]
        public void PasswordTest_ValidInput(string input, bool expected) 
        {
            // Act 
            var actual = Validate.Input(input, @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");

            //Assert
            Assert.Equal(expected, actual);

        }
    }
}
