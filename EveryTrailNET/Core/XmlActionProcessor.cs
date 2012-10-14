using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EveryTrailNET.Core.QueryResponse;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using EveryTrailNET.Objects;
using EveryTrailNET.Objects.Users;

namespace EveryTrailNET.Core
{
    public class XmlActionProcessor : IEveryTrailActionProcessor
    {
        private static readonly string _userLogin = "/api/user/login";
        private static readonly string _singleTrip = "/api/trip";
        private static readonly string _favoriteTrips = "/api/user/favoritetrips";
        private static readonly string _checkUserName = "/api/user/checkusername";
        private static readonly string _checkUserEmail = "/api/user/checkemail";
        private static readonly string _userProfileInfo = "/api/user";
        private static readonly string _usersFollowers = "/api/user/followers";
        private static readonly string _usersTrips = "/api/user/trips";
        private static readonly string _tripData = "/api/trip/data";
        private static readonly string _search = "/api/index/search/";

        public void TripData(int tripId)
        {
            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "trip_id", Value = tripId });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_tripData, requestParams);

            System.IO.StreamReader sr = new System.IO.StreamReader(response.ResponseStream);

            string tripData = sr.ReadToEnd();
        }
        public SearchResponse Search(string searchQuery, int guideLimit = 0)
        {
            SearchResponse result = new SearchResponse();

            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter(){ Name="q", Value=searchQuery});
            requestParams.Add(new EveryTrailRequestParameter(){ Name="guide_limit", Value= guideLimit});

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_search, requestParams);

            if(response.SuccessfulConnection)
            {
                XDocument xResponse = XDocument.Load(response.ResponseStream);

                XAttribute statusAttr = xResponse.Root.Attribute("status");

                if (statusAttr != null)
                {
                    if (statusAttr.Value == "success")
                    {
                        result.Status = true;

                        result.Trips = new List<Trip>();

                        XElement tripsEle = xResponse.Root.Element("trips");

                        IEnumerable<XElement> trips = tripsEle.Elements("trip");

                        foreach (XElement tripEle in trips)
                        {
                            result.Trips.Add(Trip.FromXElement(tripEle));
                        }
                    }
                }
            }

            return result;

        }
        public UserLoginResponse UserLogin(string userName, string password)
        {
            UserLoginResponse result = new UserLoginResponse();

            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "username", Value = userName });
            requestParams.Add(new EveryTrailRequestParameter() { Name = "password", Value = password });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_userLogin, requestParams);

            if (response.SuccessfulConnection)
            {
                XDocument xResponse = XDocument.Load(response.ResponseStream);

                XAttribute statusAttr = xResponse.Root.Attribute("status");
                if (statusAttr != null)
                {
                    if (statusAttr.Value == "success")
                    {
                        result.Status = true;

                        XElement userEle = xResponse.Root.Element("userID");
                        if (userEle != null)
                        {
                            string user = userEle.Value;
                            int userId = 0;
                            if (int.TryParse(user, out userId))
                            {
                                result.UserID = userId;
                            }
                        }
                    }
                    else
                    {
                        result.Status = false;
                    }
                }
                else
                {
                    result.Status = false;
                }
            }

            return result;
        }


        public List<Trip> GetUserTrips(int userId)
        {
            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "user_id", Value = userId });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_usersTrips, requestParams);

            if (response.SuccessfulConnection)
            {
                XDocument xResponse = XDocument.Load(response.ResponseStream);
            }
            return null;
        }


        public Trip SingleTrip(int tripId)
        {
            Trip result = new Trip();

            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "trip_id", Value = tripId });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_singleTrip, requestParams);

            if (response.SuccessfulConnection)
            {
                XDocument xResponse = XDocument.Load(response.ResponseStream);

                XElement tripEle = xResponse.Element("etTripResponse").Element("trip");

                if (tripEle != null)
                {
                    result = Trip.FromXElement(xResponse.Element("etTripResponse").Element("trip"));
                }
            }
            return result;
        }


        public List<Trip> FavoriteTrips(int userId)
        {
            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "user_id", Value = userId });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_favoriteTrips, requestParams);

            List<Trip> trips = new List<Trip>();

            if (response.SuccessfulConnection)
            {
                XDocument xResponse = XDocument.Load(response.ResponseStream);


                IEnumerable<XElement> ts = xResponse.Element("etUserFavoritetripsResponse").Element("trips").Elements("trip");

                foreach (XElement tEle in ts)
                {
                    Trip t = Trip.FromXElement(tEle);
                    trips.Add(t);
                }
            }

            return trips;
        }

        // EveryTrail Wiki Errata:
        // The XML format of the error response does not match 
        // the format in the Wiki
        public CheckUserNameResponse CheckUserName(string userName)
        {
            CheckUserNameResponse result = new CheckUserNameResponse();

            //first check if username is valid
            string invalidReg = @"([^\w\d-_])";
            if (Regex.IsMatch(userName, invalidReg))
            {
                throw new UserNameInvalidException() { UserName = userName };
            }

            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "username", Value = userName });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_checkUserName, requestParams);

            XDocument xResponse = XDocument.Load(response.ResponseStream);

            XAttribute statusAttr = xResponse.Root.Attribute("status");
            XElement errorEle = xResponse.Root.Element("errors");
            if (statusAttr != null)
            {
                if (statusAttr.Value == "success")
                {
                    XElement userNameEle = xResponse.Root.Element("username");
                    if (userNameEle != null)
                    {
                        if (userNameEle.Value == userNameEle.Value) result.Status = CheckUserNameStatus.Success;
                    }
                }
            }
            else if (errorEle != null)
            {
                XElement xError = errorEle.Element("error");
                if (xError != null)
                {
                    if (xError.Value == "6") result.Status = CheckUserNameStatus.UserNameTaken;
                    else if (xError.Value == "10") result.Status = CheckUserNameStatus.Unknown;
                    else { result.Status = CheckUserNameStatus.Unknown; }
                }
            }
            return result;
        }

        // Everytrail Wiki Errata: The actual XML response format from EveryTrail 
        // does not match the format in the Wiki
        public CheckUserEmailResponse CheckUserEmail(string email)
        {
            if (String.IsNullOrEmpty(email)) throw new IncorrectEmailFormatException();

            CheckUserEmailResponse result = new CheckUserEmailResponse() { Status = CheckUserEmailStatus.Unknown };

            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "email", Value = email });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_checkUserEmail, requestParams);

            XDocument xResponse = XDocument.Load(response.ResponseStream);

            if (xResponse != null)
            {
                if (xResponse.Root.Element("errors") != null)
                {
                    XElement errorEle = xResponse.Root.Element("errors").Element("error");

                    if (errorEle != null)
                    {
                        if (errorEle.Value == "7") result.Status = CheckUserEmailStatus.EmailAddressTaken;
                        else if (errorEle.Value == "4") result.Status = CheckUserEmailStatus.IncorrectEmailFormat;
                        else if (errorEle.Value == "10") result.Status = CheckUserEmailStatus.Unknown;
                    }
                }
                else if (xResponse.Root.Element("email") != null)
                {
                    XElement successEle = xResponse.Root.Element("email");
                    if (successEle.Value == email) result.Status = CheckUserEmailStatus.Success;
                }
            }

            return result;
        }


        public UserProfileResponse UserProfileInfo(int userId)
        {
            if (userId == 0) throw new Exception("Must provide user id");

            UserProfileResponse result = new UserProfileResponse() { Status = UserProfileInfoStatus.Unknown };

            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "user_id", Value = userId });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_userProfileInfo, requestParams);

            XDocument xResponse = XDocument.Load(response.ResponseStream);

            if (xResponse != null)
            {
                XAttribute status = xResponse.Root.Attribute("status");
                if (status != null)
                {
                    if (status.Value == "success")
                    {
                        XElement userEle = xResponse.Root.Element("user");
                        if (userEle != null)
                        {
                            User responseUser = UserFromXmlElement(userEle);
                            if (responseUser != null)
                            {
                                result.Status = UserProfileInfoStatus.Success;
                                result.ResponseUser = responseUser;
                            }
                            else
                            {
                                result.Status = UserProfileInfoStatus.Unknown;
                            }
                        }
                    }
                }
            }
            return result;
        }


        public List<User> GetUserFollowers(int userId)
        {
            if (userId == 0) throw new ArgumentException("Must provide user id");

            List<User> result = new List<User>();

            List<EveryTrailRequestParameter> requestParams = new List<EveryTrailRequestParameter>();
            requestParams = new List<EveryTrailRequestParameter>();
            requestParams.Add(new EveryTrailRequestParameter() { Name = "user_id", Value = userId });

            EveryTrailResponse response = EveryTrailRequest.MakeRequest(_usersFollowers, requestParams);

            XDocument xResponse = XDocument.Load(response.ResponseStream);

            if (xResponse != null)
            {
                IEnumerable<XElement> usersEle = xResponse.Root.Element("users").Elements("user");

                foreach (XElement userEle in usersEle)
                {
                    User follower = UserFromXmlElement(userEle);
                    if (follower != null)
                    {
                        result.Add(follower);
                    }
                }
            }

            return result;
        }
        private User UserFromXmlElement(XElement userEle)
        {
            User result = null;

            XAttribute userIdAttr = userEle.Attribute("id");
            if (userIdAttr != null)
            {
                result = new User();

                int userId = 0;

                int.TryParse(userIdAttr.Value, out userId);

                result.UserId = userId;

                XElement userNameEle = userEle.Element("username");
                if (userNameEle != null)
                {
                    result.UserName = userNameEle.Value;
                }

                XElement firstNameEle = userEle.Element("firstName");
                if (firstNameEle != null)
                {
                    result.FirstName = firstNameEle.Value;
                }

                XElement lastNameEle = userEle.Element("lastName");
                if (lastNameEle != null)
                {
                    result.LastName = lastNameEle.Value;
                }

            }

            return result;
        }
    }
}
