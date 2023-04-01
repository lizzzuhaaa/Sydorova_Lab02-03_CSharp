using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01Sydorova.Exceptions
{
    internal class ValidationEmail : Exception
    {
        private string _message;

        public ValidationEmail(string message)
        {
            _message = message;
        }
        public ValidationEmail(string message, string email) : this("Your email is invalid format")
        {
        }

        public override string Message
        {
            get { return _message; }
        }
    }
}
