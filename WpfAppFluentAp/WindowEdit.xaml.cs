﻿using Cafe.Model;
using Login_to_the_program__Admin__User_;
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

namespace WpfAppFluentAp
{
    /// <summary>
    /// Interaction logic for WindowEdit.xaml
    /// </summary>
    public partial class WindowEdit : Window
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public User SelectedObject { get; set; }
        public WindowEdit()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new CafeDbContext())
            {
                Users = new ObservableCollection<User>(context.Users.ToList());

                dg.ItemsSource = Users; 
            }
        }


        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SelectedObject = dg.SelectedItem as User;
        }

        private void Button_SaveChanges_Click(object sender, RoutedEventArgs e) 
        {
            using (var context = new CafeDbContext())
            {
                if (Users.LastOrDefault() != null && Users.LastOrDefault().Id == 0)  
                {
                    if (Users.LastOrDefault().Birthday == null) 
                    {
                        Users.LastOrDefault().Birthday = DateTime.Now;
                    }

                    if (Users.LastOrDefault().Name == null || Users.LastOrDefault().Password == null) 
                    {
                        MessageBox.Show("User information cannot be null", "Error", MessageBoxButton.OK);

                        Users.Remove(Users.LastOrDefault());
                        return;
                    }
                    context.Users.Add(Users.LastOrDefault());
                    context.SaveChanges();

                    Users.Add(Users.LastOrDefault());
                    Users.RemoveAt((Users.Count - 2)); 

                    return;
                }

                if (SelectedObject != null) 
                {
                    var currentObject = context.Users.SingleOrDefault(u => u.Id == SelectedObject.Id);  

                    if (currentObject != null)
                    {
                        currentObject.Name = SelectedObject.Name;
                        currentObject.Password = SelectedObject.Password;
                        currentObject.Birthday = SelectedObject.Birthday;

                        context.SaveChanges();
                    }
                }
            }
        }

        private void Button_DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedObject != null)
            {
                using (var context = new CafeDbContext())
                {
                    var waiterToDelete = context.Users.SingleOrDefault<User>(w => w.Id == SelectedObject.Id);

                    var userRoles = context.UserRole
                 .Where(ur => ur.WaiterId == SelectedObject.Id)
                 .Select(ur => ur.RoleId)
                 .ToList();


                    if (waiterToDelete != null)
                    {

                        if (userRoles.Contains(Role.Admin.Id) && context.UserRole.Count(ur => ur.RoleId == Role.Admin.Id) <= 1)
                        {
                            MessageBox.Show("You can't delete the last user with Admin rights", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        context.Users.Remove(waiterToDelete); 
                        context.SaveChanges();

                        Users.Remove(SelectedObject);
                        SelectedObject = null;
                    }
                }
            }
        }

        private void dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id") 
            {
                e.Cancel = true; 
            }
        }
    }
}
