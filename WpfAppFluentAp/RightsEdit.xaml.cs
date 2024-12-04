using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cafe.Model;
using Login_to_the_program__Admin__User_;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfAppFluentAp
{
    /// <summary>
    /// Interaction logic for RightsEdit.xaml
    /// </summary>
    public partial class RightsEdit : Window
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public int SelectedObjectId { get; set; } = -1;

        public RightsEdit()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<RoleChecked> TypeRoles { get; set; } = new ObservableCollection<RoleChecked>
        {
            new RoleChecked { Role = Role.User, IsChecked = false },
            new RoleChecked { Role = Role.Manager, IsChecked = false },
            new RoleChecked { Role = Role.Admin, IsChecked = false }
        };

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new CafeDbContext())
            {
                Users = new ObservableCollection<User>(context.Users.ToList());
                UserDataGrid.ItemsSource = Users;
            }
        }

        private void dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID" || e.Column.Header.ToString() == "Name" || e.Column.Header.ToString() == "Birthday") 
            {
                e.Cancel = true; 
            }
        }
        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedObjectId = (UserDataGrid.SelectedItem as User).Id;

            if (SelectedObjectId != null)
            {
                using (var context = new CafeDbContext())
                {
                    var userRoles = context.UserRole
                   .Where(ur => ur.WaiterId == SelectedObjectId)
                   .Select(ur => ur.RoleId)
                   .ToList();

                    for (int i = 0; i < TypeRoles.Count; i++) 
                    {
                        if (userRoles.Contains(TypeRoles[i].Role.Id))
                        {
                            TypeRoles[i].IsChecked = true;
                        }
                        else if (!userRoles.Contains(TypeRoles[i].Role.Id))
                        {
                            TypeRoles[i].IsChecked = false;
                        }
                    }
                }
            }
        }


        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (
                (RoleComboBox.SelectedItem as RoleChecked == null || SelectedObjectId == -1)
                || (RoleComboBox.SelectedItem as RoleChecked == null && SelectedObjectId == -1)
                )
            {
                return;
            }

            var selectedRoleId = (RoleComboBox.SelectedItem as RoleChecked).Role.Id;

            using (var context = new CafeDbContext())
            {
                var userRoles = context.UserRole
                    .Where(ur => ur.WaiterId == SelectedObjectId)
                    .Select(ur => ur.RoleId)
                    .ToList();


                if (userRoles.Contains(selectedRoleId))
                {
                    var userRoleToRemove = context.UserRole
                        .SingleOrDefault(ur => ur.WaiterId == SelectedObjectId && ur.RoleId == selectedRoleId);
                    var i = context.UserRole
                           .Count(ur => ur.RoleId == Role.Admin.Id);
                    if (userRoleToRemove != null)
                    {
                        if (selectedRoleId == Role.Admin.Id && context.UserRole
                           .Count(ur => ur.RoleId == Role.Admin.Id) <= 1)
                        {
                            MessageBox.Show("You can't delete the last user with Admin rights", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            RoleComboBox.SelectedItem = null; 
                            return;
                        }
                        context.UserRole.Remove(userRoleToRemove);

                        TypeRoles.SingleOrDefault(ur => ur.Role.Id == selectedRoleId).IsChecked = false; 
                    }
                }
                else
                {
                    context.UserRole.Add(new UserRole { WaiterId = SelectedObjectId, RoleId = selectedRoleId });

                    TypeRoles.SingleOrDefault(ur => ur.Role.Id == selectedRoleId).IsChecked = true; 
                }

                context.SaveChanges();
            }
            RoleComboBox.SelectedItem = null; 
        }
    }
}
