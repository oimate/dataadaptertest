using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace L5KDescExtractor
{
    public class L5XFile
    {
        private string file;
        private XDocument xmlfile;
        private List<XElement> tags, rungs, datatypes, descriptions;

        public L5XFile(string file)
        {
            this.file = file;
        }

        public XDocument XmlFile
        {
            get
            {
                if (xmlfile == null && System.IO.File.Exists(file)) { xmlfile = ParseL5X(file); }
                return xmlfile;
            }
        }
        public List<XElement> Tags
        {
            get
            {
                if (tags == null && XmlFile != null) { tags = GetTags(XmlFile); }
                return tags;
            }
        }
        public List<XElement> Rungs
        {
            get
            {
                if (rungs == null && XmlFile != null) { rungs = GetRungs(XmlFile); }
                return rungs;
            }
        }
        public List<XElement> Datatypes
        {
            get
            {
                if (datatypes == null && XmlFile != null) { datatypes = GetTypes(XmlFile); }
                return datatypes;
            }
        }
        public List<XElement> Descriptions
        {
            get
            {
                if (descriptions == null && Tags != null) { descriptions = GetDescriptions(Tags); }
                return descriptions;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">path to L5X file</param>
        /// <returns>L5X file as XDocument</returns>
        public static XDocument ParseL5X(string file)
        {
            try
            {
                return XDocument.Load(file);
            }
            catch (Exception e)
            {
                log(e);
                return null;
            }
        }
        public static List<XElement> GetTags(XDocument file)
        {
            try
            {
                return (from t in file.Root.Element("Controller").Element("Tags").Descendants("Tag")
                        //where t.HasAttributes && (t.Attribute("TagType").Value == "Base" || t.Attribute("TagType").Value == "Consumed" || t.Attribute("TagType").Value == "Produced")
                        select t
                            ).ToList();
            }
            catch (Exception e)
            {
                log(e);
                return null;
            }
        }
        public static List<XElement> GetDescriptions(List<XElement> tags)
        {
            try
            {
                return tags.Descendants("Description").ToList();
            }
            catch (Exception e)
            {
                log(e);
                return null;
            }
        }
        public static List<XElement> GetTypes(XDocument file)
        {
            try
            {
                return file.Root.Element("Controller").Element("DataTypes").Descendants("DataType").ToList();
            }
            catch (Exception e)
            {
                log(e);
                return null;
            }
        }
        public static List<XElement> GetRungs(XDocument file)
        {
            try
            {
                //list = file.Root.Element("Controller").Elements("Programs").Elements("Routines").Elements("Routine").Elements("RLLContent").Descendants("Rung").ToList();
                return file.Root.Element("Controller").Element("Programs").Descendants("Routines").Descendants("Routine").Descendants("RLLContent").Descendants("Rung").ToList();
            }
            catch (Exception e)
            {
                log(e);
                return null;
            }
        }

        public void ReplaceComment(XElement element, XElement baseDescription, XElement compareDescription)
        {
            if (element.Name == "Tag" && baseDescription.Name == "Description" && compareDescription.Name == "Description")
            {
                if (baseDescription.Parent == element)
                {
                    baseDescription.Remove();
                    element.AddFirst(compareDescription);
                }
            }
            if (element.Name != "Rung" && baseDescription.Name != "Comment" && compareDescription.Name != "Comment")
            {
                if (baseDescription.Parent == element)
                {
                    baseDescription.Remove();
                    element.AddFirst(compareDescription);
                }
            }
            else
            {
                throw new ArgumentException("Unsupported element pair");
            }
        }

        #region Utils
        private static void log(Exception any)
        {
            log(any.Message);
        }
        private static void log(string p)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
