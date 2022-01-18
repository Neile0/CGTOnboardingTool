using CGTOnboardingTool.Securities;
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

namespace CGTOnboardingTool.UISections
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Report report;

        public Dashboard(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            var rows = report.Rows();
            if (rows.Count() > 0)
            {
                var row = rows[0];


                MessageBox.Show(row.Security.ToString());
            }

            
            


            this.DashboardReportView.ItemsSource = rows;
        }
    }
}
