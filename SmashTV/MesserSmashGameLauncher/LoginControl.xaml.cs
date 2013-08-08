using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MesserSmashGameLauncher {
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl {
        public delegate void VoidDelegate();
        public event VoidDelegate LoginClicked;
        public event VoidDelegate CreateUserClicked;

        public LoginControl() {
            InitializeComponent();
            userName.Text = "messer_@hotmail.com";
            password.Password = "team1234";
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (inputIsValid()) {
                if (LoginClicked != null) {
                    LoginClicked.Invoke();
                }
            } else {
                invalidateInputs();
            }
        }

        private void createUser_click(object sender, RoutedEventArgs e) {
            if (inputIsValid()) {
                if (CreateUserClicked != null) {
                    CreateUserClicked.Invoke();
                }
            } else {
                invalidateInputs();
            }
        }

        private void password_lostFocus(object sender, RoutedEventArgs e) {
            if (passwordValid()) {
                password.Background = Brushes.White;
            } else {
                password.Background = Brushes.Red;
            }
        }

        private void username_lostFocus(object sender, RoutedEventArgs e) {
            if (usernameValid()) {
                userName.Background = Brushes.White;
            } else {
                userName.Background = Brushes.Red;
            }
        }

        private bool usernameValid() {
            var s = userName.Text;
            return s.Contains('@') && s.Contains('.') && s.Substring(s.LastIndexOf('.')).Length >= 3;
        }

        private bool passwordValid() {
            var s = password.Password;
            return s.Length > 5;
        }

        private bool inputIsValid() {
            return usernameValid() && passwordValid();
        }

        private void invalidateInputs() {
            password_lostFocus(null, null);
            username_lostFocus(null, null);
        }

        private void changed(object sender, TextChangedEventArgs e) {
            username_lostFocus(null, null);
        }

        private void pwChanged(object sender, RoutedEventArgs e) {
            password_lostFocus(null, null);
        }

        private void onPreviewKey(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Button_Click(null, null);
                //Whatever code you want if enter key is pressed goes here
            }
        }
    }
}
