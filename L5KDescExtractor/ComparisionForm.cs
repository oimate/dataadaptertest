using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace L5KDescExtractor
{
    public partial class ComparisionForm : Form
    {
        private List<XElement> tags_with_desc_2;
        public List<XElement> Tags_with_desc_2
        {
            get
            {
                if (tags_with_desc_2 == null) tags_with_desc_2 = new List<XElement>();
                return tags_with_desc_2;
            }
        }

        List<string> log = new List<string>();
        void Log(string msg)
        {
            var line = string.Format("{0}\t{1}", DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"), msg);
            log.Add(line);
            using (var sw = System.IO.File.AppendText("log.txt"))
            {
                sw.WriteLine(line);
            }
        }

        public ComparisionForm()
        {
            InitializeComponent();
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            var f1 = new L5XFile(@"f:\users\PLRADSLI\Documents\Work\PROJECTS\ford kentucky\_SW\CT_319102.L5X");
            var f2 = new L5XFile(@"f:\users\PLRADSLI\Documents\Work\PROJECTS\ford kentucky\_SW\CT_319102_spioch.L5X");

            var compare = new L5XComparer(f1, f2);
            List<L5XPair> rungs = await Task.Run<List<L5XPair>>(() => { return compare.GetRungs(); });
            List<L5XPair> tags = await Task.Run<List<L5XPair>>(() => { return compare.GetTags(); });

            System.IO.File.WriteAllLines("runglog.txt", compare.LogRungs.ToArray());
            System.IO.File.WriteAllLines("taglog.txt", compare.LogTags.ToArray());

            MessageBox.Show("Done!");

            dgv_rungs.DataSource = rungs;
            dgv_tags.DataSource = tags;

            //System.Diagnostics.Debugger.Break();
        }
    }
}
