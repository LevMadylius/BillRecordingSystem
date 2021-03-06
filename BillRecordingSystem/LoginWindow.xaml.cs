﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserInfo.UserId = Queries.GetUserId(LoginBox.Text, Encryptor.MD5Hash(passwordBox.Password));
                UserInfo.LoginId = Queries.GetLoginIdByUserId(UserInfo.UserId);
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Wrong username or password. Please, try again");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
