using Domain.Commands;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class ValidatePasswordHandler : IRequestHandler<ValidatePassword, bool>
    {

        private readonly char[] SpecialChars = "!@#$%^&*()-+".ToCharArray();

        public Task<bool> Handle(ValidatePassword request, CancellationToken cancellationToken)
        {
            return Task.FromResult(IsValidPassword(request.Password));
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length <= 8 || !HasUniqueCharacters(password))
            {
                return false;
            }

            var currentScore = 0;

            var sawUpper = false;
            var sawLower = false;
            var sawDigit = false;
            var sawSpecial = false;

            foreach (var c in password)
            {
                if (char.IsWhiteSpace(c))
                {
                    return false;
                }

                if (!sawSpecial && IsSpecial(c))
                {
                    currentScore += 1;
                    sawSpecial = true;
                }
                else
                {
                    if (!sawDigit && char.IsDigit(c))
                    {
                        currentScore += 1;
                        sawDigit = true;
                    }
                    else
                    {
                        if (!sawUpper || !sawLower)
                        {
                            if (!char.IsUpper(c))
                            {
                                sawUpper = true;
                            } 
                            else
                            {
                                sawLower = true;
                            }

                            if (sawUpper && sawLower)
                            {
                                currentScore += 1;
                            }
                        }
                    }
                }
            }

            if (!sawSpecial || !sawUpper || !sawLower || !sawDigit)
            {
                return false;
            }

            return currentScore switch
            {
                0 => false,
                1 => false,
                _ => true,
            };
        }

        private bool IsSpecial(char value) => SpecialChars.Contains(value);

        private bool HasUniqueCharacters(string value)
        {
            for (int i = 0; i < value.Length - 1; i++)
            {
                for (int j = i + 1; j < value.Length; j++)
                {
                    if (value[i] == value[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
