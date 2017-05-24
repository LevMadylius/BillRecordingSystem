using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BillRecordingSystem.DB;
using BillRecordingSystem.Classes;
using System.IO;

namespace BillRecordingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetFromToPickers();
            SetListExpances();
            UpdateUserInfo();
            UpdateStatistics();
            SetTotalSpent();
        }

        private void SetFromToPickers()
        {
            if (Queries.CheckIfExpencesEmpty(UserInfo.UserId))
            {
                DateTime EarliestExpenceDate = Queries.GetDateOfEarliestExpence(UserInfo.UserId);
                DateTime LatestExpenceDate = Queries.GetDateOfLatestExpence(UserInfo.UserId);

                dateFrom.SelectedDate = EarliestExpenceDate;
                dateTo.SelectedDate = LatestExpenceDate;

                dateFromStatictics.SelectedDate = EarliestExpenceDate;
                dateToStatictics.SelectedDate = LatestExpenceDate;
            }
        }

        private void SetTotalSpent()
        {
            DateTime EarliestExpenceDate = Queries.GetDateOfEarliestExpence(UserInfo.UserId);
            DateTime LatestExpenceDate = Queries.GetDateOfLatestExpence(UserInfo.UserId);

            int money = StatisticsHelper.GetSpentForPeriod(EarliestExpenceDate, LatestExpenceDate, UserInfo.UserId);

            lblTotalExpence.Content += $" {money}";
        }

        private void SetFromToPickers(DateTime from, DateTime to)
        {
           
            dateFrom.SelectedDate = from;
            dateTo.SelectedDate = to;

            dateFromStatictics.SelectedDate = from;
            dateToStatictics.SelectedDate = to;
        }

        public void UpdateUserInfo()
        {
            var user = Queries.GetUserById(UserInfo.UserId);
            
            lblFullName.Content = $"{user.FirstName} {user.LastName}";
            lblSalary.Content += $" {user.MonthIncome}";
            
            if(File.Exists(user.PictureLink))
                ProfileImage.Source = new BitmapImage(new Uri(user.PictureLink));
        }

        // All expences of current user.
        private async void SetListExpances()
        {
            if (Queries.CheckIfExpencesEmpty(UserInfo.UserId))
            {             
                List<Expences> list = await Task.Run<List<Expences>>(() => Queries.GetListExpances(UserInfo.UserId));
                gridExpences.ItemsSource =  list;
            }
        }

        private void btnAddExpance_Click(object sender, RoutedEventArgs e)
        {
            ExpenceWindow ew = new ExpenceWindow();
            ew.Show();
        }

        private async void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            DateTime dFrom = dateFrom.SelectedDate.Value;
            DateTime dTo = dateTo.SelectedDate.Value;
            if (comboType.Text.Contains("All"))
            {
                
                var list = await Task.Run<List<Expences>>
                    (
                        () => Queries.GetFilteredListExpances(UserInfo.UserId, dFrom, dTo)
                    );
                gridExpences.ItemsSource = list;
            }
            else
            {
                string type = comboType.Text;
                var list = await Task.Run<List<Expences>>
                                    (
                                        () => Queries.GetFilteredListExpances(UserInfo.UserId, dFrom, dTo,type)
                                    );
                gridExpences.ItemsSource = list;

            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            SetListExpances();
            SetFromToPickers();
        }

        private void gridExpences_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    var expence = grid.SelectedItem as Expences;

                    ExpenceWindow expenceWindow = new ExpenceWindow(expence);
                    expenceWindow.Show();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(this);
            registrationWindow.Show();
        }
        

        private void comboBoxTimePeriod_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string condition = comboBoxTimePeriod.Text;

            switch (condition)
            {
                case "Last week":
                    SetFromToPickers(DateHelper.FirstDayWeek(), DateTime.Today);
                    break;
                case "Last month":
                    SetFromToPickers(DateHelper.FirstDayMonth(), DateTime.Today);
                    break;
                case "All time":
                    SetFromToPickers();
                    break;
            }
        }

        private void UpdateStatistics()
        {
            DateTime datefrom = dateFromStatictics.SelectedDate.Value;
            DateTime dateTo = dateToStatictics.SelectedDate.Value;


            try
            {
                var mostExpensive = Queries.GetMostExpencive(UserInfo.UserId, datefrom, dateTo);                

                lblMostExpensiveproduct.Content = $"Most expensive is {mostExpensive.Name}, price: {mostExpensive.MonthAmount}, date: {mostExpensive.BillDate}";
            }
            catch
            {
                lblMostExpensiveproduct.Content = "No expences for this period";
            }

            try
            {
                var mostPopularCategory = Queries.GetMostPopularCategory(UserInfo.UserId, datefrom, dateTo);
                
                lblMostPopularCategory.Content = $"Most popular category is {mostPopularCategory.Name}";
            }
            catch
            {
                lblMostExpensiveproduct.Content = "No expences for this period";
            }

            var spentForPeriod = StatisticsHelper.GetSpentForPeriod(datefrom, dateTo, UserInfo.UserId);

            lblSpentForPeriod.Content = $"Spent money for this period {spentForPeriod}";
        }

        private void btnFilterStatictics_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatistics();
        }

        private void comboBoxTimePeriod_Copy_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string condition = comboBoxTimePeriod_Copy.Text;

            switch (condition)
            {
                case "Last week":
                    SetFromToPickers(DateHelper.FirstDayWeek(), DateTime.Today);
                    break;
                case "Last month":
                    SetFromToPickers(DateHelper.FirstDayMonth(), DateTime.Today);
                    break;
                case "All time":
                    SetFromToPickers();
                    break;
            }
        }
    }
}
