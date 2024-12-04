using Cafe.Model;
using Microsoft.EntityFrameworkCore;

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
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EFCoreWPF
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshWaitersList();
        }

        private void RefreshWaitersList()
        {
            try
            {
                using (var context = new InfoDbContext())
                {
                    var waiters = context.Waiters.ToList();
                    dg.ItemsSource = waiters;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }

        }


        private void AddWaiter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text) && txtBirthday.SelectedDate.HasValue)
            {
                using (var context = new InfoDbContext())
                {

                    var newWaiter = new Waiter
                    {
                        Name = txtName.Text,
                        Password = txtPassword.Text,
                        Birthday = txtBirthday.SelectedDate.Value
                    };
                    context.Waiters.Add(newWaiter);
                    context.SaveChanges();
                }
                txtName.Text = null;
                txtPassword.Text = null;
                txtBirthday.SelectedDate = default;
                RefreshWaitersList();
            }
        }

        private void DeleteWaiter_Click(object sender, RoutedEventArgs e)
        {
           
                using (var context = new InfoDbContext())
            {
                
                var waiters = context.Waiters.ToArray();
                if (dg.SelectedItem is Waiter selectedWaiter)
                {
                    var waiterToDelete = context.Waiters.FirstOrDefault(w => w.Id == selectedWaiter.Id);
                    if (waiterToDelete != null)
                    {

                            MessageBoxResult ms = MessageBox.Show("do you want to delete the user?", "question", MessageBoxButton.YesNo);                          
                            if (ms == MessageBoxResult.Yes)
                            {
                            context.Waiters.Remove(waiterToDelete);
                            }
                    }
                }
             context.SaveChanges();
            }

            RefreshWaitersList();
        }

        private void EditWaiter_Click(object sender, RoutedEventArgs e)
        {
           
            using (var context = new InfoDbContext())
            {

                var waiters = context.Waiters.ToArray();
                if (dg.SelectedItem is Waiter selectedWaiter)
                {
                    var waiterToEdit = context.Waiters.FirstOrDefault(w => w.Id == selectedWaiter.Id);
                    MessageBoxResult ms = MessageBox.Show("do you want to Edit the user?", "question", MessageBoxButton.YesNo);
                    if (ms == MessageBoxResult.Yes)
                    {
                        if (!string.IsNullOrWhiteSpace(txtName.Text)) { waiterToEdit.Name = txtName.Text; }
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text)) { waiterToEdit.Password = txtPassword.Text; }
                        if (txtBirthday.SelectedDate.HasValue) { waiterToEdit.Birthday = txtBirthday.SelectedDate.Value; }
                    }
                }
                context.SaveChanges();
            }
            txtName.Text = null;
            txtPassword.Text = null;
            txtBirthday.SelectedDate = default;
            RefreshWaitersList();
        }
    }
}