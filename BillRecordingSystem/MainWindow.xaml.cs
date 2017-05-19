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
namespace BillRecordingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    //створити binding list до listview 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetFromToPickers();
            SetListExpances();

        }

        private void SetFromToPickers()
        {
            if (Queries.CheckIfExpencesEmpty(UserInfo.UserId))
            {
                DateTime EarliestExpenceDate = Queries.GetDateOfEarliestExpence(UserInfo.UserId);
                DateTime LatestExpenceDate = Queries.GetDateOfLatestExpence(UserInfo.UserId);

                dateFrom.SelectedDate = EarliestExpenceDate;
                dateTo.SelectedDate = LatestExpenceDate;
            }
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

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            SetListExpances();
        }
    }
}
