using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searchly
{
    class User
    {
        private int userID, age;
        private string firstName, lastName, email, mobileNumber, gender, password;
        public User() 
        {
            Console.WriteLine("Default Constructor \nDo Nothing");
        }
        public bool addUser(string firstName, string lastName,string email,string mobileNumber,string gender,int age,string password)
        {
            userID = DatabaseHandler.Select("Users", "").Rows.Count + 1;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.mobileNumber = mobileNumber;
            this.gender = gender;
            this.age = age;
            this.password = password;
            string clause = "WHERE email = '" + email + "';";
            DataTable dt = DatabaseHandler.Select("Users", clause);
            if (dt.Rows.Count == 0)
            {
                try
                {
                    int count = DatabaseHandler.Select("Users", "").Rows.Count + 1;
                    DatabaseHandler.Insert("Users", new string[]
                    {
                    count.ToString(),
                    firstName,
                    lastName,
                    email,
                    mobileNumber.ToString(),
                    age.ToString(),
                    gender.ToString(),
                    password,
                    });
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception thrown");
                    throw e;
                }
            }
            else
                return false;            
        }
        public User getUser(string email,string password)
        {
            User user = new User();
            string clause = "WHERE email = '" + email + "' AND password = '" + password + "';";
            try
            {
                DataTable dt = DatabaseHandler.Select("Users", clause);
                user.userID = int.Parse(dt.Rows[0][0].ToString());
                user.firstName = dt.Rows[0][1].ToString();
                user.lastName = dt.Rows[0][2].ToString();
                user.email = dt.Rows[0][3].ToString();
                user.mobileNumber = dt.Rows[0][4].ToString();
                user.age = int.Parse(dt.Rows[0][5].ToString());
                user.gender = dt.Rows[0][6].ToString();
                user.password = dt.Rows[0][7].ToString();
                return user;
            }
            catch(Exception e)
            {
                Console.WriteLine("Couldn't select User");
                throw e;
            }
        }
        public bool login(string email, string password)
        {
            User user = new User();
            string clause = "WHERE email = '" + email + "' AND password = '" + password + "';";
            DataTable dt = DatabaseHandler.Select("Users", clause);
            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        public int getUserID(string email,string password) 
        {
            string clause = "WHERE email = '" + email + "' AND password = '" + password + "';";
            DataTable dt = DatabaseHandler.Select("Users", clause);
            return (int.Parse(dt.Rows[0][0].ToString()));
        }
    }
}
