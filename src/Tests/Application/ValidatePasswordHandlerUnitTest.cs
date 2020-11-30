using Application;
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

        [Fact]
        public void ShoulBeRejectPasswordWithFewCharacters()
        {
            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "ab" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "a1@" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "a1@D" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "a1@De" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "a1@Def" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "a1@Defg" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "a1@Defgh" },
                new CancellationTokenSource().Token
            ).Result);
        }

        [Fact]
        public void ShoulBeRejectPasswordWithRepeatedChars()
        {
            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "aa" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "AAAbbbCc" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.False(_handler.Handle(
                new ValidatePassword() { Password = "AbTp9!foo" },
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
            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9!fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9@fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9#fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9$fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9%fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9^fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9&fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9*fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9(fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9)fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9+fok" },
                new CancellationTokenSource().Token
            ).Result);

            Assert.True(_handler.Handle(
                new ValidatePassword() { Password = "AaTp9-fok" },
                new CancellationTokenSource().Token
            ).Result);
        }
    }
}
