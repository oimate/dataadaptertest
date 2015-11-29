using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L5KDescExtractor
{
    //externator
    public partial class MainWindow : Form
    {
        string filepath;
        Dictionary<string, string> tags;
        Dictionary<string, string> estop;
        List<string> networks;
        List<AliasMagicNrDesc> fltlist;
        List<AliasMagicNrDesc> msglist;
        List<AliasMagicNrDesc> bitlist;

        #region GUI (constructor, buttons)
        public MainWindow()
        {
            InitializeComponent();
        }
        private void bStart_Click(object sender, EventArgs e)
        {
            //string filepath = @"f:\tmp\CT_319104.L5K";
            ParseL5K(filepath);
        }
        private void bL5kFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "L5K files (*.L5K)|*.L5K|L5X files (*.L5X)|*.L5X";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bStart.Enabled = true;
                filepath = ofd.FileName;
            }
        }
        #endregion

        private void ParseL5K(string filepath)
        {
            var ext = Path.GetExtension(filepath).ToUpper();
            switch (ext)
            {
                case ".L5X":
                    L5XFile f = new L5XFile(filepath);
                    tags = GetTags(f);
                    estop = GetEstop(f);
                    networks = (from r in f.Rungs
                                select r.Value).ToList();
                    break;
                case ".L5K":
                    string fileinmemory = File.ReadAllText(filepath);
                    tags = GetTags(fileinmemory);
                    estop = GetEstop(fileinmemory);
                    networks = GetNetworks(fileinmemory);
                    break;
                default:
                    break;
            }
            fltlist = GetFltList(networks);
            msglist = GetMsgList(networks);
            bitlist = GetBitList(networks);
            ExportToFile(Path.GetDirectoryName(filepath), Path.GetFileNameWithoutExtension(filepath), ext + ".xls");
        }
        private void ExportToFile(string directory, string filenamenoext, string ext)
        {
            string path = directory + "\\" + filenamenoext + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
            FileInfo fi = new FileInfo(path);
            using (StreamWriter sw = fi.CreateText())
            {
                foreach (var item in fltlist)
                {
                    sw.WriteLine(item.ToString());
                }
                foreach (var item in msglist)
                {
                    sw.WriteLine(item.ToString());
                }
                foreach (var item in bitlist)
                {
                    sw.WriteLine(item.ToString());
                }
            }
        }
        private List<AliasMagicNrDesc> GetFltList(List<string> networks)
        {
            List<AliasMagicNrDesc> ret = new List<AliasMagicNrDesc>();
            foreach (String network in networks)
            {
                if (!network.Contains(@"Pf_Alarm8(")) continue;
                ret.AddRange(Parse_pf_Alarm8(network));
            }
            return ret;
        }
        private List<AliasMagicNrDesc> GetMsgList(List<string> networks)
        {
            List<AliasMagicNrDesc> ret = new List<AliasMagicNrDesc>();
            foreach (String network in networks)
            {
                if (!network.Contains(@"Pf_ExtOpMsg8(")) continue;
                //oteinstructions = ParseNetworkForOte(network);
                ret.AddRange(Parse_pf_Msg8(network));
            }
            return ret;
        }
        private List<AliasMagicNrDesc> GetBitList(List<string> networks)
        {
            List<AliasMagicNrDesc> ret = new List<AliasMagicNrDesc>();
            foreach (String network in networks)
            {
                if (!network.Contains(@"Sf_OpBit16(")) continue;
                //oteinstructions = ParseNetworkForOte(network);
                ret.AddRange(Parse_sf_OpBit16(network));
            }
            return ret;
        }
        private Dictionary<string, string> GetEstop(L5XFile fileinmemory)
        {
            var dict = new Dictionary<string, string>();
            var estop = fileinmemory.Datatypes.First(d => d.FirstAttribute.Value == "ud_EStop");
            var bits = estop.Descendants("Member").Where(m => m.Attribute("DataType").Value == "BIT").ToList();
            foreach (var item in bits)
            {
                var desc = item.Element("Description").Value;
                var name = item.FirstAttribute.Value;
                dict.Add(name, desc);
            }
            return dict;
        }
        private Dictionary<string, string> GetEstop(string fileinmemory)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            Regex r = new Regex(@"(?<=\r\n\tDATATYPE\sud_EStop\s).*?(?=\r\n\tEND_DATATYPE)", RegexOptions.Singleline);
            Match m = r.Match(fileinmemory);
            string datatype = m.Value;

            Regex bit = new Regex("BIT\\s(?<tag>\\S+)\\s\\S+\\s:\\s\\d\\s\\(Description\\s:=\\s\"(?<desc>[^\"]+)\"\\);", RegexOptions.Singleline);
            Match bitm = bit.Match(datatype);

            while (bitm.Success)
            {
                string tag = bitm.Groups["tag"].Value;
                string desc = bitm.Groups["desc"].Value;
                ret.Add(tag, desc);
                bitm = bitm.NextMatch();
            }

            return ret;
        }
        private List<AliasMagicNrDesc> Parse_pf_Alarm8(String network)
        {
            Dictionary<string, string> oteinstructions = ParseNetworkForOte(network);
            List<AliasMagicNrDesc> ret = new List<AliasMagicNrDesc>();
            Regex iw;
            Regex r = new Regex(@"Pf_Alarm8\((?<fbi>\S+?),(?<ifw1>\d+?),(?<ifw2>\d+?),(?<ifw3>\d+?),(?<ifw4>\d+?),(?<ifw5>\d+?),(?<ifw6>\d+?),(?<ifw7>\d+?),(?<ifw8>\d+?),(?<zVar>\S+?),(?<zGen>\S+?)\)", RegexOptions.Singleline);
            Match pf8;
            pf8 = r.Match(network);
            string fbi = pf8.Groups["fbi"].Value;
            if (fbi == "FBI_CD03_DC0344_DIMFM3")
            {
                //System.Diagnostics.Debugger.Break();
            }
            int[] IFW = new int[8];
            IFW[0] = int.Parse(pf8.Groups["ifw1"].Value);
            IFW[1] = int.Parse(pf8.Groups["ifw2"].Value);
            IFW[2] = int.Parse(pf8.Groups["ifw3"].Value);
            IFW[3] = int.Parse(pf8.Groups["ifw4"].Value);
            IFW[4] = int.Parse(pf8.Groups["ifw5"].Value);
            IFW[5] = int.Parse(pf8.Groups["ifw6"].Value);
            IFW[6] = int.Parse(pf8.Groups["ifw7"].Value);
            IFW[7] = int.Parse(pf8.Groups["ifw8"].Value);

            //string pattern = string.Format("(XIC|XIO)\\((?<alias>\\S+)\\)\\sOTE\\({0}.IFW(?<nr>\\d)\\)", fbi);
            string pattern = string.Format(@"(XIC|XIO)\((?<alias>\S+)\)");
            iw = new Regex(pattern, RegexOptions.Singleline);
            //Match aliasm = iw.Match(network);

            for (int i = 1; i < 9; i++)
            {
                string fbikey = string.Format("{0}.IFW{1}", fbi, i);
                if (!oteinstructions.ContainsKey(fbikey)) continue;
                Match logic = iw.Match(oteinstructions[fbikey]);
                if (logic.Success)
                {
                    //int ifw = int.Parse(logic.Groups["nr"].Value);
                    string alias = logic.Groups["alias"].Value;
                    if (alias.ToLower() == "true") //IFW[ifw - 1] == 0 
                    {
                        logic = logic.NextMatch();
                        continue;
                    }
                    bool checkestop = alias.Contains("ESTOP.");
                    string desc = string.Empty;
                    if (tags.ContainsKey(alias))
                    {
                        desc = tags[alias];
                    }
                    else if (checkestop)
                    {
                        string estopkey = alias.Substring(6, alias.Length - 6);
                        if (estop.ContainsKey(estopkey))
                        {
                            alias = estopkey;
                            desc = estop[estopkey];
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(alias);
                        }
                    }
                    else if (alias.Contains("_LifeBit"))
                    {
                        string[] split = alias.Split('.');
                        alias = split[0];
                        desc = "HMI Warning";
                    }
                    else
                    {
                        //desc = alias;
                        System.Diagnostics.Debug.WriteLine(alias);
                    }
                    AliasMagicNrDesc retal = new AliasMagicNrDesc(alias, IFW[i - 1], desc, "A", oteinstructions[fbikey]);
                    ret.Add(retal);
                }
                else
                {
                    string instr = oteinstructions[fbikey];
                    instr = (!string.IsNullOrWhiteSpace(instr)) ? instr : fbikey;
                    AliasMagicNrDesc retal = new AliasMagicNrDesc(oteinstructions[fbikey], IFW[i - 1], string.Empty, "A", instr);
                    ret.Add(retal);
                }
            }
            return ret;
        }
        private List<AliasMagicNrDesc> Parse_pf_Msg8(String network)
        {
            Dictionary<string, string> oteinstructions = ParseNetworkForOte(network);
            List<AliasMagicNrDesc> ret = new List<AliasMagicNrDesc>();
            Regex iw;
            Regex r = new Regex(@"Pf_ExtOpMsg8\((?<fbi>\S+?),(?<ifw1>\d+?),(?<ifw2>\d+?),(?<ifw3>\d+?),(?<ifw4>\d+?),(?<ifw5>\d+?),(?<ifw6>\d+?),(?<ifw7>\d+?),(?<ifw8>\d+?),(?<zVar>\S+?)\)", RegexOptions.Singleline);
            Match msg8;
            msg8 = r.Match(network);
            string fbi = msg8.Groups["fbi"].Value;
            int[] IFW = new int[8];
            IFW[0] = int.Parse(msg8.Groups["ifw1"].Value);
            IFW[1] = int.Parse(msg8.Groups["ifw2"].Value);
            IFW[2] = int.Parse(msg8.Groups["ifw3"].Value);
            IFW[3] = int.Parse(msg8.Groups["ifw4"].Value);
            IFW[4] = int.Parse(msg8.Groups["ifw5"].Value);
            IFW[5] = int.Parse(msg8.Groups["ifw6"].Value);
            IFW[6] = int.Parse(msg8.Groups["ifw7"].Value);
            IFW[7] = int.Parse(msg8.Groups["ifw8"].Value);

            //string pattern = string.Format("(XIC|XIO)\\((?<alias>\\S+)\\)\\sOTE\\({0}.IOM(?<nr>\\d)\\)", fbi);
            string pattern = string.Format(@"(XIC|XIO)\((?<alias>\S+)\)");
            iw = new Regex(pattern, RegexOptions.Singleline);
            //Match aliasm = iw.Match(network);

            for (int i = 1; i < 9; i++)
            {
                string fbikey = string.Format("{0}.IOM{1}", fbi, i);
                if (!oteinstructions.ContainsKey(fbikey)) continue;
                Match logic = iw.Match(oteinstructions[fbikey]);
                if (logic.Success)
                {
                    //int ifw = int.Parse(logic.Groups["nr"].Value);
                    string alias = logic.Groups["alias"].Value;
                    if (alias.ToLower() == "false") //IFW[ifw - 1] == 0 
                    {
                        logic = logic.NextMatch();
                        continue;
                    }
                    string desc = string.Empty;
                    if (tags.ContainsKey(alias))
                    {
                        desc = tags[alias];
                    }
                    else
                    {
                        //desc = alias;
                        System.Diagnostics.Debug.WriteLine(alias);
                    }
                    AliasMagicNrDesc retal = new AliasMagicNrDesc(alias, IFW[i - 1], desc, "S", oteinstructions[fbikey]);
                    ret.Add(retal);
                }
                else
                {
                    string instr = oteinstructions[fbikey];
                    instr = (!string.IsNullOrWhiteSpace(instr)) ? instr : fbikey;
                    AliasMagicNrDesc retal = new AliasMagicNrDesc(oteinstructions[fbikey], IFW[i - 1], string.Empty, "S", instr);
                    ret.Add(retal);
                }
            }
            //while (aliasm.Success)
            //{
            //    int ifw = int.Parse(aliasm.Groups["nr"].Value);
            //    string alias = aliasm.Groups["alias"].Value;
            //    if (alias.ToLower() == "false") //IFW[ifw - 1] == 0 
            //    {
            //        aliasm = aliasm.NextMatch();
            //        continue;
            //    }
            //    string desc = string.Empty;
            //    if (tags.ContainsKey(alias))
            //    {
            //        desc = tags[alias];
            //    }
            //    else
            //    {
            //        System.Diagnostics.Debug.WriteLine(alias);
            //    }
            //    AliasMagicNrDesc retal = new AliasMagicNrDesc(alias, IFW[ifw - 1], desc, "S", string.Empty);
            //    ret.Add(retal);
            //    aliasm = aliasm.NextMatch();
            //}
            return ret;
        }
        private List<AliasMagicNrDesc> Parse_sf_OpBit16(String network)
        {
            Dictionary<string, string> bitinstructions = ParseNetworkForBit(network);
            List<AliasMagicNrDesc> ret = new List<AliasMagicNrDesc>();
            Regex iw;
            Regex r = new Regex(@"Sf_OpBit16\((?<fbi>[^,]+),(?<int1>\d+),(?<int2>\d+),(?<int3>\d+),(?<int4>\d+),(?<int5>\d+),(?<int6>\d+),(?<int7>\d+),(?<int8>\d+),(?<int9>\d+),(?<int10>\d+),(?<int11>\d+),(?<int12>\d+),(?<int13>\d+),(?<int14>\d+),(?<int15>\d+),(?<int16>\d+),(?<zvar>[^,]+),(?<zgen>[^\)]+)\)", RegexOptions.Singleline);
            Match msg8;
            msg8 = r.Match(network);
            string fbi = msg8.Groups["fbi"].Value;
            int[] IFW = new int[16];
            IFW[0] = int.Parse(msg8.Groups["int1"].Value);
            IFW[1] = int.Parse(msg8.Groups["int2"].Value);
            IFW[2] = int.Parse(msg8.Groups["int3"].Value);
            IFW[3] = int.Parse(msg8.Groups["int4"].Value);
            IFW[4] = int.Parse(msg8.Groups["int5"].Value);
            IFW[5] = int.Parse(msg8.Groups["int6"].Value);
            IFW[6] = int.Parse(msg8.Groups["int7"].Value);
            IFW[7] = int.Parse(msg8.Groups["int8"].Value);
            IFW[8] = int.Parse(msg8.Groups["int9"].Value);
            IFW[9] = int.Parse(msg8.Groups["int10"].Value);
            IFW[10] = int.Parse(msg8.Groups["int11"].Value);
            IFW[11] = int.Parse(msg8.Groups["int12"].Value);
            IFW[12] = int.Parse(msg8.Groups["int13"].Value);
            IFW[13] = int.Parse(msg8.Groups["int14"].Value);
            IFW[14] = int.Parse(msg8.Groups["int15"].Value);
            IFW[15] = int.Parse(msg8.Groups["int16"].Value);

            for (int i = 1; i < 17; i++)
            {
                string fbikey = string.Format("{0}.OpBit{1}", fbi, i);
                if (!bitinstructions.ContainsKey(fbikey)) continue;
                string output = bitinstructions[fbikey];
                if (output != "Nop_0" || IFW[i - 1] != 0)
                {
                    output = (output != "Nop_0") ? output : string.Empty;
                    AliasMagicNrDesc retal = new AliasMagicNrDesc(output, IFW[i - 1], string.Empty, "S", bitinstructions[fbikey]);
                    ret.Add(retal);
                }
            }
            return ret;
        }
        private List<string> GetNetworks(string fileinmemory)
        {
            var programs = GetPrograms(fileinmemory);
            Regex r = new Regex(@"(?<=\t{5}N:\s).*?(?=;)", RegexOptions.Singleline);
            List<string> ret = new List<string>();
            foreach (string program in programs)
            {
                MatchCollection networks = r.Matches(program);
                foreach (Match network in networks)
                {
                    ret.Add(network.Value);
                }
            }
            return ret;
        }
        private List<string> GetPrograms(string fileinmemory)
        {
            List<string> ret = new List<string>();
            Regex r = new Regex(@"(?<=\r\n\tPROGRAM\s).*?(?=\r\n\tEND_PROGRAM)", RegexOptions.Singleline);
            MatchCollection programs = r.Matches(fileinmemory);
            foreach (Match program in programs)
            {
                ret.Add(program.Value);
            }
            return ret;
        }
        private Dictionary<string, string> GetTags(L5XFile fileinmemory)
        {
            string name, description;
            var dict = new Dictionary<string, string>();
            foreach (var item in fileinmemory.Tags)
            {
                var desc = item.Element("Description");
                if (desc != null)
                {
                    name = desc.Parent.FirstAttribute.Value;
                    description = desc.Value;
                    dict.Add(name, description);
                }
            }
            return dict;
        }
        private Dictionary<string, string> GetTags(string fileinmemory)
        {
            Regex r = new Regex(@"(?<=\r\n\tTAG\r\n)(?<tags>.*)(?=\r\n\tEND_TAG)", RegexOptions.Singleline);
            Match m = r.Match(fileinmemory);
            string tagsstring = m.Value;

            Regex alias = new Regex(@"(?<=\t\t)(?<name>\S+)\sOF\s\S+\s\((?<prop>.*?)\)(?=;)", RegexOptions.Singleline);
            m = alias.Match(tagsstring);

            Regex desc = new Regex("Description := \"(?<desc>.*)\"", RegexOptions.Singleline);

            Dictionary<string, string> ret = new Dictionary<string, string>();
            while (m.Success)
            {
                string description = desc.Match(m.Value).Groups["desc"].Value;
                ret.Add(m.Groups["name"].Value, description);
                m = m.NextMatch();
            }

            return ret;
        }
        private Dictionary<string, string> ParseNetworkForOte(string network)
        {
            string instruction;
            Dictionary<string, string> oteinstructions = new Dictionary<string, string>();
            Regex ote = new Regex(@"OTE\((?<inp>\S*?)\)", RegexOptions.Singleline);
            MatchCollection otemc = ote.Matches(network);
            char c;
            foreach (Match otem in otemc)
            {
                if (otem.Groups["inp"].Value == "FBI_CD03_DC0344_DIMFM3.IFW1")
                {
                    //System.Diagnostics.Debugger.Break();
                }
                int cl = 0;
                bool found = false;
                for (int i = otem.Index - 1; i > 0; i--)
                {
                    c = network[i];
                    switch (c)
                    {
                        case '(':
                            cl--;
                            break;
                        case ')':
                            cl++;
                            break;
                        case ']':
                            cl++;
                            break;
                        case '[':
                            cl--;
                            if (cl >= 0) continue;
                            instruction = network.Substring(i + 1, otem.Index - i - 1).Trim();
                            oteinstructions.Add(otem.Groups["inp"].Value, instruction);
                            found = true;
                            break;
                        case ',':
                            if (cl != 0) continue;
                            instruction = network.Substring(i + 1, otem.Index - i - 1).Trim();
                            oteinstructions.Add(otem.Groups["inp"].Value, instruction);
                            found = true;
                            break;
                        default:
                            if (i != 0) continue;
                            instruction = network.Substring(i + 1, otem.Index - i - 1).Trim();
                            oteinstructions.Add(otem.Groups["inp"].Value, instruction);
                            found = true;
                            break;
                    }
                    if (found) break;
                }
            }
            return oteinstructions;
        }
        private Dictionary<string, string> ParseNetworkForBit(string network)
        {
            Dictionary<string, string> bitinstructions = new Dictionary<string, string>();
            Regex bitsy = new Regex(@"XIC\((?<fbi>[^\.]+)\.(?<out>[^\)]+)\) OTE\((?<ote>[^\)]+)\)", RegexOptions.Singleline);
            MatchCollection ms = bitsy.Matches(network);
            foreach (Match m in ms)
            {
                string key = string.Format("{0}.{1}", m.Groups["fbi"].Value, m.Groups["out"].Value);
                string val = m.Groups["ote"].Value;
                bitinstructions.Add(key, val);
            }
            return bitinstructions;
        }
    }

    public struct AliasMagicNrDesc
    {
        public string Alias;
        public int MagicNr;
        public string Desc;
        public string Type;
        public string Instruction;
        public AliasMagicNrDesc(string a, int m, string d, string t, string i)
        {
            Alias = a;
            MagicNr = m;
            Desc = d;
            Type = t;
            Instruction = i;
        }
        public override string ToString()
        {
            string nodesc = string.Empty;
            string nomgnr = string.Empty;
            string basestring = string.Format("T03_\t{3}{1}\tmessage\t\t{0} {2}", Alias, MagicNr, Desc, Type);
            if (string.IsNullOrWhiteSpace(Desc))
            {
                nodesc = "no descr";
            }
            if (MagicNr == 0)
            {
                nomgnr = "no magicnr";
            }
            basestring = string.Format("{0}\t{1}\t{2}\t{3}", basestring, nodesc, nomgnr, Instruction);
            return basestring;
        }
    }

    public struct Instruction
    {
        public string Ote;
        public string Text;
        public Instruction(string o, string i)
        {
            Ote = o;
            Text = i;
        }
    }
}