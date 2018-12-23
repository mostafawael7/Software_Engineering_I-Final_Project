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
using System.Data;
using System.Text.RegularExpressions;

namespace Searchly
{
    /// <summary>   
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public static int signUpUserID;
        public static bool signUpUser;
        public SignUp()
        {
            InitializeComponent();
        }
        private void SignUpClicked(object sender, RoutedEventArgs e)
        {
            if(FirstNameTextBox.Text!="" || LastNameTextBox.Text!="" || EmailTextBox.Text!="" || MobileNumberTextBox.Text!="" ||
                AgeTextBox.Text!="" || GenderTextBox.Text!="" || PasswordTextBox.Password!="" || ConfirmPasswordTextBox.Password != "")
            {
                if (notContainsAlphabet(AgeTextBox.Text))
                {
                    if (MobileNumberTextBox.Text.Length == 11 && notContainsAlphabet(MobileNumberTextBox.Text))
                    {
                        if (PasswordTextBox.Password == ConfirmPasswordTextBox.Password)
                        {
                            string clause = "WHERE email = '" + EmailTextBox.Text + "';";
                            DataTable dt = DatabaseHandler.Select("Users", clause);
                            if (dt.Rows.Count == 0)
                            {
                                if (new User().addUser(
                                    FirstNameTextBox.Text,
                                    LastNameTextBox.Text,
                                    EmailTextBox.Text,
                                    MobileNumberTextBox.Text,
                                    GenderTextBox.Text,
                                    int.Parse(AgeTextBox.Text),
                                    PasswordTextBox.Password))
                                {
                                    DataTable dt2 = DatabaseHandler.Select("Users", new string[] { "userID" }, $"WHERE email = '{EmailTextBox.Text}';");
                                    signUpUserID = int.Parse(dt2.Rows[0][0].ToString());
                                    signUpUser = true;
                                    Login.loginUser = false;
                                    MessageBox.Show("Signed up Successfully", "Done!", MessageBoxButton.OK);
                                    NavigationService.Navigate(new Uri("Home.xaml", UriKind.RelativeOrAbsolute));
                                }
                                else
                                    MessageBox.Show("An unknown error occured /nPlease try again", "Error", MessageBoxButton.OK);
                            }
                            else
                                MessageBox.Show("This email already exists, please login", "Error", MessageBoxButton.OK);
                        }
                        else
                            MessageBox.Show("The passwords don't match", "Error", MessageBoxButton.OK);
                    }
                    else
                        MessageBox.Show("Please enter your mobile number correctly", "Error", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Please enter your age correctly", "Error", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Please make sure that you entered all the fields correctly", "Error", MessageBoxButton.OK);
        }
        public static bool notContainsAlphabet(string str)
        {
            Regex reg = new Regex(@"[a-zA-Z]");
            return reg.IsMatch(str);
        }
    }
}

/*
 else
            {
                if (PasswordTextBox.Password == ConfirmPasswordTextBox.Password)
                {
                    int count = DatabaseHandler.Select("Users", "").Rows.Count+1;
                    DatabaseHandler.Insert("Users", new string[] {count.ToString(),FirstNameTextBox.Text,LastNameTextBox.Text,EmailTextBox.Text,
                        MobileNumberTextBox.Text,AgeTextBox.Text,GenderTextBox.Text,PasswordTextBox.Password});
                    MessageBox.Show("Signed up Successfully", "Done!", MessageBoxButton.OK);
                    NavigationService.Navigate(new Uri("Home.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    MessageBox.Show("Please enter the same password", "Error", MessageBoxButton.OK);
                }
            }
     
     */
