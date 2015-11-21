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
        public ComparisionForm()
        {
            InitializeComponent();
        }

        L5XFile f1, f2;
        List<L5XPair> rungs, tags;
        BindingSource src_rungs, src_tags;
        async private void button1_Click(object sender, EventArgs e)
        {
            f1 = new L5XFile(@"f:\users\PLRADSLI\Documents\Work\PROJECTS\ford kentucky\_SW\CT_319102.L5X");
            f2 = new L5XFile(@"f:\users\PLRADSLI\Documents\Work\PROJECTS\ford kentucky\_SW\CT_319102_spioch.L5X");

            var compare = new L5XComparer(f1, f2);
            rungs = await Task.Run<List<L5XPair>>(() => { return compare.GetRungs(); });
            tags = await Task.Run<List<L5XPair>>(() => { return compare.GetTags(); });

            System.IO.File.WriteAllLines("runglog.txt", compare.LogRungs.ToArray());
            System.IO.File.WriteAllLines("taglog.txt", compare.LogTags.ToArray());

            MessageBox.Show("Done!");

            src_rungs = new BindingSource();
            src_rungs.DataSource = rungs;
            dgv_rungs.DataSource = src_rungs;

            src_tags = new BindingSource();
            src_tags.DataSource = tags;
            dgv_tags.DataSource = src_tags;

            //System.Diagnostics.Debugger.Break();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (f1 != null)
            {
                //f1.XmlFile.Save("CT_319102_after.L5X");
            }
        }

        private void acceptFromBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    foreach (DataGridViewRow item in dgv_rungs.SelectedRows)
                    {
                        var pair = rungs[item.Index];
                        pair.Selection = 1;
                    }
                    src_rungs.ResetBindings(false);
                    break;
                case 1:
                    foreach (DataGridViewRow item in dgv_tags.SelectedRows)
                    {
                        var pair = tags[item.Index];
                        pair.Selection = 1;
                    }
                    src_tags.ResetBindings(false);
                    break;
                default:
                    throw new Exception("Unexpected selection!");
            }
        }

        private void acceptFromCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    foreach (DataGridViewRow item in dgv_rungs.SelectedRows)
                    {
                        var pair = rungs[item.Index];
                        pair.Selection = 2;
                    }
                    src_rungs.ResetBindings(false);
                    break;
                case 1:
                    foreach (DataGridViewRow item in dgv_tags.SelectedRows)
                    {
                        var pair = tags[item.Index];
                        pair.Selection = 2;
                    }
                    src_tags.ResetBindings(false);
                    break;
                default:
                    throw new Exception("Unexpected selection!");
            }
        }
    }
}
