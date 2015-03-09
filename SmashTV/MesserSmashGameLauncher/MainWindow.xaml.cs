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
using System.Diagnostics;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace MesserSmashGameLauncher {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private string TargetDirectory;

        [DllImport("msi.dll")]
        public static extern Int32 MsiQueryProductState(string szProduct);

        private static string XNA_MSI_QUERY_CODE =      "{D69C8EDE-BBC5-436B-8E0E-C5A6D311CF4F}"; //xna dev studio 4.0  or vice versa
        private static string XNA_MSI_QUERY_CODE2 =     "{2BFC7AA0-544C-4E3A-8796-67F3BE655BE9}"; //xna redist 4.0 or vice versa
        LogoutControl _logoutControl;
        bool _loggedIn;
        private bool _gameUpdated;
        private string _url;
        private FileInfo _targetfile;
        private string _latestVersion;
        private bool _hasXna;
        private  string _redistUrl;
        private FileInfo _xnaRedistFile;

        public MainWindow() {
            InitializeComponent();
            TargetDirectory = Environment.CurrentDirectory + "/gamedata/";
            var foo = loginControl;
            foo.LoginClicked += new LoginControl.VoidDelegate(foo_LoginClicked);
            foo.CreateUserClicked += new LoginControl.VoidDelegate(foo_CreateUserClicked);
            Model.load();
            loginControl.userName.Text = Model.UserName;
            loginControl.password.Password = Model.Password;
            verifyXnaRedist();
            doCheckGameVersion();
        }

        void verifyXnaRedist() {
            _hasXna = MsiQueryProductState(XNA_MSI_QUERY_CODE) == 5 || MsiQueryProductState(XNA_MSI_QUERY_CODE2) == 5;
        }

        void foo_CreateUserClicked() {
            var user = loginControl.userName.Text;
            var pw = loginControl.password.Password;
            Model.UserName = user;
            Model.Password = pw;

            doCreateUser(user, pw);
        }

        void foo_LoginClicked() {
            var user = loginControl.userName.Text;
            var pw = loginControl.password.Password;
            Model.UserName = user;
            Model.Password = pw;

            doLogin(user, pw);
        }

        private void changeToLoggedInMode() {
            contentArea.Children.Remove(loginControl);
            if (_logoutControl == null) {
                _logoutControl = new LogoutControl();
                WpfUtilities.cloneControlProperties(_logoutControl, loginControl);
                _logoutControl.LogoutClicked += new LogoutControl.VoidDelegate(_logoutControl_LogoutClicked);
                _logoutControl.PlayClicked += new LogoutControl.VoidDelegate(_logoutControl_PlayClicked);
            }
            contentArea.Children.Add(_logoutControl);
            _logoutControl.UserName = Model.UserName;
            rendezvousLoginAndUpdated();
        }

        public void rendezvousLoginAndUpdated() {
            bool value = _loggedIn && _gameUpdated && _hasXna;

            if (!_hasXna) {
                return;
            }

            this.Dispatcher.Invoke((Action)(() => {
                if (_logoutControl != null) {
                    _logoutControl.GameUptoDate = value;
                }
                if (value) {
                    progressControl.progressbar.Value = 100;
                    progressControl.status.Content = "STATUS: Ready to play...";
                    progressControl.version.Content = String.Format("Installed Version: {0}", _latestVersion);
                } else {
                    progressControl.version.Content = String.Format("Installed Version: {1} | Target version: {0}", _latestVersion, defaultIfEmptyOrNull(Model.ClientVersion, "N/A"));
                }
            }));
        }

        private void downloadXnaRedist(string redistUrl) {
            _gameUpdated = false;
            setStatus("Downloading new version...");
            var url = new Uri(redistUrl);
            var s = "./patches" + url.AbsoluteUri.Substring(url.AbsoluteUri.LastIndexOf("/"));
            WebClient wc = new WebClient();
            wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_RedistDownloaded);
            _xnaRedistFile = new FileInfo(s);
            if (!_xnaRedistFile.Directory.Exists) {
                _xnaRedistFile.Directory.Create();
            }
            wc.DownloadFileAsync(url, s);
        }

        void wc_RedistDownloaded(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            var success = e.Cancelled == false && e.Error == null;
            if (success) {
                Process p = new Process();
                p.StartInfo.FileName = "msiexec";
                p.StartInfo.Arguments = String.Format("/i {0} /Lv* {1} INSTALLLEVEL=200 ALLUSERS=1", _xnaRedistFile.FullName, "./patches/xnaRedistInstallLog.txt");
                p.Exited += new EventHandler(p_Exited);
                p.Start();
            } else {
                setStatus(String.Format("[{0}] Error downloading file...", _url));
                MessageBox.Show(String.Format("Failed to download file ({1}): {0}", e.Error, _url));
            }
        }

        void p_Exited(object sender, EventArgs e) {
            verifyXnaRedist();
            if (_hasXna) {
                rendezvousLoginAndUpdated();
            } else {
                MessageBox.Show("Unable to automatically install xna redist which is required to run this game, please manually install it from /gamedata/patches/_xnafx40redist.msi folder or download it from the web at @http://download.microsoft.com/download/E/C/6/EC68782D-872A-4D58-A8D3-87881995CDD4/XNAGS40_setup.exe");
            }
        }

        private void doCheckGameVersion() {
            var server = new LocalServer(Model.ServerIp);
            var dir = new Dictionary<string, object> {
                {MesserSmashWeb.GAME_VERSION, Model.ClientVersion ?? ""}
            };

            _gameUpdated = false;
            progressControl.progressbar.Value = 0;
            progressControl.status.Content = "STATUS: Checking Game Version!";
            progressControl.version.Content = "Version: " + defaultIfEmptyOrNull(Model.ClientVersion, "N/A");
            server.launcherCheckVersion(onVersionCallback, dir);
        }

        private string defaultIfEmptyOrNull(string s, string def) {
            return s != null && s.Length > 0 ? s : def;
        }

        private void doUpdateGame() {
            _gameUpdated = false;
            setStatus("Downloading new version...");
            var url = new Uri(_url);
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
            if (progressControl != null && progressControl.IsVisible) {
                var value = e.BytesReceived / (float)e.TotalBytesToReceive * 95;

                this.Dispatcher.Invoke((Action)(() => {
                    progressControl.progressbar.Value = value;
                    setStatus("Downloading new version...");
                    progressControl.version.Content = String.Format("Installed Version: {1} | Target version: {0}", _latestVersion, defaultIfEmptyOrNull(Model.ClientVersion, "N/A"));
                }));
            }
        }

        private void setStatus(string s) {
            this.Dispatcher.Invoke((Action)(() => {
                progressControl.status.Content = String.Format("STATUS: {0}", s);
            }));
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {

            var success = e.Cancelled == false && e.Error == null;
            if (success) {
                setStatus("Installing Files...");
                extractFile(_targetfile, TargetDirectory);
            } else {
                setStatus(String.Format("[{0}] Error downloading file...", _url));
                MessageBox.Show(String.Format("Failed to download file ({1}): {0}", e.Error, _url));
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
                Model.ClientVersion = _latestVersion;
                Model.save();
                setStatus("Newest Version Installed...");
                rendezvousLoginAndUpdated();
            }
        }

        private void unrar_ExtractionProgress(object sender, ExtractionProgressEventArgs e) {
            var value = 95 + e.PercentComplete / 100 * (100-95);
            this.Dispatcher.Invoke((Action)(() => {
                progressControl.progressbar.Value = value;
                progressControl.status.Content = "STATUS: Installing...";
            }));
        }

        private void onVersionCallback(int rc, string data) {
            switch ((ReturnCodes)rc) {
                case ReturnCodes.OK:
                    Model.Online = true;
                    try {
                        var tbl = MesserSmashWeb.toObject(data);
                        _latestVersion = tbl[MesserSmashWeb.GAME_VERSION].ToString();
                        _gameUpdated = _latestVersion == Model.ClientVersion;
                        _url = tbl[MesserSmashWeb.GAME_VERSION_LATEST_URL].ToString();
                        _redistUrl = tbl[MesserSmashWeb.XNA_REDIST_URL].ToString();
                        if(_gameUpdated)  {
                            setStatus("No Update Needed");
                        }
                    } catch (System.Exception ex) {
                        MessageBox.Show(String.Format("Error parsing login request data={0}", ex.ToString()));
                    }
                    break;
                case ReturnCodes.TIMEOUT:
                    Model.Online = false;
                    break;

            }

            if (!_hasXna && _redistUrl != null) {
                downloadXnaRedist(_redistUrl);
            }

            rendezvousLoginAndUpdated();
            if (Model.Online && !_gameUpdated) {
                doUpdateGame();
            }
        }

        private void doCreateUser(string user, string pw) {
            _loggedIn = false;
            var server = new LocalServer(Model.ServerIp);
            var dir = new Dictionary<string, object> {
                {MesserSmashWeb.USER_NAME, user},
                {MesserSmashWeb.LAUNCHER_PASSWORD, pw}
            };

            server.launcherCreateUser(onLoginCallback, dir);
        }

        private void doLogin(string user, string pw) {
            _loggedIn = false;
            var server = new LocalServer(Model.ServerIp);
            var dir = new Dictionary<string, object> {
                {MesserSmashWeb.USER_NAME, user},
                {MesserSmashWeb.LAUNCHER_PASSWORD, pw}
            };
            server.launcherLogin(onLoginCallback, dir);
        }

        private void onLoginCallback(int rc, string data) {
            if (Model.Online == false && rc != (int)ReturnCodes.TIMEOUT) {
                Model.Online = true;
                if (!_gameUpdated) {
                    doCheckGameVersion();
                }
            }

            switch ((ReturnCodes)rc) {
                case ReturnCodes.OK:
                    try {
                        var tbl = MesserSmashWeb.toObject(data);
                        var userid = tbl[MesserSmashWeb.USER_ID].ToString();
                        var sessionid = tbl[MesserSmashWeb.VERIFIED_LOGIN_SESSION].ToString();
                        Model.UserId = userid;
                        Model.Token = sessionid;
                        Model.save();
                        _loggedIn = true;
                    } catch (System.Exception ex) {
                        MessageBox.Show(String.Format("Error parsing login request data={0}", ex.ToString()));
                    }
                    break;
                case ReturnCodes.TIMEOUT:
                    Model.Online = false;
                    MessageBox.Show(String.Format("[Error:{0}] No server response, check internet connection", rc));
                    break;
                case ReturnCodes.USER_EXISTS:
                    MessageBox.Show("User Already Exists!");
                    break;
                default:
                    MessageBox.Show(String.Format("[Error:{0}] Invalid username or password", rc));
                    break;
            }

            if (isLoggedIn()) {
                changeToLoggedInMode();
            }
        }

        void _logoutControl_LogoutClicked() {
            _loggedIn = false;
            contentArea.Children.Remove(_logoutControl);
            contentArea.Children.Add(loginControl);
        }

        private bool isLoggedIn() {
            return _loggedIn;
        }

        void _logoutControl_PlayClicked() {
            var fi = new FileInfo("./gamedata/messersmash.exe");
            if (fi.Exists) {
                var psi = new ProcessStartInfo();
                psi.EnvironmentVariables[MesserSmashWeb.ENVIRONMENT_LAUNCHED_FROM_LAUNCHER] = true.ToString();
                psi.EnvironmentVariables[MesserSmashWeb.USER_NAME] = Model.UserName;
                psi.EnvironmentVariables[MesserSmashWeb.USER_ID] = Model.UserId;
                psi.EnvironmentVariables[MesserSmashWeb.VERIFIED_LOGIN_SESSION] = Model.Token;
                psi.EnvironmentVariables[MesserSmashWeb.GAME_VERSION] = Model.ClientVersion;

                psi.UseShellExecute = false;
                psi.WorkingDirectory = fi.Directory.FullName;
                psi.FileName = fi.FullName;
                Process.Start(psi);
            } else {
                MessageBox.Show(String.Format("Could not find the game.exe file expected:[{0}]!", fi.FullName));
            }
        }

    }
}
