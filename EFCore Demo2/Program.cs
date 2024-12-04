// See https://aka.ms/new-console-template for more information
using Cafe.Model;
using EFCore_Demo2;

Console.WriteLine("Hello, World!");

using (var context = new CafeDbContext())
{
    
    var waiters = context.Waiters.ToArray();

    foreach(var waiter in context.Waiters.ToArray())
    {
        Console.WriteLine($"{waiter.Id} {waiter.Name} {waiter.Password} {waiter.Birthday}");
    }

    var w = waiters.FirstOrDefault();
    if( w != null)
    {
        w.Birthday = DateTime.Now;
    }
    var l = waiters.LastOrDefault();
    if( l != null)
    {
        context.Waiters.Remove(l);
        context.SaveChanges();
    }
}