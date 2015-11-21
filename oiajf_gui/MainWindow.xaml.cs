using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using L5KDescExtractor;

namespace oiajf_gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        async private void Button_Click(object sender, RoutedEventArgs e)
        {
            var f1 = new L5XFile(@"f:\users\PLRADSLI\Documents\Work\PROJECTS\ford kentucky\_SW\CT_319102.L5X");
            var f2 = new L5XFile(@"f:\users\PLRADSLI\Documents\Work\PROJECTS\ford kentucky\_SW\CT_319102_spioch.L5X");

            var compare = new L5XComparer(f1, f2);
            List<L5XPair> rungs = await Task.Run<List<L5XPair>>(() => { return compare.GetRungs(); });
            List<L5XPair> tags = await Task.Run<List<L5XPair>>(() => { return compare.GetTags(); });

            System.IO.File.WriteAllLines("runglog.txt", compare.LogRungs.ToArray());
            System.IO.File.WriteAllLines("taglog.txt", compare.LogTags.ToArray());

            await this.ShowMessageAsync("Job done!", "Rung and tags compared!");

            dupa.ItemsSource = tags;

        }
    }
}
