using Cafe.Models;
using EFCore_Demo;

Console.WriteLine("Hello, World!");

using (var context = new CafeDbContext())
{
    var waiters = context.Waiters.Where(s => s.Name.Length < 5).ToArray();


    foreach (var waiter in waiters)
    {
        Console.WriteLine($"{waiter.Id} {waiter.Name} {waiter.Password} {waiter.Birthday}");
    }

    var w = waiters.FirstOrDefault();
    if (w != null)
    {
        w.Birthday = DateTime.Now;
        //context.SaveChanges();
    }
    var l = waiters.LastOrDefault();
    if (l != null)
    {
        context.Waiters.Remove(l);
        //context.SaveChanges();
    }
    context.SaveChanges();
}