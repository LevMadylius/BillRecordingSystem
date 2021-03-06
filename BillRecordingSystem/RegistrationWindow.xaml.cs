﻿using BillRecordingSystem.Classes;
using BillRecordingSystem.DB;
using Microsoft.Win32;
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

namespace BillRecordingSystem
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private string imagePath;
        private MainWindow _mainWindow;
        public RegistrationWindow()
        {
            InitializeComponent();
            
            
        }

        public RegistrationWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (_mainWindow == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
        }

        private LoginInfo GetLoginInfoFromData()
        {
            var result = new LoginInfo();

            string loginName;

            if(boxLogin.Text == "" || boxLogin.Text.Length > 50)
            {
                throw new Exception();
            }
            else
            {
                loginName = boxLogin.Text;
            }

            string password;
            if(boxPassword.Password != boxConfirmPassword.Password)
            {
                throw new Exception();
            }
            else
            {
                password = boxPassword.Password;
            }
            if (UserInfo.LoginId != -1)
                result.IdLoginInfo = UserInfo.LoginId;


            result.LoginName = loginName;
            result.Pass = Encryptor.MD5Hash(password);

            return result;
        }

        private Users GetUserFromData()
        {
            var result = new Users();

            string firstName;
            if(boxFirstName.Text == "" || boxFirstName.Text.Length > 50)
            {
                throw new Exception();
            }
            else
            {
                firstName = boxFirstName.Text;
            }

            string lastName;
            if (boxLastName.Text == "" || boxLastName.Text.Length > 50)
            {
                throw new Exception();
            }
            else
            {
                lastName = boxLastName.Text;
            }

            int income;
            if (!Int32.TryParse(boxMonthIncome.Text, out income))
            {
                throw new Exception();
            }

            if(UserInfo.UserId != -1)
                result.IdUser = UserInfo.UserId;

            result.MonthIncome = income;
            result.FirstName = firstName;
            result.LastName = lastName;            

            return result;
        }


        private void SetImagePath(ref Users user)
        {
            user.PictureLink = (imagePath != null)? imagePath : null;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            LoginInfo loginInfo;
            Users user;
            try
            {
                loginInfo = GetLoginInfoFromData();
                user = GetUserFromData();
                SetImagePath(ref user);
                
                Queries.RegisterOrUpdate(user, loginInfo);
                
                UserInfo.UserId = user.IdUser;
                UserInfo.LoginId = loginInfo.IdLoginInfo;

                MessageBox.Show("Successfull.");
                
                if (_mainWindow != null)
                {
                    _mainWindow.UpdateUserInfo();
                }
                else
                {
                    // shows main window after registration completed successfully.
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Incorrect parameters,please try again\r\nError message: " + ex.Message);
            }
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            string strfilename = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";

            if (dialog.ShowDialog(this) == true)
            {
                strfilename = dialog.InitialDirectory + dialog.FileName;
            }

            ProfileImage.Source = new BitmapImage(new Uri(strfilename));
            imagePath = strfilename;
        }
    }
}