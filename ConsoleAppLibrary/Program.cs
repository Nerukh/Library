using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleAppLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LibraryContext())
            {

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
                    var books = GetBooksByReader(context, reader1);
                    Console.WriteLine($"\nBooks borrowed by '{reader1.Name}':");
                    foreach (var book in books)
                    {
                        Console.WriteLine(book.Name);
                    }
                }
            }
        }

        static List<Reader> GetReadersByBook(LibraryContext context, Books book)
        {
            return context.ReaderBooks
                .Where(rb => rb.BookId == book.Id)
                .Include(rb => rb.Reader)
                .Select(rb => rb.Reader)
                .ToList();
        }

        // Функція для отримання книг по читачу
        static List<Books> GetBooksByReader(LibraryContext context, Reader reader)
        {
            return context.ReaderBooks
                .Where(rb => rb.ReaderId == reader.Id)
                .Include(rb => rb.Book)
                .Select(rb => rb.Book)
                .ToList();
        }
    }
}