using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            //Load Items from database and display them to the list view
            List.Items.Clear();
            //List.Items.Add(new Item().listItems());
            List.ItemsSource = new Item().listItems();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("AddItem.xaml", UriKind.RelativeOrAbsolute));
        }
        public BitmapImage convertToImage(string url)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(url, UriKind.Absolute);
            bitmap.EndInit();
            return bitmap;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text != "Enter the name of the item")
                List.ItemsSource = new Item().search(SearchTextBox.Text);
            else
                List.ItemsSource = new Item().listItems();
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                SearchTextBox.Text = "Enter the name of the item";
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(SearchTextBox.Text== "Enter the name of the item")
            {
                SearchTextBox.Text = "";
            }
        }
    }
}
