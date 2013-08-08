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
    /// Interaction logic for LogoutControl.xaml
    /// </summary>
    public partial class LogoutControl : UserControl {
        public delegate void VoidDelegate();
        public event VoidDelegate LogoutClicked;

        public LogoutControl() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (LogoutClicked != null) {
                LogoutClicked.Invoke();
            }
        }
    }
}
