using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveryTrailNET.Objects
{
    public class UserException : Exception
    {
        public string UserName { get; set; }
    }
    public class UserNameInvalidException : UserException { }
    public class UserNameTakenException : UserException { }
    public class UserNameUnknownException : UserException { }

    public class UserEmailException : Exception
    {
        public string Email { get; set; }
    }
    public class IncorrectEmailFormatException : UserEmailException { }
    public class EmailAddressTakenException : UserEmailException { }
    public class EmailAddressUnknownException : UserEmailException { }
}
