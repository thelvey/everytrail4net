using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace EveryTrailNET.Objects
{
    public class Trip
    {
        private const string _gpxLocationBase = "http://www.everytrail.com/downloadGPX.php?trip_id=";

        public int TripID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public Activity TripActivity { get; set; }
        public string Description { get; set; }
        public string Tips { get; set; }
        public DateTime TripDate { get; set; }
        public DateTime TripUpdatedDate { get; set; }
        public Length Length { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Visible { get; set; }
        public TripRating Rating { get; set; }
        public Location TripLocation { get; set; }
        public string GPXLocation { get; set; }
        public string Kml { get; set; }

        public string RealGPXLocation
        {
            get
            {
                return _gpxLocationBase + TripID;
            }
        }

        public static XDocument GetGpx(string gpxLocation)
        {
            XDocument result = null;

            try
            {
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(gpxLocation);

                WebResponse resp = wr.GetResponse();
                if (resp.ContentLength > 0)
                {
                    result = XDocument.Load(resp.GetResponseStream());
                }
                resp.Close();
            }
            catch (WebException exc)
            {
                // do something
            }
            return result;
        }
        public static Trip FromXElement(XElement tEle)
        {
            Trip t = new Trip();
            t.TripID = Convert.ToInt32(tEle.Attribute("id").Value);
            t.Name = tEle.Element("name").Value;
            t.UserID = Convert.ToInt32(tEle.Element("user").Attribute("id").Value);
            t.UserName = tEle.Element("user").Value;

            t.TripActivity = new Activity();
            t.TripActivity.ID = Convert.ToInt32(tEle.Element("activity").Attribute("id").Value);
            t.TripActivity.Name = tEle.Element("activity").Value;

            t.Description = tEle.Element("description").Value.Trim();
            t.Tips = tEle.Element("tips").Value.Trim();

            t.TripDate = DateTime.Parse(tEle.Element("date").Value);

            if (tEle.Element("updated_date") != null)
            {
                DateTime updatedDate = DateTime.MinValue;
                DateTime.TryParse(tEle.Element("updated_date").Value, out updatedDate);
                t.TripUpdatedDate = updatedDate;
            }

            t.Length = new Length();
            t.Length.Amount = double.Parse(tEle.Element("length").Value);
            // if this changes at some point this will need to actually check
            t.Length.Units = Length.LengthUnits.Metric;

            t.Duration = new TimeSpan(0, 0, Convert.ToInt32(tEle.Element("duration").Value));

            if (tEle.Element("visibility") != null)
            {
                t.Visible = Convert.ToBoolean(Convert.ToInt32(tEle.Element("visibility").Value));
            }

            t.Rating = new TripRating();
            t.Rating.NumberVotes = Convert.ToInt32(tEle.Element("rating").Attribute("votes").Value);
            t.Rating.OverallRating = double.Parse(tEle.Element("rating").Value);

            t.TripLocation = new Location();
            t.TripLocation.Description = tEle.Element("location").Value;
            t.TripLocation.Latitude = double.Parse(tEle.Element("location").Attribute("lat").Value);
            t.TripLocation.Longitude = double.Parse(tEle.Element("location").Attribute("lon").Value);

            t.GPXLocation = tEle.Element("gpx").Value;

            if (tEle.Element("kml") != null)
            {
                t.Kml = tEle.Element("kml").Value;
            }
            return t;
        }
    }
}
