using Application.UseCases;
using Domain.Commands;
using System.Threading;
using Xunit;

namespace Tests.Application
{
    public class ValidatePasswordHandleUnitTest
    {
        private ValidatePasswordHandler _handler = new ValidatePasswordHandler();

        [Fact]
        public void ShouldBeFalseEmptyPassword()
        {
            Assert.False(_handler.Handle(
                new ValidatePassword() {  Password = string.Empty },
                new CancellationTokenSource().Token
            ).Result);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("a1@")]
        [InlineData("a1@D")]
        [InlineData("a1@De")]
        [InlineData("a1@Def")]
        [InlineData("a1@Defg")]
        [InlineData("a1@Defgh")]
        public void ShoulBeRejectPasswordWithFewCharacters(string password)
        {
            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = password },
                new CancellationTokenSource().Token
            ).Result);
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("AAAbbbCc")]
        [InlineData("AbTp9!foo")]
        public void ShoulBeRejectPasswordWithRepeatedChars(string password)
        {
            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = password },
                new CancellationTokenSource().Token
            ).Result);
        }

        [Fact]
        public void ShoulBeValidatePasswordWithDifferentsCasesChars()
        {
            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9^fok" },
                new CancellationTokenSource().Token
            ).Result);
        }

        [Fact]
        public void ShoulBeRejectPasswordWithSpace()
        {
            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9 fok" },
                new CancellationTokenSource().Token
            ).Result);
        }

        [Fact]
        public void ShoulBeRejectPasswordNoSpecialChar()
        {
            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9.ok" },
                new CancellationTokenSource().Token
            ).Result);
        }

        [Fact]
        public void ShouldBeValidateAllPassword()
        {
            foreach (var value in "!@#$%^&*()-+")
            {
                Assert.True(_handler.Handle(
                    new ValidatePassword() { Password = $"AaTp9{value}fok" },
                    new CancellationTokenSource().Token
                ).Result);
            }
        }
    }
}
