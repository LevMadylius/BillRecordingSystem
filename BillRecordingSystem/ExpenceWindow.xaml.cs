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
using System.Windows.Shapes;
using BillRecordingSystem.DB;
using BillRecordingSystem.Classes;

namespace BillRecordingSystem
{
    /// <summary>
    /// Interaction logic for ExpenceWindow.xaml
    /// </summary>
    public partial class ExpenceWindow : Window
    {
        private Expences _expence;

        public ExpenceWindow()
        {
            InitializeComponent();

            btnEdit.Visibility = Visibility.Hidden;
        }

        public ExpenceWindow(Expences expence)
        {
            InitializeComponent();

            _expence = expence;

            btnConfirm.Visibility = Visibility.Hidden;
            btnDeny.Visibility = Visibility.Hidden;
            SetIsEnabledFalse();
            SetExpenceFields();
        }

        private void SetIsEnabledFalse()
        {
            boxName.IsEnabled = false;
            boxMoney.IsEnabled = false;
            boxComment.IsEnabled = false;
            DatePicker.IsEnabled = false;
            comboType.IsEnabled = false;
        }
        private void SetIsEnabledTrue()
        {
            boxName.IsEnabled = true;
            boxMoney.IsEnabled = true;
            boxComment.IsEnabled = true;
            DatePicker.IsEnabled = true;
            comboType.IsEnabled = true;
        }

        private Expences GetExpenceFromData()
        {
            var result = new Expences();

            string name;
            //50 is max length
            if (boxName.Text == "" || boxName.Text.Length > 50)
            {
                throw new Exception();
            }
            else
            {
                name = boxName.Text;
            }

            string comment;
            if (boxComment.Text == "" || boxComment.Text.Length > 50)
            {
                throw new Exception();
            }
            else
            {
                comment = boxComment.Text;
            }

            int moneyValue;
            if(!Int32.TryParse(boxMoney.Text,out moneyValue))
            {
                throw new Exception();
            }

            int idType= Queries.GetExpenceTypeId(comboType.Text);          

            DateTime date = DatePicker.SelectedDate.Value.Date;

            result.Name = name;
            result.BillDate = date;
            result.Comment = comment;
            result.MonthAmount = moneyValue;
            result.IdExpenceType = idType;
            result.IdUser = UserInfo.UserId;
            result.IdExpence = (_expence != null) ? _expence.IdExpence : 0;

            return result;
        }

        private void SetExpenceFields()
        {
            boxComment.Text = _expence.Comment;
            boxName.Text = _expence.Name;
            boxMoney.Text = _expence.MonthAmount.ToString();
            DatePicker.Text = _expence.BillDate.ToString();


            comboType.Text = _expence.ExpenceTypes.Name;
        }

        private async void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Expences expence;
            try
            {
                expence = GetExpenceFromData();
                await Task.Run(()=> Queries.InsertExpence(expence));
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Incorrect parameters,please try again");
            }

            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetIsEnabledTrue();

            btnEdit.Visibility = Visibility.Hidden;
            btnConfirm.Visibility = Visibility.Visible;
            btnDeny.Visibility = Visibility.Visible;            
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
