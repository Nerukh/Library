// See https://aka.ms/new-console-template for more information
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using Dapper;
using Cafe.Model;
Console.WriteLine("Hello, World!");
using (IDbConnection dbConnection = new System.Data.SQLite.SQLiteConnection("DataSource=C:/Users/t3809/Downloads/P22-master/cafe.db"))
{



    var query = "select * from Waiters";
    var result = dbConnection.Query<Waiter>(query); 
    foreach(Waiter waiter in result)
    {
        Console.WriteLine($"{waiter.Id} {waiter.Name} {waiter.Password} {waiter.Birthday}");
    }

    var dbCommand1 = dbConnection.CreateCommand();
    dbCommand1.CommandText = "select * from Waiters";
    var dbReader = dbCommand1.ExecuteReader();
    while (dbReader.Read())
    {
        Console.WriteLine($"{dbReader.GetInt32(0)} {dbReader.GetString(1)} {dbReader.GetString(2)} {dbReader.GetString(3)}");
    }


}


//dbConnection.Open();

//var tr = dbConnection.BeginTransaction();
// var dbCommand = dbConnection.CreateCommand();
//dbCommand.Transaction = tr;
//dbCommand.CommandText = "insert into Waiters (name,password,birthday) values (@name, @password,@birthday)";
//var parName = dbCommand.CreateParameter();
//parName.ParameterName = "name";
//var parPassword = dbCommand.CreateParameter();
//parPassword.ParameterName = "password";
//var parBirthday = dbCommand.CreateParameter();
//parBirthday.ParameterName = "birthday";
//try
//{
//    foreach (var item in data)
//    {
//        var dbCommand = dbConnection.CreateCommand();
//        dbCommand.Transaction = tr;

//        dbCommand.CommandText = "insert into Waiters (name,password,birthday) values (@name, @password,@birthday)";
//        var parName = dbCommand.CreateParameter();
//        parName.ParameterName = "name";
//        var parPassword = dbCommand.CreateParameter();
//        parPassword.ParameterName = "password";
//        var parBirthday = dbCommand.CreateParameter();
//        parBirthday.ParameterName = "birthday";

//        parName.Value = item.Name;
//        parPassword.Value = item.Password;
//        parBirthday.Value = item.Birthday;
//        dbCommand.Parameters.Add(parName);
//        dbCommand.Parameters.Add(parPassword);
//        dbCommand.Parameters.Add(parBirthday);
//        dbCommand.ExecuteNonQuery();
//    }
//    tr.Commit();
//    Console.WriteLine("Commit");
//}
//catch (Exception ex)
//{
//    tr.Rollback();
//    Console.WriteLine($"Error:{ex.Message} Rollback");
//}