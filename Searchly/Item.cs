using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Searchly
{
    class Item
    {
        public int userID, itemID;
        public string name { get; set; }
        //public System.Drawing.Image pictureFromFile { get; set; }
        public string category { get; set; }
        //public System.Windows.Media.Imaging.BitmapImage pictureBitmap { get; set; }
        public string pictureString { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string location { get; set; }
        public Item()
        {
            Console.WriteLine("Default Constructor /nDo Nothing");
        }
        public bool addItem(string name,string category, string picture,string description, string date,string location)
        {
            itemID = DatabaseHandler.Select("Item", "").Rows.Count + 1;
            this.name = name;
            this.category = category;
            this.pictureString = picture;
            this.description = description;
            this.date = date;
            this.location = location;
            if (Login.loginUser)
                userID = Login.loginUserID;
            if (SignUp.signUpUser)
                userID = SignUp.signUpUserID;
            bool success;
            try
            {
                DatabaseHandler.Insert("Item", new string[]
                {
                    itemID.ToString(),
                    name,
                    category,
                    picture,
                    description,
                    date,
                    location,
                    userID.ToString()
                });
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown");
                success = false;
                throw e;
            }
            if (success)
                return true;
            else
                return false;
        }
        public List<Item> search(string name)
        {
            List<Item> list = new List<Item>();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CBUACSI\\SQLEXPRESS;Initial Catalog=Searchly;Integrated Security=True");
            connection.Open();
            SqlCommand cmd = new SqlCommand($"SELECT name,category,picture,description,date,location FROM Item WHERE name = '{name}';", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Item
                {
                    name = reader.GetValue(0).ToString(),
                    category = reader.GetValue(1).ToString(),
                    pictureString = reader.GetValue(2).ToString(),
                    description = reader.GetValue(3).ToString(),
                    date = reader.GetValue(4).ToString(),
                    location = reader.GetValue(5).ToString()
                });
                //imageList.Items.Add(new Item
                //{
                //    pictureBitmap = convertToImage("C:\\Users\\Mostafa\\Desktop\\ACELogoFinalFinallFinal1234.png")
                //    //convertToImage((reader.GetValue(2).ToString().Replace(@"\",@"\\")))
                //});
            }
            reader.Close();
            connection.Close();
            return list;
        }
        public List<Item> listItems()
        {
            List<Item> list = new List<Item>();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CBUACSI\\SQLEXPRESS;Initial Catalog=Searchly;Integrated Security=True");
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT name,category,picture,description,date,location FROM Item;", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Item
                {
                    name = reader.GetValue(0).ToString(),
                    category = reader.GetValue(1).ToString(),
                    pictureString = reader.GetValue(2).ToString(),
                    description = reader.GetValue(3).ToString(),
                    date = reader.GetValue(4).ToString(),
                    location = reader.GetValue(5).ToString()
                });
                //imageList.Items.Add(new Item
                //{
                //    pictureBitmap = convertToImage("C:\\Users\\Mostafa\\Desktop\\ACELogoFinalFinallFinal1234.png")
                //    //convertToImage((reader.GetValue(2).ToString().Replace(@"\",@"\\")))
                //});
            }
            reader.Close();
            connection.Close();
            return list;
        }
    }
}



