using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class Roles : Window
    {
        public static string access;
        MainWindow window = new MainWindow();
        public Roles()
        {
            InitializeComponent();
            RolesGrid.ItemsSource = MainWindow.tables.Roles.GetData();
            AccessTbx.Text = "0000000000";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void RolesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RolesGrid.SelectedItem as DataRowView != null)
            {
                TitleTbx.Text = (RolesGrid.SelectedItem as DataRowView)["Title"].ToString();
                AccessTbx.Text = (RolesGrid.SelectedItem as DataRowView)["Access"].ToString();
            }
        }

        private void Chose_Access(object sender, RoutedEventArgs e)
        {
            access = AccessTbx.Text;
            AccessDialog accessDialog = new AccessDialog();
            accessDialog.ShowDialog();
            AccessTbx.Text = access;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, new string[1] { TitleTbx.Text }))
            {
                MainWindow.tables.Roles.InsertRoles(TitleTbx.Text, AccessTbx.Text);
                RolesGrid.ItemsSource = MainWindow.tables.Roles.GetData();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(RolesGrid.SelectedItem))
            {
                MainWindow.tables.Roles.DeleteRoles(
                    Convert.ToInt32((RolesGrid.SelectedItem as DataRowView)["ID_Role"]));
                RolesGrid.ItemsSource = MainWindow.tables.Roles.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(RolesGrid.SelectedItem, new string[1] {TitleTbx.Text }))
            {
                MainWindow.tables.Roles.UpdateRoles(TitleTbx.Text, AccessTbx.Text,
                    Convert.ToInt32((RolesGrid.SelectedItem as DataRowView)["ID_Role"]));
                RolesGrid.ItemsSource = MainWindow.tables.Roles.GetData();
            }
        }

    }
}