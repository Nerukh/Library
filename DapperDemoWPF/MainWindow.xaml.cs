using Dapper;
using Cafe.Model;
using System.Data;
using System.Data.Common;
using System.Reflection;
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
using System.Data.SQLite;

namespace DapperDemoWPF
{
    public partial class MainWindow : Window
    {
        List<Waiter> waiters;
        private readonly string connectionString = "Data Source=C:/Users/t3809/Downloads/P22-master/cafe.db";

        public MainWindow()
        {
            waiters = new List<Waiter>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshWaitersList();
        }

        private void RefreshWaitersList()
        {
            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                waiters = dbConnection.Query<Waiter>("SELECT * FROM waiters").AsList();
                dg.ItemsSource = waiters;
                
            }
        }

        private void AddWaiter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text) && txtBirthday.SelectedDate.HasValue)
            {
                using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
                {
                    var existingIds = dbConnection.Query<int>("SELECT Id FROM Waiters ORDER BY Id").AsList();
                    int newId = 1;
                    while (existingIds.Contains(newId)) newId++;

                    var newWaiter = new Waiter
                    {
                        Id = newId,
                        Name = txtName.Text,
                        Password = txtPassword.Text,
                        Birthday = txtBirthday.SelectedDate.Value
                    };

                    string insertQuery = "INSERT INTO Waiters (Id, Name, Password, Birthday) VALUES (@Id, @Name, @Password, @Birthday)";
                    dbConnection.Execute(insertQuery, newWaiter);
                }
                RefreshWaitersList();
            }
        }

        private void DeleteWaiter_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
            {
                if (dg.SelectedItem is Waiter selectedWaiter)
                {
                    dbConnection.Execute("DELETE FROM Waiters WHERE Id = @Id", new { selectedWaiter.Id });
                }
                else
                {
                    var lastId = dbConnection.ExecuteScalar<int?>("SELECT Id FROM Waiters ORDER BY Id DESC LIMIT 1");
                    if (lastId.HasValue)
                    {
                        dbConnection.Execute("DELETE FROM Waiters WHERE Id = @Id", new { Id = lastId });
                    }
                }
            }
            RefreshWaitersList();
        }

        private void EditWaiter_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem is Waiter selectedWaiter &&
                !string.IsNullOrWhiteSpace(txtName.Text) &&
                !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                txtBirthday.SelectedDate.HasValue)
            {
                selectedWaiter.Name = txtName.Text;
                selectedWaiter.Password = txtPassword.Text;
                selectedWaiter.Birthday = txtBirthday.SelectedDate.Value;

                try
                {
                    using (IDbConnection dbConnection = new SQLiteConnection(connectionString))
                    {
                        string updateQuery = "UPDATE Waiters SET Name = @Name, Password = @Password, Birthday = @Birthday WHERE Id = @Id";
                        dbConnection.Execute(updateQuery, selectedWaiter);
                    }
                    MessageBox.Show("Waiter details updated successfully!");
                    RefreshWaitersList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating waiter: {ex.Message}");
                }
            }
        }
    }
}