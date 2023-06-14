using Scratch1Bank;
using System.ComponentModel.DataAnnotations;

namespace Scratch1Bank.Test
{
    public class ChoiceTest
    {
        [Fact]
        public void Choice_ValidInput()
        {
            // Arrange

            //Act
            var expect = true;
            var actual = Validate.Choice("1");

            // Assert
            Assert.Equal(expect, actual);




        }

        [Theory]
        [InlineData(true,"1")]
        [InlineData(false,"")]
        [InlineData(false,"a")]
        [InlineData(false,"0")]
        [InlineData(false,"1.9")]
        public void Choice_ValidInput_TestCases(bool expected, string input)
        {
            // Arrange


            //Act
            //var expect = true;
            var actual = Validate.Choice(input);

            // Assert
            Assert.Equal(expected, actual);




        }
    }

}