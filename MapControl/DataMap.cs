﻿using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms
{
    public class DataMap : System.Windows.Forms.Map
    {
        public class Line
        {
            private System.Point _BeginGeoLocation;
            private Color _Color;
            private System.Point _EndGeoLocation;
            private Object _Object;

            public System.Point BeginGeoLocation
            {
                get
                {
                    return _BeginGeoLocation;
                }
                set
                {
                    _BeginGeoLocation = value;
                }
            }

            public Color Color
            {
                get
                {
                    return _Color;
                }
                set
                {
                    _Color = value;
                }
            }

            public System.Point EndGeoLocation
            {
                get
                {
                    return _EndGeoLocation;
                }
                set
                {
                    _EndGeoLocation = value;
                }
            }

            public Object Object
            {
                get
                {
                    return _Object;
                }
                set
                {
                    _Object = value;
                }
            }
        }

        public class Point
        {
            private Color _Color;
            private System.Point _GeoLocation;
            private Object _Object;

            public Color Color
            {
                get
                {
                    return _Color;
                }
                set
                {
                    _Color = value;
                }
            }

            public System.Point GeoLocation
            {
                get
                {
                    return _GeoLocation;
                }
                set
                {
                    _GeoLocation = value;
                }
            }

            public Object Object
            {
                get
                {
                    return _Object;
                }
                set
                {
                    _Object = value;
                }
            }
        }

        private readonly List<Line> _Lines;
        private readonly List<Point> _Points;

        public List<Line> Lines
        {
            get
            {
                return _Lines;
            }
        }

        public List<Point> Points
        {
            get
            {
                return _Points;
            }
        }

        public DataMap()
        {
            _Lines = new List<Line>();
            _Points = new List<Point>();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);
            foreach(var Line in _Lines)
            {
                var BeginScreenLocation = GetScreenLocationFromGeoLocation(Line.BeginGeoLocation);
                var EndScreenLocation = GetScreenLocationFromGeoLocation(Line.EndGeoLocation);

                if(((BeginScreenLocation.X >= 0) && (BeginScreenLocation.Y >= 0) && (BeginScreenLocation.X <= Width) && (BeginScreenLocation.Y <= Height)) || ((EndScreenLocation.X >= 0) && (EndScreenLocation.Y >= 0) && (EndScreenLocation.X <= Width) && (EndScreenLocation.Y <= Height)))
                {
                    EventArguments.Graphics.DrawLine(new Pen(Line.Color, 5), BeginScreenLocation.X, BeginScreenLocation.Y, EndScreenLocation.X, EndScreenLocation.Y);
                }
            }
            foreach(var Point in _Points)
            {
                var ScreenLocation = GetScreenLocationFromGeoLocation(Point.GeoLocation);

                if((ScreenLocation.X >= 0) && (ScreenLocation.Y >= 0) && (ScreenLocation.X <= Width) && (ScreenLocation.Y <= Height))
                {
                    EventArguments.Graphics.FillRectangle(new SolidBrush(Point.Color), ScreenLocation.X - 2, ScreenLocation.Y - 2, 5, 5);
                }
            }
        }
    }
}
