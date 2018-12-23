using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Page
    {
        public static string imagePath;
        public AddItem()
        {
            InitializeComponent();
        }

        private void BrowseImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ItemImage.Source = new BitmapImage(new Uri(op.FileName));
                imagePath = op.FileName;
            }
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            //System.Drawing.Image i = System.Drawing.Image.FromFile(imagePath);
            //byte[] arr = File.ReadAllBytes(imagePath);
            if (NameTextBox.Text != "" || CategoryComboBox.Text != "" || DescriptionTextBox.Text != "" || DateTextBox.Text != "" || LocationTextBox.Text != "")
            {
                if (CategoryComboBox.Text != "Personal Stuff" || CategoryComboBox.Text != "Mobiles" || CategoryComboBox.Text != "Wallets" || CategoryComboBox.Text != "Keys" || CategoryComboBox.Text != "Other")
                {
                    if (!SignUp.notContainsAlphabet(DateTextBox.Text))
                    {
                        if (new Item().addItem(
                            NameTextBox.Text,
                            CategoryComboBox.Text,
                            imagePath,
                            DescriptionTextBox.Text,
                            DateTextBox.Text,
                            LocationTextBox.Text))
                        {
                            MessageBox.Show("Your item is posted Successfully", "Done!", MessageBoxButton.OK);
                            NavigationService.Navigate(new Uri("Home.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                    else
                        MessageBox.Show("Please Re-Enter the date", "Error", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Choose one of the categories", "Error", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Please make sure that you entered all the fields correctly", "Error", MessageBoxButton.OK);
        }
        //public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        image.
        //        // Convert Image to byte[]
        //        image.Save(ms, format);
        //        byte[] imageBytes = ms.ToArray();

        //        // Convert byte[] to Base64 String
        //        string base64String = Convert.ToBase64String(imageBytes);
        //        return base64String;
        //    }
        //}
        //public static BitmapImage ToWpfImage(this System.Drawing.Image img)
        //{
        //    MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
        //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

        //    BitmapImage ix = new BitmapImage();
        //    ix.BeginInit();
        //    ix.CacheOption = BitmapCacheOption.OnLoad;
        //    ix.StreamSource = ms;
        //    ix.EndInit();
        //    return ix;
        //}
        //public BitmapSource LoadImage(Byte[] imageData)
        //{
        //    using (MemoryStream ms = new MemoryStream(imageData))
        //    {
        //        var decoder = BitmapDecoder.Create(ms,
        //            BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
        //        return decoder.Frames[0];
        //    }
        //}
        //public byte[] imageToByteArray(System.Drawing.Image imageIn)
        //{
        //    ImageConverter ic = new ImageConverter();
        //    return (byte[])ic.ConvertTo(imageIn, typeof(byte[]));
        //}
    }
}
