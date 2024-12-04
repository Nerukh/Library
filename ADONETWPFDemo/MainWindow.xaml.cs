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

namespace ADONETWPFDemo
{
    public partial class MainWindow : Window
    {
        List<Waiter> waiters;

        public MainWindow()
        {
            InitializeComponent();
            waiters = new List<Waiter>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (IDbConnection dbConnection = new System.Data.SQLite.SQLiteConnection("DataSource=C:/Users/t3809/Downloads/N22-master/cafe.db"))
            {
                dbConnection.Open();
                waiters.Clear();

                RefreshWaitersList();

                dbConnection.Close();

            }

        }

        private void RefreshWaitersList()
        {
            using (IDbConnection dbConnection = new System.Data.SQLite.SQLiteConnection("DataSource=C:/Users/t3809/Downloads/N22-master/cafe.db"))
            {
                dbConnection.Open();
                waiters.Clear();

                var dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = "Select * from Waiters";
                var dbReader = dbCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    waiters.Add(new Waiter
                    {
                        Id = dbReader.GetInt32(0),
                        Name = dbReader.GetString(1),
                        Password = dbReader.GetString(2),
                        Birthday = DateTime.Parse(dbReader.GetString(3))
                    });
                }

                dg.ItemsSource = null;
                dg.ItemsSource = waiters;
                dbConnection.Close();
            }
        }


        private void AddWaiter_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtPassword.Text != "" && txtBirthday.SelectedDate.HasValue)
            {

                using (IDbConnection dbConnection = new System.Data.SQLite.SQLiteConnection("DataSource=C:/Users/t3809/Downloads/N22-master/cafe.db"))
            {
                dbConnection.Open();
                var getIdsCommand = dbConnection.CreateCommand();
                getIdsCommand.CommandText = "Select Id from Waiters order by Id";
                var dbReader = getIdsCommand.ExecuteReader();

                List<int> ids = new List<int>();
                while (dbReader.Read())
                {
                    ids.Add(dbReader.GetInt32(0));
                }
                dbReader.Close();

                int newId = 1; 
                while (ids.Contains(newId))
                {
                    newId++; 
                }

                Waiter newWaiter = new Waiter
                {
                    Id=newId,
                    Name = txtName.Text,
                    Password = txtPassword.Text,
                    Birthday = txtBirthday.SelectedDate.Value
                };

                var dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = "Insert into Waiters (Id, Name, Password, Birthday) VALUES (@Id, @Name, @Password, @Birthday)";

                var paramId = dbCommand.CreateParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = newWaiter.Id;
                dbCommand.Parameters.Add(paramId);

                var paramName = dbCommand.CreateParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = newWaiter.Name;
                dbCommand.Parameters.Add(paramName);

                var paramPassword = dbCommand.CreateParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = newWaiter.Password;
                dbCommand.Parameters.Add(paramPassword);

                var paramBirthday = dbCommand.CreateParameter();
                paramBirthday.ParameterName = "@Birthday";
                paramBirthday.Value = newWaiter.Birthday;
                dbCommand.Parameters.Add(paramBirthday);

                dbCommand.ExecuteNonQuery();
                dbConnection.Close();
            }
            RefreshWaitersList();
            }
        }

        private void DeleteWaiter_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection dbConnection = new System.Data.SQLite.SQLiteConnection("DataSource=C:/Users/t3809/Downloads/N22-master/cafe.db"))
            {
                dbConnection.Open();

                if (dg.SelectedItem is Waiter selectedWaiter)
                {
                    var dbCommand = dbConnection.CreateCommand();
                    dbCommand.CommandText = "Delete from Waiters where Id = @Id";

                    var paramId = dbCommand.CreateParameter();
                    paramId.ParameterName = "@Id";
                    paramId.Value = selectedWaiter.Id;
                    dbCommand.Parameters.Add(paramId);

                    dbCommand.ExecuteNonQuery();
                }
                else
                {

                    var getLastIdCommand = dbConnection.CreateCommand();
                    getLastIdCommand.CommandText = "Select Id from Waiters order by Id DESC LIMIT 1";
                    var lastId = getLastIdCommand.ExecuteScalar();
                    var deleteLastCommand = dbConnection.CreateCommand();
                    deleteLastCommand.CommandText = "Delete from Waiters where Id = @Id";

                    var paramId = deleteLastCommand.CreateParameter();
                    paramId.ParameterName = "@Id";
                    paramId.Value = lastId;
                    deleteLastCommand.Parameters.Add(paramId);

                    deleteLastCommand.ExecuteNonQuery();
                    
                }

                dbConnection.Close();
            }

            RefreshWaitersList();
        }

        private void EditWaiter_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem is Waiter selectedWaiter && txtName.Text != "" && txtPassword.Text !="" && txtBirthday.SelectedDate.HasValue)
            {
                    selectedWaiter.Name = txtName.Text;
                    selectedWaiter.Password = txtPassword.Text; 
                    selectedWaiter.Birthday = txtBirthday.SelectedDate.Value;

                    try
                    {
                        using (IDbConnection dbConnection = new System.Data.SQLite.SQLiteConnection("DataSource=C:/Users/t3809/Downloads/N22-master/cafe.db"))
                        {
                            dbConnection.Open();
                            var dbCommand = dbConnection.CreateCommand();
                            dbCommand.CommandText = "UPDATE Waiters SET Name = @Name, Password = @Password, Birthday = @Birthday WHERE Id = @Id";

                            var paramId = dbCommand.CreateParameter();
                            paramId.ParameterName = "@Id";
                            paramId.Value = selectedWaiter.Id;
                            dbCommand.Parameters.Add(paramId);

                            var paramName = dbCommand.CreateParameter();
                            paramName.ParameterName = "@Name";
                            paramName.Value = selectedWaiter.Name;
                            dbCommand.Parameters.Add(paramName);

                            var paramPassword = dbCommand.CreateParameter();
                            paramPassword.ParameterName = "@Password";
                            paramPassword.Value = selectedWaiter.Password; 
                            dbCommand.Parameters.Add(paramPassword);

                            var paramBirthday = dbCommand.CreateParameter();
                            paramBirthday.ParameterName = "@Birthday";
                            paramBirthday.Value = selectedWaiter.Birthday;
                            dbCommand.Parameters.Add(paramBirthday);


                            dbCommand.ExecuteNonQuery();
                            dbConnection.Close();

                            MessageBox.Show("Waiter details updated successfully!");
                        }
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

