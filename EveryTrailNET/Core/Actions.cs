using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveryTrailNET.Core.QueryResponse;
using EveryTrailNET.Objects;
using EveryTrailNET.Objects.Users;

namespace EveryTrailNET.Core
{
    /// <summary>
    /// Methods used to perform actions against the EveryTrail API
    /// </summary>
    public static class Actions
    {
        public enum UserTripTypes { MyTrips, FavoriteTrips };

        private static IEveryTrailActionProcessor _implementation = new XmlActionProcessor();


        public static void SetImplementation(IEveryTrailActionProcessor impl)
        {
            _implementation = impl;
        }

        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="userName">User name for user to login</param>
        /// <param name="password">Password for user to login</param>
        /// <returns>Login response with status and user ID if successful</returns>
        public static UserLoginResponse UserLogin(string userName, string password)
        {
            return _implementation.UserLogin(userName, password);
        }
        public static Trip SingleTrip(int tripId)
        {
            return _implementation.SingleTrip(tripId);
        }
        public static List<Trip> FavoriteTrips(int userId)
        {
            return _implementation.FavoriteTrips(userId);
        }
        public static CheckUserNameResponse CheckUserName(string userName)
        {
            return _implementation.CheckUserName(userName);
        }
        public static CheckUserEmailResponse CheckUserEmail(string email)
        {
            return _implementation.CheckUserEmail(email);
        }
        public static UserProfileResponse UserProfileInfo(int userId)
        {
            return _implementation.UserProfileInfo(userId);
        }
        public static List<User> UserFollowers(int userId)
        {
            return _implementation.GetUserFollowers(userId);
        }
        public static SearchResponse Search(string searchQuery)
        {
            return _implementation.Search(searchQuery);
        }
        public static void TripData(int tripId)
        {
            _implementation.TripData(tripId);
        }
        public static void GetUserTrips(int userId)
        {
            _implementation.GetUserTrips(userId);
        }
    }
    

}
