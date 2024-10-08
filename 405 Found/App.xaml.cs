using _405Found;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows;

namespace _405_Found
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Corrected connection string with escaped backslashes
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=405_Found;Integrated Security=True;Encrypt=True";

            var optionsBuilder = new DbContextOptionsBuilder<cs_405Found>();
            optionsBuilder.UseSqlServer(connectionString);

            try
            {
                using (var dbContext = new cs_405Found(optionsBuilder.Options))
                {
                    // Ensure the database is created
                    dbContext.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during database creation
                MessageBox.Show($"An error occurred while creating the database: {ex.Message}", "Database Creation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
