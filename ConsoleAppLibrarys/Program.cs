using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using ConsoleAppLibrarys;
using Microsoft.Extensions.Options;

namespace ConsoleAppLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LibraryContext())
            {
                //var readers = new[]
                //{
                //    new Reader { Name = "John Doe", Street = "123 Main St", Birthday = new DateTime(1990, 5, 15) },
                //    new Reader { Name = "Jane Smith", Street = "456 Oak St", Birthday = new DateTime(1985, 8, 22) },
                //    new Reader { Name = "Alice Johnson", Street = "789 Pine St", Birthday = new DateTime(1992, 3, 10) },
                //    new Reader { Name = "Bob Brown", Street = "321 Elm St", Birthday = new DateTime(1988, 12, 2) },
                //    new Reader { Name = "Charlie White", Street = "654 Maple St", Birthday = new DateTime(1995, 7, 25) }
                //};
                //context.Readers.AddRange(readers);
                //context.SaveChanges();


                //var books = new[]
                //{
                //    new Books { Name = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10, Genre = "Fiction", Pages = 180 },
                //    new Books { Name = "1984", Author = "George Orwell", Price = 15, Genre = "Dystopian", Pages = 328 },
                //    new Books { Name = "Moby Dick", Author = "Herman Melville", Price = 20, Genre = "Adventure", Pages = 720 },
                //    new Books { Name = "Pride and Prejudice", Author = "Jane Austen", Price = 12, Genre = "Romance", Pages = 432 },
                //    new Books { Name = "To Kill a Mockingbird", Author = "Harper Lee", Price = 18, Genre = "Fiction", Pages = 281 }
                //};
                //context.Books.AddRange(books);
                //context.SaveChanges();

                var readers = context.Readers.ToList();
                var books = context.Books.ToList();

                //var readerBooks = new[]
                //{
                //    new ReaderBook { ReaderId = readers[0].Id, BookId = books[3].Id, Status = Option.Bought },
                //    new ReaderBook { ReaderId = readers[2].Id, BookId = books[5].Id, Status = Option.Taken_Away },
                //    new ReaderBook { ReaderId = readers[1].Id, BookId = books[6].Id, Status = Option.Sold_Out },
                //    new ReaderBook { ReaderId = readers[4].Id, BookId = books[2].Id, Status = Option.Brought },
                //    new ReaderBook { ReaderId = readers[3].Id, BookId = books[0].Id, Status = Option.Taken_Away },
                //    new ReaderBook { ReaderId = readers[5].Id, BookId = books[4].Id, Status = Option.Brought }
                //};
                //context.ReaderBooks.AddRange(readerBooks);
                //context.SaveChanges();



                var book1 = context.Books.FirstOrDefault();
                if (book1 != null)
                {
                    var bookReaders = GetReadersByBook(context, book1);
                    Console.WriteLine($"Readers who borrowed '{book1.Name}':");
                    foreach (var reader in bookReaders)
                    {
                        Console.WriteLine(reader.Name);
                    }
                }

                var reader1 = context.Readers.FirstOrDefault();
                if (reader1 != null)
                {
                    var book2 = GetBooksByReader(context, reader1);
                    Console.WriteLine($"\nBooks borrowed by '{reader1.Name}':");
                    foreach (var book in books)
                    {
                        Console.WriteLine(book.Name);
                    }
                }

                var availableBooks = GetBooksAvailableInLibrary(context);
                Console.WriteLine("\nBooks available in the library:");
                foreach (var book in availableBooks)
                {
                    Console.WriteLine($"{book.Name} by {book.Author}");
                }

                var mostExpensiveBook = GetMostExpensiveBook(context);
                if (mostExpensiveBook != null)
                {
                    Console.WriteLine($"\nThe most expensive book is '{mostExpensiveBook.Name}' by {mostExpensiveBook.Author}, priced at {mostExpensiveBook.Price}.");
                }
                else
                {
                    Console.WriteLine("\nNo books found in the library.");
                }
            }
        }

        static List<Reader> GetReadersByBook(LibraryContext context, Book book)
        {
            return context.ReaderBooks
                .Where(rb => rb.BookId == book.Id)
                .Include(rb => rb.Readers)
                .Select(rb => rb.Readers)
                .ToList();
        }

        static List<Book> GetBooksByReader(LibraryContext context, Reader reader)
        {
            return context.ReaderBooks
                .Where(rb => rb.ReaderId == reader.Id)
                .Include(rb => rb.Book)
                .Select(rb => rb.Book)
                .ToList();
        }

        static List<Book> GetBooksAvailableInLibrary(LibraryContext context)
        {
            var books = context.Books.ToList();

            return books
                .Where(book =>
                {
                    var lastReaderBook = context.ReaderBooks
                        .Where(rb => rb.BookId == book.Id)
                        .OrderByDescending(rb => rb.Id)
                        .Take(2)
                        .ToList();
                    return lastReaderBook.Count >= 2 &&
                           (lastReaderBook.First().Status == Option.Bought || lastReaderBook.First().Status == Option.Taken_Away);
                })
                .ToList();
        }

        static Book GetMostExpensiveBook(LibraryContext context)
        {
            return context.Books
                .OrderByDescending(book => book.Price)  
                .FirstOrDefault();  
        }

    }
}