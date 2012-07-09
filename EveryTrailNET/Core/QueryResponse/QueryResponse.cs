using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveryTrailNET.Objects.Users;

namespace EveryTrailNET.Core.QueryResponse
{
    public class QueryResponse
    {
        public bool Status { get; set; }
    }
    public class UserLoginResponse : QueryResponse
    {
        public int UserID { get; set; }
    }

    public enum CheckUserEmailStatus { Success, IncorrectEmailFormat, EmailAddressTaken, Unknown };
    public class CheckUserEmailResponse
    {
        public CheckUserEmailStatus Status = CheckUserEmailStatus.Unknown;
    }

    public enum CheckUserNameStatus { Success, UserNameTaken, Unknown };
    public class CheckUserNameResponse
    {
        public CheckUserNameStatus Status = CheckUserNameStatus.Unknown;
    }

    public enum UserProfileInfoStatus { Success, Unknown };
    public class UserProfileResponse
    {
        public User ResponseUser { get; set; }
        public UserProfileInfoStatus Status { get; set; }
    }
    
}
