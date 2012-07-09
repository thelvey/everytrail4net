using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EveryTrailNET.Core;
using EveryTrailNET.Core.QueryResponse;
using Rhino.Mocks;
using System.IO;
using EveryTrailNET.Objects;
using EveryTrailNET.Objects.Users;

namespace Tests.User
{
    [TestFixture]
    public class UserTests
    {
        private bool _runImplementationTests = false;

        private string _realUser = "realuser";
        private string _realPass = "realpass";

        private string _userName = "username";
        private string _password = "password";

        [TestFixtureSetUp]
        public void Setup()
        {
        }

        [Test]
        public void UserLogin()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;
            string responseText = "<etUserLoginResponse status=\"success\"><userID>1</userID></etUserLoginResponse>";
            Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(responseText));
            response.ResponseStream = s;

            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {


                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                UserLoginResponse loginResponse = Actions.UserLogin(_userName, _password);

                Assert.AreEqual(true, loginResponse.Status);
                Assert.AreEqual(1, loginResponse.UserID);
            }
        }
        [Test]
        public void UserLoginFailed()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;
            string responseText = "<etUserLoginResponse status=\"error\">  <errors>    <error>      <code>11</code>      <message />    </error>  </errors></etUserLoginResponse>";
            Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(responseText));
            response.ResponseStream = s;


            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                UserLoginResponse loginResponse = Actions.UserLogin(_userName, _password);

                Assert.AreEqual(false, loginResponse.Status);
            }

        }

        [Test]
        public void UserLoginImplementation()
        {
            if (_runImplementationTests)
            {
                EveryTrailRequest.SetImplementation(new DefaultEveryTrailRequestHandler());

                UserLoginResponse loginResponse = Actions.UserLogin(_realUser, _realPass);

                Assert.AreEqual(true, loginResponse.Status);
                Assert.AreEqual(99442, loginResponse.UserID);

                UserLoginResponse failLoginResponse = Actions.UserLogin("3232kdsjflk2340sj", "232390dskjd202");

                Assert.AreEqual(false, failLoginResponse.Status);
            }
        }
        [Test]
        public void FavoriteTripsImplementation()
        {
            if (_runImplementationTests)
            {
                EveryTrailRequest.SetImplementation(new DefaultEveryTrailRequestHandler());
                List<Trip> l = Actions.FavoriteTrips(99442);
                Assert.AreEqual(2, l.Count);
                Assert.AreEqual(138074, l[0].UserID);
                Assert.AreEqual(149956, l[1].UserID);
            }
        }

        [Test]
        public void CheckUserNameInvalid()
        {
            string invalid = "Inv@l1dUs3rn@me!";

            Assert.Throws(typeof(UserNameInvalidException), delegate
            {
                Actions.CheckUserName(invalid);
            });
        }

        [Test]
        public void CheckUserNameValid()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            string validUserName = "Valid_Username-82383202434820";

            string responseText = "<etUserCheckUsernameResponse status=\"success\"><username>" + validUserName + "</username></etUserCheckUsernameResponse>";
            Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(responseText));
            response.ResponseStream = s;


            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                CheckUserNameResponse checkResponse = Actions.CheckUserName(validUserName);

                Assert.AreEqual(CheckUserNameStatus.Success, checkResponse.Status);
            }

        }


        [Test]
        public void CheckUserNameTaken()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            string validUserName = "tbone2000";

            string responseText = "<ETResponse><errors><error>6</error></errors></ETResponse>";
            Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(responseText));
            response.ResponseStream = s;


            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                CheckUserNameResponse checkResponse = Actions.CheckUserName(validUserName);

                Assert.AreEqual(CheckUserNameStatus.UserNameTaken, checkResponse.Status);
            }

        }
        [Test]
        public void CheckUserNameUnknown()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            string validUserName = "validusername";

            string responseText = "<ETResponse><errors><error>10</error></errors></ETResponse>";
            Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(responseText));
            response.ResponseStream = s;


            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                CheckUserNameResponse checkResponse = Actions.CheckUserName(validUserName);

                Assert.AreEqual(CheckUserNameStatus.Unknown, checkResponse.Status);
            }
        }
        [Test]
        public void CheckEmailTaken()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            string email = "takenemail@domain.com";
            //string responseText = @"<ETResponse><email>" + email + "</email></ETResponse>";
            string responseText = @"<ETResponse>
              <errors>
                <error>7</error>
              </errors>
            </ETResponse>";

            Stream s = new MemoryStream(Encoding.ASCII.GetBytes(responseText));
            response.ResponseStream = s;

            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                CheckUserEmailResponse checkEmailResponse = Actions.CheckUserEmail(email);

                Assert.AreEqual(CheckUserEmailStatus.EmailAddressTaken, checkEmailResponse.Status);
            }
        }
        [Test]
        public void CheckEmailInvalid()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            string email = "invalidemail";
            //string responseText = @"<ETResponse><email>" + email + "</email></ETResponse>";
            string responseText = @"<ETResponse>
              <errors>
                <error>4</error>
              </errors>
            </ETResponse>";

            Stream s = new MemoryStream(Encoding.ASCII.GetBytes(responseText));
            response.ResponseStream = s;

            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                CheckUserEmailResponse checkEmailResponse = Actions.CheckUserEmail(email);

                Assert.AreEqual(CheckUserEmailStatus.IncorrectEmailFormat, checkEmailResponse.Status);
            }
        }
        [Test]
        public void CheckEmailUnknown()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            string email = "invalidemail";
            //string responseText = @"<ETResponse><email>" + email + "</email></ETResponse>";
            string responseText = @"<ETResponse>
              <errors>
                <error>10</error>
              </errors>
            </ETResponse>";

            Stream s = new MemoryStream(Encoding.ASCII.GetBytes(responseText));
            response.ResponseStream = s;

            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                CheckUserEmailResponse checkEmailResponse = Actions.CheckUserEmail(email);

                Assert.AreEqual(CheckUserEmailStatus.Unknown, checkEmailResponse.Status);
            }
        }
        [Test]
        public void CheckEmailSuccess()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            string email = "success@email.address";
            //string responseText = @"<ETResponse><email>" + email + "</email></ETResponse>";
            string responseText = @"<ETResponse><email>" + email + "</email></ETResponse>";

            Stream s = new MemoryStream(Encoding.ASCII.GetBytes(responseText));
            response.ResponseStream = s;

            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                CheckUserEmailResponse checkEmailResponse = Actions.CheckUserEmail(email);

                Assert.AreEqual(CheckUserEmailStatus.Success, checkEmailResponse.Status);
            }
        }
        [Test]
        public void UserProfileInfoSuccess()
        {
            EveryTrailResponse response = new EveryTrailResponse();
            response.SuccessfulConnection = true;

            int userId = 1;
            string userName = "username";
            string firstName = "firstname";
            string lastName = "lastname";

            string responseText = "<etUserResponse status=\"success\"><user id=\"" + userId + "\"><username>" + userName + "</username><firstName>" + firstName + "</firstName><lastName>" + lastName + "</lastName><photo>http://images.everytrail.com/userpics/99442-cpoint.jpg</photo><intro>introtext</intro><intro><![CDATA[]]></intro><location /><pictureOffset>0</pictureOffset></user></etUserResponse>";

            Stream s = new MemoryStream(Encoding.ASCII.GetBytes(responseText));
            response.ResponseStream = s;

            MockRepository mocks = new MockRepository();
            IRequestHandler mockHandler = mocks.DynamicMock<IRequestHandler>();

            using (mocks.Record())
            {
                Expect.Call(mockHandler.MakeRequest(null, null))
                    .IgnoreArguments()
                    .Return(response);
            }
            using (mocks.Playback())
            {
                EveryTrailRequest.SetImplementation(mockHandler);

                UserProfileResponse userProfileResponse = Actions.UserProfileInfo(userId);

                Assert.AreEqual(UserProfileInfoStatus.Success, userProfileResponse.Status);
                Assert.AreEqual(userName, userProfileResponse.ResponseUser.UserName);
                Assert.AreEqual(firstName, userProfileResponse.ResponseUser.FirstName);
                Assert.AreEqual(lastName, userProfileResponse.ResponseUser.LastName);
            }
        }
        [Test]
        public void UserFollowerTest()
        {
            // todo: finish writing this test
            //int userId = 99442;
            //List<EveryTrailNET.Objects.Users.User> userFollowers = Actions.UserFollowers(userId);
        }
    }
}
