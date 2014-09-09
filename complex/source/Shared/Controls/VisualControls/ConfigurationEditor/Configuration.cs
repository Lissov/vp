using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualControls.ConfigurationEditor
{
    public class Configuration
    {
        public Configuration()
        {
            Vertices = new List<Vertex>();
            Links = new List<Link>();
        }

        public List<Vertex> Vertices
        {
            get;
            set;
        }

        public List<Link> Links
        {
            get;
            set;
        }
    }

    public class ConfigurationObject
    {
        public int ID { get; set; }

        public object Properties { get; set; }
    }

    public class Vertex : ConfigurationObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int? RealHeight { get; set; }

        public string DisplayText { get; set; }
    }

    public class Link : ConfigurationObject
    {
        public Vertex Begin { get; set; }
        public Vertex End { get; set; }
    }
}
