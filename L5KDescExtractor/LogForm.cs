using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L5KDescExtractor
{
    public partial class LogForm : Form
    {
        public delegate void UpdateLoglinesDelegate(List<string> loglines);

        public LogForm(List<string> loglines)
        {
            InitializeComponent();
            logfield.Lines = loglines.ToArray();
        }

        public void UpdateLoglines(List<string> loglines)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new UpdateLoglinesDelegate(UpdateLoglines), loglines);
            }
            else
            {
                logfield.Lines = loglines.ToArray();
            }
        }
    }
}
