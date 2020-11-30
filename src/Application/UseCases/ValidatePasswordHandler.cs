using Domain.Commands;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ValidatePasswordHandler : IRequestHandler<ValidatePassword, bool>
    {
        private const int MinPasswordLength = 8;

        private readonly char[] SpecialChars = "!@#$%^&*()-+".ToCharArray();

        public Task<bool> Handle(ValidatePassword request, CancellationToken cancellationToken)
        {
            return Task.FromResult(IsValidPassword(request.Password));
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length <= MinPasswordLength || !HasUniqueCharacters(password))
            {
                return false;
            }

            var currentScore = 0;

            var hasUpper = false;
            var hasLower = false;
            var hasDigit = false;
            var hasSpecial = false;

            foreach (var c in password)
            {
                if (char.IsWhiteSpace(c))
                {
                    return false;
                }

                if (!hasSpecial && IsSpecial(c))
                {
                    currentScore += 1;
                    hasSpecial = true;
                }
                else
                {
                    if (!hasDigit && char.IsDigit(c))
                    {
                        currentScore += 1;
                        hasDigit = true;
                    }
                    else
                    {
                        if (!hasUpper || !hasLower)
                        {
                            if (!char.IsUpper(c))
                            {
                                hasUpper = true;
                            } 
                            else
                            {
                                hasLower = true;
                            }

                            if (hasUpper && hasLower)
                            {
                                currentScore += 1;
                            }
                        }
                    }
                }
            }

            if (!hasSpecial || !hasUpper || !hasLower || !hasDigit)
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
