using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveryTrailNET.Core.QueryResponse;
using EveryTrailNET.Objects;
using EveryTrailNET.Objects.Users;

namespace EveryTrailNET.Core
{
    public interface IEveryTrailActionProcessor
    {
        #region Users
        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="userName">username of the user making the request</param>
        /// <param name="password">password of the user making the request</param>
        /// <returns>UserLoginResponse containing status of response and ID if successful</returns>
        UserLoginResponse UserLogin(string userName, string password);

        /// <summary>
        /// Checks if a username is available for registration
        /// </summary>
        /// <param name="userName">Username to check</param>
        /// <returns>CheckUserNameResponse with response status</returns>
        CheckUserNameResponse CheckUserName(string userName);
        
        /// <summary>
        /// Checks if an email address is available for registration
        /// </summary>
        /// <param name="email">Email address to check</param>
        /// <returns>CheckUserEmailResponse with response status</returns>
        CheckUserEmailResponse CheckUserEmail(string email);

        /// <summary>
        /// Get the information for a given user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>UserProfileResponse containing status and User if successful</returns>
        UserProfileResponse UserProfileInfo(int userId);

        /// <summary>
        /// Get a list of trips for a given user
        /// </summary>
        /// <param name="userId">The id of the onwer of the trips</param>
        /// <param name="tripType"></param>
        /// <param name="limit">The maximum number of results to return (default is 20)</param>
        /// <param name="start">The index of the first trip tor return from the result set. (default is 0)</param>
        /// <returns>List of Trip objects representing trips for this user</returns>
        List<Trip> GetUserTrips(int userId, Actions.UserTripTypes tripType, int limit, int start);

        /// <summary>
        /// Get users following a given user
        /// </summary>
        /// <param name="userId">The id of the user which the returned users are following</param>
        /// <returns>A list of User objects representing the users following this user</returns>
        List<User> GetUserFollowers(int userId);
        #endregion

        #region Trips
        /// <summary>
        /// Get the information for a trip
        /// </summary>
        /// <param name="tripId">The id of the trip</param>
        /// <returns>Trip object representing this single trip, if found</returns>
        Trip SingleTrip(int tripId);

        /// <summary>
        /// Get a list of trips that the given user has marked as favorites
        /// </summary>
        /// <param name="userId">The id of the owner of the trips</param>
        /// <returns>List of Trip objects representing the given user's favorite Trips</returns>
        List<Trip> FavoriteTrips(int userId);
        #endregion
    }
}
