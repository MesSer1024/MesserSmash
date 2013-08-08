﻿using System;
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

namespace MesserSmashGameLauncher {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private string TargetDirectory;

        LogoutControl _logoutControl;
        bool _loggedIn;
        private bool _gameUpdated;
        private string _url;
        private FileInfo _targetfile;
        private string _latestVersion;

        public MainWindow() {
            InitializeComponent();
            TargetDirectory = Environment.CurrentDirectory + "/gamedata/";
            var foo = loginControl;
            foo.LoginClicked += new LoginControl.VoidDelegate(foo_LoginClicked);
            foo.CreateUserClicked += new LoginControl.VoidDelegate(foo_CreateUserClicked);
            Model.load();
            loginControl.userName.Text = Model.UserName;
            loginControl.password.Password = Model.Password;
            doCheckGameVersion();
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
                _logoutControl.HorizontalAlignment = HorizontalAlignment.Left;
                _logoutControl.LogoutClicked += new LogoutControl.VoidDelegate(_logoutControl_LogoutClicked);
                _logoutControl.PlayClicked += new LogoutControl.VoidDelegate(_logoutControl_PlayClicked);
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
                    _logoutControl.progressbar.Value = 100;
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
                {MesserSmashWeb.GAME_VERSION, Model.ClientVersion}
            };

            _gameUpdated = false;
            server.launcherCheckVersion(onVersionCallback, dir);
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
                Model.ClientVersion = _latestVersion;
                Model.save();
                this.Dispatcher.Invoke((Action)(() => {
                    rendezvousLoginAndUpdated();
                }));
            }
        }

        private void unrar_ExtractionProgress(object sender, ExtractionProgressEventArgs e) {
            var value = 95 + e.PercentComplete / 100 * (100-95);
            this.Dispatcher.Invoke((Action)(() => {
                _logoutControl.progressbar.Value = value;
                _logoutControl.status.Content = "STATUS: Installing...";
            }));
        }

        private void onVersionCallback(int rc, string data) {
            Model.Online = rc != MesserWebResponse.RC_TIMEOUT;
            if (rc == MesserWebResponse.RC_OK) {
                try {
                    var tbl = MesserSmashWeb.toObject(data);
                    _latestVersion = tbl[MesserSmashWeb.GAME_VERSION].ToString();
                    _gameUpdated = _latestVersion == Model.ClientVersion;
                    _url = tbl[MesserSmashWeb.GAME_VERSION_LATEST_URL].ToString();
                } catch (System.Exception ex) {
                    MessageBox.Show(String.Format("Error parsing login request data={0}", ex.ToString()));
                }
            } else {
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
            if (Model.Online == false) {
                Model.Online = true;
                if (!_gameUpdated) {
                    doCheckGameVersion();
                }
            }
            if (rc == MesserWebResponse.RC_OK) {
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
            } else {
                if (rc == MesserWebResponse.RC_TIMEOUT) {
                    Model.Online = false;
                    MessageBox.Show(String.Format("[Error:{0}] No server response, check internet connection", rc));
                } else {
                    MessageBox.Show(String.Format("[Error:{0}] Invalid username or password", rc));
                }
                //invalidData();
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
            //var fi = new FileInfo("./gamedata/messersmash.exe");
            var fi = new FileInfo("./gamedata/messerSMASH-prepreprealpha_00007/messersmash.exe");
            if (fi.Exists) {
                var psi = new ProcessStartInfo();
                var dir = new StringDictionary();
                //dir.Add("")
                dir = psi.EnvironmentVariables;
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
                MessageBox.Show("Could not find the exe file!");
            }
        }

    }
}
