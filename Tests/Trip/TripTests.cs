using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EveryTrailNET.Objects;
using EveryTrailNET.Core;
using System.Configuration;

namespace Tests.TripTests
{
    [TestFixture]
    public class TripTests
    {
        private bool _runImplementationTests;

        [TestFixtureSetUp]
        public void Setup()
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["RunImplementationTests"]))
            {
                bool.TryParse(ConfigurationManager.AppSettings["RunImplementationTests"], out _runImplementationTests);
            }
        }

        [Test]
        public void SingleTripIntegration()
        {
            if (_runImplementationTests)
            {
                Trip t = Actions.SingleTrip(620755);

                Assert.AreEqual("Sedona HH", t.Name.Trim());
                Assert.AreEqual(99442, t.UserID);
                Assert.AreEqual(6, t.TripActivity.ID);
                Assert.AreEqual("Not familiar with Sedona trails but I'll do my best to describe the route:Made in the Shade-Highline-Baldwin-Templeton-HT-Little Horse-Broken Arrow-High on the Hog-Mystic then took SR 179 back from there.I've never been so beat up by so few miles and feet of climbing.", t.Description);
                Assert.AreEqual(new DateTime(2010, 5, 15), t.TripDate);
                Assert.AreEqual(26520.324736073, t.Length.Amount);
                //{05:43:37}
                Assert.AreEqual(new TimeSpan(5, 43, 37), t.Duration);
                Assert.AreEqual("http://cdn.everytrail.com/gpx/620755.gpx", t.GPXLocation);
                Assert.AreEqual("http://cdn.everytrail.com/kml/620755.kml", t.Kml);

                Assert.AreEqual(0, t.Rating.NumberVotes);
                Assert.AreEqual(0.0, t.Rating.OverallRating);
            }
        }
    }
}
