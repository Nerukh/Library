using Cafe.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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

namespace WpfAppLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;  // Прив'язка ViewModel до DataContext
        }

        // Зберігає зміни в базі даних
        private void Button_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new LibraryDbContext())
            {
                context.UserBooks.Update(ViewModel.SelectedUserBook);  // Оновлення вибраного UserBook
                context.SaveChanges();
                MessageBox.Show("Changes saved successfully.");
            }
        }

        // Видаляє вибраний рядок
        private void Button_DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new LibraryDbContext())
            {
                context.UserBooks.Remove(ViewModel.SelectedUserBook);  // Видалення вибраного UserBook
                context.SaveChanges();
                ViewModel.LoadUserBooks();  // Оновлення списку UserBook
                MessageBox.Show("Row deleted successfully.");
            }
        }

        private void LoadUserBooks()
        {
            using (var context = new LibraryDbContext())
            {
                var userBooks = context.UserBooks
                    .Include(ub => ub.Reader)
                .Include(ub => ub.Book)
                    .ToList();
            }
        }
    }
}