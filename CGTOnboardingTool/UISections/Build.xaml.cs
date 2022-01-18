using CGTOnboardingTool.Securities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.UISections
{
    /// <summary>
    /// Interaction logic for Build.xaml
    /// </summary>
    public partial class Build : Page
    {
        public Report report;

        private static DateOnly ParseDate(string dateStr)
        {
            var yymmdd = dateStr.Split('/');
            int year = int.Parse(yymmdd[0]);
            int month = int.Parse(yymmdd[1]);
            int day = int.Parse(yymmdd[2]);

            return new DateOnly(year, month, day);
        }

        public Build(ref Report report)
        {
            InitializeComponent();
            this.report = report;

            Security gsk = new Security("GlaxoSmithKline", "GSK");
            Security fgp = new Security("FGP Systems", "FGP");
            Security ibe = new Security("Iberdrola", "IBE");
            Security tsla = new Security("Tesla", "TSLA");
            Security aapl = new Security("Apple", "AAPL");

            List<Security> securities = new List<Security>();
            securities.Add(gsk);
            securities.Add(fgp);
            securities.Add(ibe);
            securities.Add(tsla);
            securities.Add(aapl);

            DropBuildSecurities.ItemsSource = securities;
            DropBuildSecurities.Text = "Select a Security to Build";
        }

        private void BtnBuildCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Dashboard(ref report));
        }

        private void BtnBuildComplete_Click(object sender, RoutedEventArgs e)
        {
            var userInputSecurity = DropBuildSecurities.SelectedItem as Security;
            var userInputDate = ParseDate(TxtBuildDate.Text);
            var userInputQuantity = Convert.ToDecimal(TxtBuildQuantity.Text);
            var userInputPrice = Convert.ToDecimal(TxtBuildPrice.Text);
            var userInputCost = Convert.ToDecimal(TxtBuildCost.Text);

            Tools.Build b = new Tools.Build(security: userInputSecurity, quantity: userInputQuantity, pps: userInputPrice, cost: userInputCost, date: userInputDate);

            b.perform(ref report);

            this.NavigationService.Navigate(new Dashboard(ref report));
        }
    }
}
