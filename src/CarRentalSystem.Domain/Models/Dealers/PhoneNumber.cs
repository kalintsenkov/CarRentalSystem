﻿namespace CarRentalSystem.Domain.Models.Dealers
{
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

        private void Validate(string number)
        {
            if (!number.StartsWith(PhoneNumberFirstSymbol))
            {
                throw new InvalidPhoneNumberException($"Phone number must start with a '{PhoneNumberFirstSymbol}'.");
            }

            Guard.ForStringLength<InvalidPhoneNumberException>(
                number,
                MinPhoneNumberLength,
                MaxPhoneNumberLength,
                nameof(this.Number));
        }
    }
}