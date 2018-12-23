using System;
using System.Collections.Generic;
using System.Data;
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

namespace Searchly
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public static int loginUserID;
        public static bool loginUser;
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButtonClicked(object sender, RoutedEventArgs e)
        {
            EmailTextBox.Text.ToLower();
            if (EmailTextBox.Text == "" || PasswordTextBox.Password == "")
            {
                MessageBox.Show("Please enter your Email or Password \nEmail & Password can't be empty", "Error", MessageBoxButton.OK);
            }
            else
            {
                bool login = new User().login(EmailTextBox.Text, PasswordTextBox.Password);
                if (login)
                {
                    loginUser = true;
                    SignUp.signUpUser = false;
                    DataTable dt = DatabaseHandler.Select("Users", new string[] { "userID" }, $"WHERE email = '{EmailTextBox.Text}';");
                    loginUserID = int.Parse(dt.Rows[0][0].ToString());
                    NavigationService.Navigate(new Uri("Home.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MessageBox.Show("Please SignUp \nthe email you entered isn't registered", "Error", MessageBoxButton.OK);
                }
            }
        }

        private void SignUpClicked(object sender, RoutedEventArgs e)
        {            
            loginUser = false;
            NavigationService.Navigate(new Uri("SignUp.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
