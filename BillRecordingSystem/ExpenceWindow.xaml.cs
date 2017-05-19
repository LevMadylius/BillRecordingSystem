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
        public ExpenceWindow()
        {
            InitializeComponent();

            btnEdit.Visibility = Visibility.Hidden;
        }

        private Expences GetExpenceFromData()
        {
            var result = new Expences();

            string name;
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

            return result;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Expences expence;
            try
            {
                 expence = GetExpenceFromData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Incorrect parameters,please try again");
            }
        }
    }
}
