using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Aurora.Domain.ValueTypes
{
    public struct Password
    {
        private readonly string _value;
        public readonly Contract contract;

        private Password(string value)
        {
            _value = value;
            contract = new Contract();
            Validate();

            if(contract.Valid)
                _value = Convert.ToBase64String(new UTF8Encoding().GetBytes(_value));
        }

        public override string ToString() =>
            _value;

        public static implicit operator Password(string input) =>
            new Password(input.Trim());

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return AddNotification("Is necessary to inform the Password.");

            if(_value.Length > 10)
                return AddNotification("The password must be less than 10 characters.");

            // if (!Regex.IsMatch(_value, (@"[A-Z]")));
               // return AddNotification("The password cannot contain uppercase letters");

            if (!Regex.IsMatch(_value, (@"[^a-zA-Z0-9]")))
                return AddNotification("The password must contain a special character.");

            return true;
        }

        private bool AddNotification(string message)
        {
            contract.AddNotification(nameof(Password), message);
            return false;
        }
    }
}
