using Microsoft.Data.SqlClient;
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
using System.Windows.Shapes;

namespace _405_Found.Tables
{
    /// <summary>
    /// Interaction logic for UserTable.xaml
    /// </summary>
    public partial class UserTable : Window
    {
        public UserTable()
        {
            InitializeComponent();
        }
        private void register(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=405_Found;Integrated Security=True;Encrypt=True");
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Books (BookName, Genre, User_Id, Status) values" +
                "(@BookName,@Genre,@User_Id,@Status)", connection);


            command.ExecuteNonQuery();
        }
    }
   
}
