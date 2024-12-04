using System;
using System.Collections.Generic;
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

    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
        }
        private void editingDataUsersButton_Click(object sender, RoutedEventArgs e)
        {
            WindowEdit windowedit = new WindowEdit();
            windowedit.ShowDialog();
        }

        //private void rightsEditorButton_Click(object sender, RoutedEventArgs e)
        //{
        //    RightsEditWindow rightsEditWindow = new RightsEditWindow();
        //    rightsEditWindow.ShowDialog();
        //}

    }
}
