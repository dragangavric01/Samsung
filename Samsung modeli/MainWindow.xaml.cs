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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Samsung_modeli {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        List<User> users;

        public MainWindow() {
            users = new List<User>(2) { new User("asdf", "123", UserType.Admin), new User("fdsa", "321", UserType.Guest)};

            InitializeComponent();
        }

        private bool IsInputOk() {
            if (tbUsername.Text.Trim() == "" && pbPassword.Password == "") {
                tbError.Text = "Fields are empty";
                tbError.Visibility = Visibility.Visible;

                return false;
            }

            if (tbUsername.Text.Trim() == "") {
                tbError.Text = "Username field is empty";
                tbError.Visibility = Visibility.Visible;
                return false;
            }

            if (pbPassword.Password == "") {
                tbError.Text = "Password field is empty";
                tbError.Visibility = Visibility.Visible;
                return false;
            }

            return true;
        }

        private void buttonLogIn_Click(object sender, RoutedEventArgs e) {
            tbError.Visibility = Visibility.Hidden;

            if (!IsInputOk()) {
                return;
            }

            foreach (User user in users) {
                if (tbUsername.Text.Trim() == user.Username && pbPassword.Password == user.Password) {
                    if (user.Type == UserType.Admin) {
                        AdminModelsWindow adminModelsWindow = new AdminModelsWindow();
                        adminModelsWindow.Show();
                    } else {
                        GuestModelsWindow guestModelsWindow = new GuestModelsWindow();
                        guestModelsWindow.Show();
                    }

                    this.Close();
                    return;
                }
            }

            tbError.Text = "Username or password is incorrect";
            tbError.Visibility = Visibility.Visible;
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
