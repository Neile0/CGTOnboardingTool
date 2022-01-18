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
    /// Interaction logic for Rebuild.xaml
    /// </summary>
    public partial class Rebuild : Page
    {
        public Report report;
        private List<Security> securities;

        public Rebuild(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            Security gsk = new Security("GlaxoSmithKline", "GSK");
            Security fgp = new Security("FGP Systems", "FGP");
            Security ibe = new Security("Iberdrola", "IBE");
            Security tsla = new Security("Tesla", "TSLA");
            Security aapl = new Security("Apple", "AAPL");

            securities = new List<Security>();
            securities.Add(gsk);
            securities.Add(fgp);
            securities.Add(ibe);
            securities.Add(tsla);
            securities.Add(aapl);

            DropRebuildOldSecurity.ItemsSource = this.report.GetSecurities();
        }

        private static DateOnly ParseDate(string dateStr)
        {
            var yymmdd = dateStr.Split('/');
            int year = int.Parse(yymmdd[0]);
            int month = int.Parse(yymmdd[1]);
            int day = int.Parse(yymmdd[2]);

            return new DateOnly(year, month, day);
        }

        private void BtnRebuildCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void DropRebuildOldSecurity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DropRebuildNewSecurity.Items.Clear();

            var selected = DropRebuildOldSecurity.SelectedItem;

            foreach (var sec in securities)
            {
                if (!sec.Equals(selected))
                {
                    DropRebuildNewSecurity.Items.Add(sec);
                }
            }
        }

        private void BtnRebuild_Click(object sender, RoutedEventArgs e)
        {
            var userInputDate = ParseDate(TxtRebuildDate.Text);

            var userInputOldSecurity = DropRebuildOldSecurity.SelectedItem as Security;
            var userInputNewSecurity = DropRebuildNewSecurity.SelectedItem as Security;

            var userInputOldSecuirtyReduce = Convert.ToDecimal(TxtRebuildOldQuantityReduce.Text);
            var userInputNewSecuirtyQuantity = Convert.ToDecimal(TxtRebuildNewQuantity.Text);

            Tools.Rebuild rb = new Tools.Rebuild(oldSecurity: userInputOldSecurity, quantityToReduce: userInputOldSecuirtyReduce, newSecurity: userInputNewSecurity, quantityToBuild: userInputNewSecuirtyQuantity, date: userInputDate);

            rb.perform(ref report);

            this.NavigationService.Navigate(new Dashboard(ref report));
        }
    }
}
