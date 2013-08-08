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
        public event VoidDelegate PlayClicked;

        public LogoutControl() {
            InitializeComponent();
            ContentRoot.DataContext = this;
            GameUptoDate = false;
        }

        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(LogoutControl), new PropertyMetadata("messer_@hotmail.com"));

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (LogoutClicked != null) {
                LogoutClicked.Invoke();
            }
        }

        public bool GameUptoDate {
            set {
                playButton.IsEnabled = value;
            }
        }

        public string UserName {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        private void PlayButtonClicked(object sender, RoutedEventArgs e) {
            if (PlayClicked != null) {
                PlayClicked.Invoke();
            }
        }
    }
}
