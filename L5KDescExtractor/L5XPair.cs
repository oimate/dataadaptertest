using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L5KDescExtractor
{
    public struct L5XPair
    {
        public L5XPair(XElement element, XElement basefiledesc, XElement comparefiledesc)
        {
            this.element = element;
            this.basefiledesc = basefiledesc;
            this.comparefiledesc = comparefiledesc;
            this.selection = 0;
        }
        public XElement element, basefiledesc, comparefiledesc;
        public string Element { get { return GetElementAsString(); } }
        public string BaseFileDesc { get { return basefiledesc.Value; } }
        public string CompareFileDesc { get { return comparefiledesc.Value; } }

        private int selection;
        public int Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        private string GetElementAsString()
        {
            switch (element.Name.LocalName)
            {
                case "Tag":
                    return element.FirstAttribute.Value;
                case "Rung":
                    return string.Format("{0}@{1}", element.Parent.Parent.FirstAttribute.Value, element.FirstAttribute.Value);
                default:
                    throw new ArgumentOutOfRangeException("element name not supported");
            }
        }

    }
}
