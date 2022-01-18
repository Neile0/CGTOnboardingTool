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
    /// Interaction logic for Reduce.xaml
    /// </summary>
    public partial class Reduce : Page
    {
        public Report report;

        public Reduce(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            DropReduceSecurities.ItemsSource = this.report.GetSecurities();
        }

        private static DateOnly ParseDate(string dateStr)
        {
            var yymmdd = dateStr.Split('/');
            int year = int.Parse(yymmdd[0]);
            int month = int.Parse(yymmdd[1]);
            int day = int.Parse(yymmdd[2]);

            return new DateOnly(year, month, day);
        }

        private void BtnReduceCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void BtnReduce_Click(object sender, RoutedEventArgs e)
        {
            var userInputSecurity = DropReduceSecurities.SelectedItem as Security;
            var userInputDate = ParseDate(TxtReduceDate.Text);
            var userInputQuantity = Convert.ToDecimal(TxtReduceQuantity.Text);
            var userInputPrice = Convert.ToDecimal(TxtReducePrice.Text);
            var userInputCost = Convert.ToDecimal(TxtReduceCost.Text);

            Tools.Reduce r = new Tools.Reduce(security: userInputSecurity, quantity: userInputQuantity, pps: userInputPrice, cost: userInputCost, date: userInputDate);

            r.perform(ref report);

            this.NavigationService.Navigate(new Dashboard(ref report));
        }
    }
}
