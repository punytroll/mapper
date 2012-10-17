namespace GPS.GPX.DOM10
{
    public enum Fix
    {
        None,
        TwoD,
        ThreeD,
        DGPS,
        PPS
    }

    public class TrackPoint
    {
        public System.DateTime DateTime
        {
            get
            {
                if(_DateTime != null)
                {
                    return _DateTime.Value;
                }
                else
                {
                    throw new System.MissingFieldException("TrackPoint", "DateTime");
                }
            }
            set
            {
                _DateTime = value;
            }
        }

        public System.Double Elevation
        {
            get
            {
                if(_Elevation != null)
                {
                    return _Elevation.Value;
                }
                else
                {
                    throw new System.MissingFieldException("TrackPoint", "Elevation");
                }
            }
            set
            {
                _Elevation = value;
            }
        }

        public GPS.GPX.DOM10.Fix Fix
        {
            get
            {
                if(_Fix != null)
                {
                    return _Fix.Value;
                }
                else
                {
                    throw new System.MissingFieldException("TrackPoint", "Fix");
                }
            }
            set
            {
                _Fix = value;
            }
        }

        public System.Double Latitude
        {
            get
            {
                if(_Latitude != null)
                {
                    return _Latitude.Value;
                }
                else
                {
                    throw new System.MissingFieldException("TrackPoint", "Latitude");
                }
            }
            set
            {
                _Latitude = value;
            }
        }

        public System.Double Longitude
        {
            get
            {
                if(_Longitude != null)
                {
                    return _Longitude.Value;
                }
                else
                {
                    throw new System.MissingFieldException("TrackPoint", "Longitude");
                }
            }
            set
            {
                _Longitude = value;
            }
        }

        public System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public System.Double Speed
        {
            get
            {
                if(_Speed != null)
                {
                    return _Speed.Value;
                }
                else
                {
                    throw new System.MissingFieldException("TrackPoint", "Speed");
                }
            }
            set
            {
                _Speed = value;
            }
        }

        private System.DateTime? _DateTime;
        private System.Double? _Elevation;
        private GPS.GPX.DOM10.Fix? _Fix;
        private System.Double? _Latitude;
        private System.Double? _Longitude;
        private System.String _Name;
        private System.Double? _Speed;

        public static TrackPoint LoadFromTrackPointElement(System.Xml.XmlElement TrackPointElement)
        {
            if(TrackPointElement.Attributes["lat"] != null)
            {
                if(TrackPointElement.Attributes["lon"] != null)
                {
                    var Result = new TrackPoint();

                    Result._Latitude = System.Convert.ToDouble(TrackPointElement.Attributes["lat"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    Result._Longitude = System.Convert.ToDouble(TrackPointElement.Attributes["lon"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    foreach(System.Xml.XmlNode TrackPointChildNode in TrackPointElement.ChildNodes)
                    {
                        if((TrackPointChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackPointChildNode.Name == "ele"))
                        {
                            Result._Elevation = System.Convert.ToDouble(TrackPointChildNode.FirstChild.InnerText, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if((TrackPointChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackPointChildNode.Name == "name"))
                        {
                            Result._Name = TrackPointChildNode.FirstChild.InnerText;
                        }
                        else if((TrackPointChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackPointChildNode.Name == "speed"))
                        {
                            Result._Speed = System.Convert.ToDouble(TrackPointChildNode.FirstChild.InnerText, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if((TrackPointChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackPointChildNode.Name == "time"))
                        {
                            Result._DateTime = System.Convert.ToDateTime(TrackPointChildNode.FirstChild.InnerText, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if((TrackPointChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackPointChildNode.Name == "fix"))
                        {
                            Result._Fix = _ConvertStringToFix(TrackPointChildNode.FirstChild.InnerText);
                        }
                    }

                    return Result;
                }
                else
                {
                    throw new System.FormatException("The \"trkpt\" element has no \"lon\" attribute.");
                }
            }
            else
            {
                throw new System.FormatException("The \"trkpt\" element has no \"lat\" attribute.");
            }
        }

        public void SaveToStreamWriter(System.IO.StreamWriter StreamWriter)
        {
            if(_Latitude != null)
            {
                if(_Longitude != null)
                {
                    StreamWriter.Write("<trkpt lat=\"" + _Latitude.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\" lon=\"" + _Longitude.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\">\n");
                    if(_Elevation != null)
                    {
                        StreamWriter.Write("<ele>" + _Elevation.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "</ele>\n");
                    }
                    if(_DateTime != null)
                    {
                        StreamWriter.Write("<time>" + _DateTime.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "</time>\n");
                    }
                    if(_Speed != null)
                    {
                        StreamWriter.Write("<speed>" + _Speed.Value.ToString(System.Globalization.CultureInfo.InvariantCulture) + "</speed>\n");
                    }
                    if(_Name != null)
                    {
                        StreamWriter.Write("<name>" + _Name.ToString(System.Globalization.CultureInfo.InvariantCulture) + "</name>\n");
                    }
                    if(_Fix != null)
                    {
                        StreamWriter.Write("<fix>" + _ConvertFixToString(_Fix.Value) + "</fix>\n");
                    }
                    StreamWriter.Write("</trkpt>\n");
                }
                else
                {
                    throw new System.MissingFieldException("TrackPoint", "Longitude");
                }
            }
            else
            {
                throw new System.MissingFieldException("TrackPoint", "Longitude");
            }
        }

        public static GPS.GPX.DOM10.Fix _ConvertStringToFix(System.String String)
        {
            switch(String)
            {
            case "none":
                {
                    return GPS.GPX.DOM10.Fix.None;
                }
            case "2d":
                {
                    return GPS.GPX.DOM10.Fix.TwoD;
                }
            case "3d":
                {
                    return GPS.GPX.DOM10.Fix.ThreeD;
                }
            case "dgps":
                {
                    return GPS.GPX.DOM10.Fix.DGPS;
                }
            case "pps":
                {
                    return GPS.GPX.DOM10.Fix.PPS;
                }
            default:
                {
                    throw new System.FormatException("\"" + String + "\" has an invalid format for conversion to Fix.");
                }
            }
        }

        private static System.String _ConvertFixToString(GPS.GPX.DOM10.Fix Fix)
        {
            switch(Fix)
            {
            case GPS.GPX.DOM10.Fix.None:
                {
                    return "none";
                }
            case GPS.GPX.DOM10.Fix.TwoD:
                {
                    return "2d";
                }
            case GPS.GPX.DOM10.Fix.ThreeD:
                {
                    return "3d";
                }
            case GPS.GPX.DOM10.Fix.DGPS:
                {
                    return "dgps";
                }
            case GPS.GPX.DOM10.Fix.PPS:
                {
                    return "pps";
                }
            default:
                {
                    throw new System.FormatException();
                }
            }
        }
    }

    public class TrackSegment
    {
        public System.Collections.Generic.List<TrackPoint> TrackPoints
        {
            get
            {
                return _TrackPoints;
            }
        }

        private readonly System.Collections.Generic.List<TrackPoint> _TrackPoints;

        public TrackSegment()
        {
            _TrackPoints = new System.Collections.Generic.List<TrackPoint>();
        }

        public void SaveToStreamWriter(System.IO.StreamWriter StreamWriter)
        {
            StreamWriter.Write("<trkseg>\n");
            foreach(var TrackPoint in _TrackPoints)
            {
                TrackPoint.SaveToStreamWriter(StreamWriter);
            }
            StreamWriter.Write("</trkseg>\n");
        }
    }

    public class Track
    {
        public System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public System.Collections.Generic.List<TrackSegment> TrackSegments
        {
            get
            {
                return _TrackSegments;
            }
        }

        private System.String _Description;
        private System.String _Name;
        private readonly System.Collections.Generic.List<TrackSegment> _TrackSegments;

        public Track()
        {
            _TrackSegments = new System.Collections.Generic.List<TrackSegment>();
        }

        public static Track ReadFromTrackElement(System.Xml.XmlElement TrackElement)
        {
            var Result = new Track();

            foreach(System.Xml.XmlNode TrackChildNode in TrackElement.ChildNodes)
            {
                if((TrackChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackChildNode.Name == "description"))
                {
                    Result._Description = TrackChildNode.FirstChild.InnerText;
                }
                else if((TrackChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackChildNode.Name == "name"))
                {
                    Result._Name = TrackChildNode.FirstChild.InnerText;
                }
                else if((TrackChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackChildNode.Name == "trkseg"))
                {
                    var TrackSegment = new TrackSegment();

                    foreach(System.Xml.XmlNode TrackSegmentChildNode in TrackChildNode)
                    {
                        if((TrackSegmentChildNode.NodeType == System.Xml.XmlNodeType.Element) && (TrackSegmentChildNode.Name == "trkpt"))
                        {
                            TrackSegment.TrackPoints.Add(TrackPoint.LoadFromTrackPointElement((System.Xml.XmlElement)TrackSegmentChildNode));
                        }
                    }
                    Result._TrackSegments.Add(TrackSegment);
                }
            }

            return Result;
        }

        public void SaveToStreamWriter(System.IO.StreamWriter StreamWriter)
        {
            StreamWriter.Write("<trk>\n");
            if(_Name != null)
            {
                StreamWriter.Write("<name>" + _Name + "</name>\n");
            }
            if(_Description != null)
            {
                StreamWriter.Write("<desc>" + _Description + "</desc>\n");
            }
            foreach(var TrackSegment in _TrackSegments)
            {
                TrackSegment.SaveToStreamWriter(StreamWriter);
            }
            StreamWriter.Write("</trk>\n");
        }
    }

    public class GPX
    {
        public System.String Creator
        {
            get
            {
                return _Creator;
            }
            set
            {
                _Creator = value;
            }
        }

        public System.Collections.Generic.List<Track> Tracks
        {
            get
            {
                return _Tracks;
            }
        }

        public System.String Version
        {
            get
            {
                return "1.0";
            }
        }

        private readonly System.Collections.Generic.List<Track> _Tracks;
        private System.String _Creator;

        public GPX()
        {
            _Tracks = new System.Collections.Generic.List<Track>();
        }

        public static GPX ReadFromStream(System.IO.Stream Stream)
        {
            GPS.GPX.DOM10.GPX Result;

            var XMLDocument = new System.Xml.XmlDocument();

            XMLDocument.Load(Stream);

            var RootElement = XMLDocument.DocumentElement;

            if((RootElement != null) && (RootElement.Name == "gpx"))
            {
                if(RootElement.Attributes["version"] != null)
                {
                    if(RootElement.Attributes["version"].Value == "1.0")
                    {
                        Result = new GPS.GPX.DOM10.GPX();
                        if(RootElement.Attributes["creator"] != null)
                        {
                            Result._Creator = RootElement.Attributes["creator"].Value;
                            foreach(System.Xml.XmlNode GPXChildNode in RootElement.ChildNodes)
                            {
                                if((GPXChildNode.NodeType == System.Xml.XmlNodeType.Element) && (GPXChildNode.Name == "trk"))
                                {
                                    Result._Tracks.Add(Track.ReadFromTrackElement((System.Xml.XmlElement)GPXChildNode));
                                }
                            }
                        }
                        else
                        {
                            throw new System.FormatException("The \"gpx\" element has no \"creator\" attribute.");
                        }
                    }
                    else
                    {
                        throw new System.FormatException("The \"gpx\" element has an invalid \"version\" attribute value: \"" + RootElement.Attributes["version"].Value + "\"");
                    }
                }
                else
                {
                    throw new System.FormatException("The \"gpx\" element has no \"version\" attribute.");
                }
            }
            else
            {
                throw new System.FormatException("The root element is not a \"gpx\" element.");
            }

            return Result;
        }

        public void SaveToStreamWriter(System.IO.StreamWriter StreamWriter)
        {
            StreamWriter.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            StreamWriter.Write("<gpx version=\"1.0\" creator=\"Hagen Möbius - GPX Writer\">\n");
            foreach(var Track in _Tracks)
            {
                Track.SaveToStreamWriter(StreamWriter);
            }
            StreamWriter.Write("</gpx>\n");
        }
    }
}
