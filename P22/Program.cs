// See https://aka.ms/new-console-template for more information

using Cafe.Model;
using System.Data;

Console.WriteLine("Hello, World!");

// NET 2.0,3.0,4.0,4.8- несумісні (5.0,6.0,7.0,8.0)
// Net Core 1.0,2.0,3.0,3.1,3.2 (5.0,6.0,7.0,8.0)

var str = File.ReadAllText("C:/Users/t3809/Downloads/P22-master/MOCK_DATA.json");
var data = System.Text.Json.JsonSerializer.Deserialize<Waiter[]>(str);



using (IDbConnection dbConnection = new System.Data.SQLite.SQLiteConnection("DataSource=C:/Users/t3809/Downloads/P22-master/cafe.db"))
{
    dbConnection.Open();

    var tr = dbConnection.BeginTransaction();
    // var dbCommand = dbConnection.CreateCommand();
    //dbCommand.Transaction = tr;
    //dbCommand.CommandText = "insert into Waiters (name,password,birthday) values (@name, @password,@birthday)";
    //var parName = dbCommand.CreateParameter();
    //parName.ParameterName = "name";
    //var parPassword = dbCommand.CreateParameter();
    //parPassword.ParameterName = "password";
    //var parBirthday = dbCommand.CreateParameter();
    //parBirthday.ParameterName = "birthday";
    try
    {
        foreach (var item in data)
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.Transaction = tr;

            dbCommand.CommandText = "insert into Waiters (name,password,birthday) values (@name, @password,@birthday)";
            var parName = dbCommand.CreateParameter();
            parName.ParameterName = "name";
            var parPassword = dbCommand.CreateParameter();
            parPassword.ParameterName = "password";
            var parBirthday = dbCommand.CreateParameter();
            parBirthday.ParameterName = "birthday";

            parName.Value = item.Name;
            parPassword.Value = item.Password;
            parBirthday.Value = item.Birthday;
            dbCommand.Parameters.Add(parName);
            dbCommand.Parameters.Add(parPassword);
            dbCommand.Parameters.Add(parBirthday);
            dbCommand.ExecuteNonQuery();
        }
        tr.Commit();
        Console.WriteLine("Commit");
    }
    catch (Exception ex)
    {
        tr.Rollback();
        Console.WriteLine($"Error:{ex.Message} Rollback");
    }




    var dbCommand1 = dbConnection.CreateCommand();
    dbCommand1.CommandText = "select * from Waiters";
    var dbReader = dbCommand1.ExecuteReader();
    while (dbReader.Read())
    {
        Console.WriteLine($"{dbReader.GetInt32(0)} {dbReader.GetString(1)} {dbReader.GetString(2)} {dbReader.GetString(3)}");
    }


}
