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
using SharedSmashResources;
using System.Threading;
using System.IO;
using System.Net;
using Schematrix;

namespace MesserSmashGameLauncher {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private string TargetDirectory;

        LogoutControl _logoutControl;
        bool _waitingLogin;
        bool _loggedIn;
        private bool _gameUpdated;
        private bool _waitingVersion;
        private string _url;
        private FileInfo _targetfile;
        private string _latestVersion;

        public MainWindow() {
            InitializeComponent();
            TargetDirectory = Environment.CurrentDirectory + "/gamedata/";
            var foo = loginControl;
            foo.LoginClicked += new LoginControl.VoidDelegate(foo_LoginClicked);
            foo.CreateUserClicked += new LoginControl.VoidDelegate(foo_CreateUserClicked);
            Model.ServerIp = "http://localhost:8801/";
            doCheckGameVersion();
        }

        void foo_CreateUserClicked() {
            var user = loginControl.userName.Text;
            var pw = loginControl.password.Password;
            Model.UserName = user;

            doCreateUser(user, pw);
            if (isLoggedIn()) {
                changeToLoggedInMode();
            }
        }

        void foo_LoginClicked() {
            var user = loginControl.userName.Text;
            var pw = loginControl.password.Password;
            Model.UserName = user;

            doLogin(user, pw);

            if (isLoggedIn()) {
                changeToLoggedInMode();
            }
        }

        private void changeToLoggedInMode() {
            contentArea.Children.Remove(loginControl);
            if (_logoutControl == null) {
                _logoutControl = new LogoutControl();
                WpfUtilities.cloneControlProperties(_logoutControl, loginControl);
                _logoutControl.HorizontalAlignment = HorizontalAlignment.Left;
                _logoutControl.LogoutClicked += new LogoutControl.VoidDelegate(_logoutControl_LogoutClicked);
            }
            contentArea.Children.Add(_logoutControl);
            _logoutControl.UserName = Model.UserName;
            rendezvousLoginAndUpdated();
        }

        public void rendezvousLoginAndUpdated() {
            if (_logoutControl == null)
                return;
            bool value = _loggedIn && _gameUpdated;
            this.Dispatcher.Invoke((Action)(() => {
                _logoutControl.GameUptoDate = value;
                if (value) {
                    _logoutControl.status.Content = "STATUS: Ready to play...";
                    _logoutControl.version.Content = String.Format("GameVersion: {0}", _latestVersion);
                } else {
                    _logoutControl.version.Content = String.Format("Target version: {0}", _latestVersion);
                }
            }));
        }

        private void doCheckGameVersion() {
            var server = new LocalServer(Model.ServerIp);
            var dir = new Dictionary<string, object> {
                {MesserSmashWeb.GAME_VERSION, Model.Version}
            };

            _gameUpdated = false;
            _waitingVersion = true;
            server.launcherCheckVersion(onVersionCallback, dir);
            while (_waitingVersion) {
                Thread.Sleep(0);
            }
            rendezvousLoginAndUpdated();
            if (!_gameUpdated) {
                doUpdateGame();
            }
        }

        private void doUpdateGame() {
            var server = new LocalServer(Model.ServerIp);

            _gameUpdated = false;
            downloadNewestVersion(new Uri(_url));
        }

        public void downloadNewestVersion(Uri url) {
            var s = "./patches" + url.AbsoluteUri.Substring(url.AbsoluteUri.LastIndexOf("/"));
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            _targetfile = new FileInfo(s);
            if (!_targetfile.Directory.Exists) {
                _targetfile.Directory.Create();
            }
            wc.DownloadFileAsync(url, s);
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            if (_logoutControl != null && _logoutControl.IsVisible) {
                var value = e.BytesReceived / (float)e.TotalBytesToReceive * 95;

                this.Dispatcher.Invoke((Action)(() => {
                    _logoutControl.progressbar.Value = value;
                    _logoutControl.status.Content = "STATUS: Downloading new version...";
                    _logoutControl.version.Content = String.Format("Target version: {0}", _latestVersion);
                }));
            }
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            var success = e.Cancelled == false && e.Error == null;
            if (success) {

                extractFile(_targetfile, TargetDirectory);
            }
        }

        private void extractFile(FileInfo file, string destinationPath) {
            bool fileComplete = false;
            using (var unrar = new Unrar()) {
                try {
                    // Set destination path for all files
                    unrar.DestinationPath = destinationPath;
                    unrar.ExtractionProgress += new ExtractionProgressHandler(unrar_ExtractionProgress);
                    unrar.Open(file.FullName, Unrar.OpenMode.Extract);

                    while (unrar.ReadHeader()) {
                        unrar.Extract();
                    }
                    fileComplete = true;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                } finally {
                    unrar.Close();
                }
            }
            if (fileComplete) {
                _gameUpdated = true;
                this.Dispatcher.Invoke((Action)(() => {
                    rendezvousLoginAndUpdated();
                }));
            }
        }

        private void unrar_ExtractionProgress(object sender, ExtractionProgressEventArgs e) {
            var value = 95 + e.PercentComplete / 2;
            this.Dispatcher.Invoke((Action)(() => {
                _logoutControl.progressbar.Value = value;
                _logoutControl.status.Content = "STATUS: Installing...";
            }));
        }


        private void onVersionCallback(int rc, string data) {
            if (rc == MesserWebResponse.RC_OK) {
                try {
                    var tbl = MesserSmashWeb.toObject(data);
                    _latestVersion = tbl[MesserSmashWeb.GAME_VERSION].ToString();
                    _gameUpdated = _latestVersion == Model.Version;
                    _url = tbl[MesserSmashWeb.GAME_VERSION_LATEST_URL].ToString();
                } catch (System.Exception ex) {
                    MessageBox.Show(String.Format("Error parsing login request data={0}", ex.ToString()));
                }
            } else {
                
            }

            _waitingVersion = false;
        }

        private void doCreateUser(string user, string pw) {
            _loggedIn = false;
            var server = new LocalServer(Model.ServerIp);
            var dir = new Dictionary<string, object> {
                {MesserSmashWeb.USER_NAME, user},
                {MesserSmashWeb.LAUNCHER_PASSWORD, pw}
            };
            _waitingLogin = true;
            server.launcherCreateUser(onLoginCallback, dir);
            while (_waitingLogin) {
                Thread.Sleep(0);
            }
        }

        private void doLogin(string user, string pw) {
            _loggedIn = false;
            var server = new LocalServer(Model.ServerIp);
            var dir = new Dictionary<string, object> {
                {MesserSmashWeb.USER_NAME, user},
                {MesserSmashWeb.LAUNCHER_PASSWORD, pw}
            };
            _waitingLogin = true;
            server.launcherLogin(onLoginCallback, dir);
            while (_waitingLogin) {
                Thread.Sleep(0);
            }
        }

        private void onLoginCallback(int rc, string data) {
            if (rc == MesserWebResponse.RC_OK) {
                try {
                    var tbl = MesserSmashWeb.toObject(data);
                    var userid = tbl[MesserSmashWeb.USER_ID].ToString();
                    var sessionid = tbl[MesserSmashWeb.VERIFIED_LOGIN_SESSION].ToString();
                    Model.UserId = userid;
                    Model.Token = sessionid;
                    _loggedIn = true;
                } catch (System.Exception ex) {
                    MessageBox.Show(String.Format("Error parsing login request data={0}", ex.ToString()));
                }
            } else {
                if (rc == MesserWebResponse.RC_TIMEOUT) {
                    MessageBox.Show(String.Format("[Error:{0}] No server response, check internet connection", rc));
                } else {
                    MessageBox.Show(String.Format("[Error:{0}] Invalid username or password", rc));
                }
                //invalidData();
            }

            _waitingLogin = false;
        }

        void _logoutControl_LogoutClicked() {
            _loggedIn = false;
            contentArea.Children.Remove(_logoutControl);
            contentArea.Children.Add(loginControl);
        }

        private bool isLoggedIn() {
            return _loggedIn;
        }

        private void play_Click(object sender, RoutedEventArgs e) {

        }
    }
}
