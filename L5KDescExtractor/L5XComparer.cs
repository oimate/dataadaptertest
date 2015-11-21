using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5KDescExtractor
{
    public class L5XComparer
    {
        public string BaseFilePath { get; set; }
        public string CompareFilePath { get; set; }

        public L5XFile BaseFile { get; set; }
        public L5XFile CompareFile { get; set; }

        public L5XComparer()
        { }
        public L5XComparer(string baseFilePath, string compareFilePath)
        {
            BaseFilePath = baseFilePath;
            BaseFile = new L5XFile(BaseFilePath);
            CompareFilePath = compareFilePath;
            CompareFile = new L5XFile(compareFilePath);
        }
        public L5XComparer(L5XFile baseFile, L5XFile compareFile)
        {
            BaseFile = baseFile;
            CompareFile = compareFile;
        }

        List<string> logRungs;
        public List<string> LogRungs
        {
            get
            {
                if (logRungs == null) logRungs = new List<string>();
                return logRungs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentExceptions"></exception>
        public List<L5XPair> GetRungs()
        {
            return GetRungs(BaseFile, CompareFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentExceptions"></exception>
        public List<L5XPair> GetRungs(L5XFile baseFile, L5XFile compareFile)
        {
            if (baseFile == null || compareFile == null)
                throw new ArgumentNullException("One or both files are not parsed!");
            //rungs
            List<L5XPair> list = new List<L5XPair>();

            if (baseFile.Rungs.Count != compareFile.Rungs.Count)
            {
                throw new ArgumentException("Different rung count!");
            }
            else
            {
                LogRungs.Add(string.Format("routine\trung\tbase file comment\tcompare file comment"));
                for (int i = 0; i < baseFile.Rungs.Count; i++)
                {
                    if (baseFile.Rungs[i].Parent.Parent.FirstAttribute.Value == compareFile.Rungs[i].Parent.Parent.FirstAttribute.Value)
                    {
                        if (baseFile.Rungs[i].FirstAttribute.Value == compareFile.Rungs[i].FirstAttribute.Value)
                        {
                            if (baseFile.Rungs[i].Element("Comment") == null && compareFile.Rungs[i].Element("Comment") == null)
                                continue;
                            if (baseFile.Rungs[i].Element("Comment") != null && compareFile.Rungs[i].Element("Comment") == null)
                            {
                                //System.Diagnostics.Debugger.Break();
                                var msg = string.Format("{0}\t{1}\t{2}\t",
                                    baseFile.Rungs[i].Parent.Parent.FirstAttribute.Value,
                                    baseFile.Rungs[i].FirstAttribute.Value,
                                    baseFile.Rungs[i].Element("Comment").Value.Replace('\n', ' ')
                                    );
                                LogRungs.Add(msg);
                                continue;
                            }
                            if (baseFile.Rungs[i].Element("Comment") == null && compareFile.Rungs[i].Element("Comment") != null)
                            {
                                //System.Diagnostics.Debugger.Break();
                                baseFile.Rungs[i].AddFirst(compareFile.Rungs[i].Element("Comment"));
                                var msg = string.Format("{0}\t{1}\t\t{2}",
                                    baseFile.Rungs[i].Parent.Parent.FirstAttribute.Value,
                                    baseFile.Rungs[i].FirstAttribute.Value,
                                    compareFile.Rungs[i].Element("Comment").Value.Replace('\n', ' ')
                                    );
                                LogRungs.Add(msg);
                                continue;
                            }
                            if (baseFile.Rungs[i].Element("Comment").Value != compareFile.Rungs[i].Element("Comment").Value)
                            {
                                //System.Diagnostics.Debugger.Break();
                                var pair = new L5XPair(baseFile.Rungs[i], baseFile.Rungs[i].Element("Comment"), compareFile.Rungs[i].Element("Comment"));
                                list.Add(pair);
                                var msg = string.Format("{0}\t{1}\t{2}\t{3}",
                                    baseFile.Rungs[i].Parent.Parent.FirstAttribute.Value,
                                    baseFile.Rungs[i].FirstAttribute.Value,
                                    baseFile.Rungs[i].Element("Comment").Value.Replace('\n', ' '),
                                    compareFile.Rungs[i].Element("Comment").Value.Replace('\n', ' ')
                                    );
                                LogRungs.Add(msg);
                                continue;
                            }
                        }
                    }
                }
            }
            return list;
        }

        List<string> logTags;
        public List<string> LogTags
        {
            get
            {
                if (logTags == null) logTags = new List<string>();
                return logTags;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentExceptions"></exception>
        public List<L5XPair> GetTags()
        {
            return GetTags(BaseFile, CompareFile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentExceptions"></exception>
        public List<L5XPair> GetTags(L5XFile baseFile, L5XFile compareFile)
        {
            if (baseFile == null || compareFile == null)
                throw new ArgumentNullException("One or both files are not parsed!");
            //tags

            List<L5XPair> list = new List<L5XPair>();

            if (baseFile.Tags.Count != compareFile.Tags.Count)
            {
                throw new Exception("Different tag count!");
            }
            else
            {
                LogTags.Add(string.Format("tag\tbase file description\tcompare file description"));
                for (int i = 0; i < baseFile.Tags.Count; i++)
                {
                    var desc1 = baseFile.Tags[i].Element("Description");
                    var desc2 = compareFile.Tags[i].Element("Description");

                    if (desc1 != null && desc2 != null)
                    {
                        if (desc1.Value != desc2.Value)
                        {
                            //System.Diagnostics.Debugger.Break();
                            var pair = new L5XPair(baseFile.Tags[i],desc1, desc2);
                            list.Add(pair);
                            var msg = string.Format("{0}\t{1}\t{2}", baseFile.Tags[i].FirstAttribute.Value, desc1.Value.Replace('\n', ' '), desc2.Value).Replace('\n', ' ');
                            LogTags.Add(msg);
                        }
                    }
                    else if (desc1 == null && desc2 != null)
                    {
                        //System.Diagnostics.Debugger.Break();
                        baseFile.Tags[i].AddFirst(desc2);
                        var msg = string.Format("{0}\t\t{1}", baseFile.Tags[i].FirstAttribute.Value, desc2.Value.Replace('\n', ' '));
                        LogTags.Add(msg);
                    }
                    else if (desc1 != null && desc2 == null)
                    {
                        //System.Diagnostics.Debugger.Break();
                        var msg = string.Format("{0}\t{1}\t", baseFile.Tags[i].FirstAttribute.Value, desc1.Value.Replace('\n', ' '));
                        LogTags.Add(msg);
                    }
                }
            }
            return list;
        }
    }
}
