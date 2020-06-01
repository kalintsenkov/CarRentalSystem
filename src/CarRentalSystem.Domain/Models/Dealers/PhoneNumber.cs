namespace CarRentalSystem.Domain.Models.Dealers
{
    using System.Text.RegularExpressions;
    using Common;
    using Exceptions;

    using static ModelConstants.PhoneNumber;

    public class PhoneNumber : ValueObject
    {
        internal PhoneNumber(string number)
        {
            this.Validate(number);

            this.Number = number;
        }

        public string Number { get; }

        public static implicit operator string(PhoneNumber number) => number.Number;

        public static implicit operator PhoneNumber(string number) => new PhoneNumber(number);

        private void Validate(string number)
        {
            if (!Regex.IsMatch(number, PhoneNumberRegularExpression))
            {
                throw new InvalidPhoneNumberException("Phone number must start with a '+' and contain only digits afterwards.");
            }

            Guard.ForStringLength<InvalidPhoneNumberException>(
                number,
                MinPhoneNumberLength,
                MaxPhoneNumberLength,
                nameof(PhoneNumber));
        }
    }
}