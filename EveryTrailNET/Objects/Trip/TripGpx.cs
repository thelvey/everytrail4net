using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EveryTrailNET.Objects
{
    public class TripGpx
    {
        private List<GpxTrack> _tracks;

        public int EveryTrailId { get; set; }

        public List<GpxTrack> Tracks 
        {
            get
            {
                if (_tracks == null)
                {
                    _tracks = new List<GpxTrack>();
                }
                return _tracks;
            }
            set { _tracks = value; }
        }

        public static TripGpx FromXDocument(XDocument xdoc)
        {
            if (xdoc == null) return null;
            TripGpx result = new TripGpx();

            XNamespace ns = xdoc.Root.Attribute("xmlns").Value;

            IEnumerable<XElement> tracks = xdoc.Root.Elements(ns + "trk");

            foreach (XElement trackEle in tracks)
            {
                GpxTrack gt = new GpxTrack();

                XElement nameEle = trackEle.Element(ns + "name");
                if (nameEle != null) gt.Name = nameEle.Value;

                IEnumerable<XElement> pt = trackEle.Element(ns + "trkseg").Elements(ns + "trkpt");

                foreach (XElement elePt in pt)
                {
                    GpxTrackPoint gp = new GpxTrackPoint();
                    if (elePt.Attribute("lat") != null)
                    {
                        gp.Latitude = Convert.ToDouble(elePt.Attribute("lat").Value);
                    }
                    if (elePt.Attribute("lon") != null)
                    {
                        gp.Longitude = Convert.ToDouble(elePt.Attribute("lon").Value);
                    }

                    XElement eleEle = elePt.Element(ns + "ele");
                    if (eleEle != null && !String.IsNullOrEmpty(eleEle.Value))
                    {
                        gp.Elevation = Convert.ToDouble(eleEle.Value);
                    }

                    XElement timeEle = elePt.Element(ns + "time");
                    if (timeEle != null)
                    {
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(timeEle.Value, out dt))
                        {
                            gp.TimeRecorded = dt;
                        }
                    }

                    gt.Points.Add(gp);
                }

                result.Tracks.Add(gt);
            }

            return result;
        }
    }

    public class GpxTrack
    {
        private List<GpxTrackPoint> _points;

        public string Name { get; set; }
        public List<GpxTrackPoint> Points
        {
            get
            {
                if (_points == null) _points = new List<GpxTrackPoint>();
                return _points;
            }
            set { _points = value; }
        }

    }
    public class GpxTrackPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public DateTime TimeRecorded { get; set; }
    }
}
