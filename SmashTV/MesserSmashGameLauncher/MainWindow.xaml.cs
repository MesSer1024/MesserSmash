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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var foo = loginControl;
            foo.LoginClicked += new LoginControl.VoidDelegate(foo_LoginClicked);
            foo.CreateUserClicked += new LoginControl.VoidDelegate(foo_CreateUserClicked);
        }

        void foo_CreateUserClicked() {
            throw new NotImplementedException();
        }

        void foo_LoginClicked() {
            if (isLoggedIn()) {
                contentArea.Children.Remove(loginControl);
                var c = new LogoutControl();
                initializeWithSameValuesAs(c, loginControl);
                contentArea.Children.Add(c);
            }

        }

        private void initializeWithSameValuesAs(UserControl control, UserControl template) {
            control.HorizontalAlignment = template.HorizontalAlignment;
            control.VerticalAlignment = template.VerticalAlignment;
            control.Margin = template.Margin;
        }

        private bool isLoggedIn() {
            return true;
        }

        private void play_Click(object sender, RoutedEventArgs e) {

        }
    }
}
