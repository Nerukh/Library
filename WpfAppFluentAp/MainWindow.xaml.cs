using Login_to_the_program__Admin__User_;
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

namespace WpfAppFluentAp
{
    public partial class MainWindow : Window
    {
        public int attempts { get; set; } = 5;
        public ICommand commandToJoin { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            commandToJoin = new RelayCommand(Join);
        }

        public void Join()
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            using (var context = new CafeDbContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Name == username && u.Password == password);
                if (user != null)
                {
                    var userRoles = context.UserRole
                    .Where(ur => ur.WaiterId == user.Id)
                    .Select(ur => ur.RoleId)
                    .ToArray();

                    if (userRoles.Any(roleId => roleId == 1))
                    {
                        AdminMenu starterMenu = new AdminMenu(); 
                        this.Close();
                        attempts = 5;

                        starterMenu.ShowDialog();
                    }
                    else
                    {
                        Application.Current.Shutdown();
                                                       
                    }
                }
                else
                {
                   
                    if (attempts <= 0)
                    {
                        Application.Current.Shutdown();
                    }
                    ErrorTextBlock.Visibility = Visibility.Visible;
                    ErrorTextBlock.Text = $"The login or password is entered incorrectly, please try again. There are still attempts: {attempts}";
                    attempts--;
                }
            }
        }
    }
}
