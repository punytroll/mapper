using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace GPS.KML.Version_2_2
{
    public class Coordinates
    {
        public Double Latitude;
        public Double Longitude;
        public Double Altitude;
    }

    public class LineString
    {
        public List<Coordinates> Coordinates
        {
            get
            {
                return _Coordinates;
            }
        }

        private List<Coordinates> _Coordinates;

        private LineString()
        {
            _Coordinates = new List<Coordinates>();
        }

        public static LineString ReadFromLineStringElement(XmlElement LineStringElement)
        {
            var Result = new LineString();

            foreach(XmlNode ChildNode in LineStringElement.ChildNodes)
            {
                if((ChildNode.NodeType == XmlNodeType.Element) && (ChildNode.Name == "coordinates"))
                {
                    foreach(var Tuple in _GetTuples(ChildNode.InnerText))
                    {
                        var Parts = Tuple.Split(',');

                        if((Parts.Length >= 2) && (Parts.Length <= 3))
                        {
                            var Coordinates = new Coordinates();

                            Coordinates.Longitude = Double.Parse(Parts[0], CultureInfo.InvariantCulture);
                            Coordinates.Latitude = Double.Parse(Parts[1], CultureInfo.InvariantCulture);
                            if(Parts.Length > 2)
                            {
                                Coordinates.Altitude = Double.Parse(Parts[2], CultureInfo.InvariantCulture);
                            }
                            Result._Coordinates.Add(Coordinates);
                        }
                    }
                }
            }

            return Result;
        }

        private static List<String> _GetTuples(String Text)
        {
            var Result = new List<String>();
            String Tuple = null;

            foreach(var Character in Text)
            {
                if((Character == '\n') || (Character == ' '))
                {
                    if(Tuple != null)
                    {
                        Result.Add(Tuple);
                        Tuple = null;
                    }
                }
                else
                {
                    Tuple += Character;
                }
            }
            if(Tuple != null)
            {
                Result.Add(Tuple);
                Tuple = null;
            }

            return Result;
        }
    }

    public class Placemark
    {
        public LineString LineString
        {
            get
            {
                return _LineString;
            }
        }

        private LineString _LineString;

        private Placemark()
        {
            _LineString = null;
        }

        public static Placemark ReadFromPlacemarkElement(XmlElement PlacemarkElement)
        {
            var Result = new Placemark();

            foreach(XmlNode ChildNode in PlacemarkElement.ChildNodes)
            {
                if((ChildNode.NodeType == XmlNodeType.Element) && (ChildNode.Name == "LineString"))
                {
                    Result._LineString = LineString.ReadFromLineStringElement((XmlElement)ChildNode);
                }
            }

            return Result;
        }
    }

    public class KML
    {
        public List<Placemark> Placemarks
        {
            get
            {
                return _Placemarks;
            }
        }

        private List<Placemark> _Placemarks;

        private KML()
        {
            _Placemarks = new List<Placemark>();
        }

        public static KML ReadFromStream(Stream Stream)
        {
            KML Result = null;
            var XMLDocument = new XmlDocument();

            XMLDocument.Load(Stream);

            var RootElement = XMLDocument.DocumentElement;

            if((RootElement != null) && (RootElement.Name == "kml"))
            {
                Result = new KML();
                foreach(XmlNode ChildNode in RootElement.ChildNodes)
                {
                    if((ChildNode.NodeType == XmlNodeType.Element) && (ChildNode.Name == "Document"))
                    {
                        foreach(XmlNode DocumentChildNode in ChildNode.ChildNodes)
                        {
                            if((DocumentChildNode.NodeType == XmlNodeType.Element) && (DocumentChildNode.Name == "Placemark"))
                            {
                                Result._Placemarks.Add(Placemark.ReadFromPlacemarkElement((XmlElement)DocumentChildNode));
                            }
                        }
                    }
                }
            }
            else
            {
                throw new FormatException("The root element is not a \"kml\" element.");
            }

            return Result;
        }
    }
}
