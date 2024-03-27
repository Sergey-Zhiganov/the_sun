using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class Clients : Window
    {
        MainWindow window = new MainWindow();
        public Clients()
        {
            InitializeComponent();
            ClientsGrid.ItemsSource = MainWindow.tables.Clients.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void ClientsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsGrid.SelectedItem as DataRowView != null)
            {
                PassportTbx.Text = (ClientsGrid.SelectedItem as DataRowView)["Passport"].ToString();
                SurnameTbx.Text = (ClientsGrid.SelectedItem as DataRowView)["Surname"].ToString();
                FirstnameTbx.Text = (ClientsGrid.SelectedItem as DataRowView)["Firstname"].ToString();
                MiddleNameTbx.Text = (ClientsGrid.SelectedItem as DataRowView)["MiddleName"].ToString();
                PhoneTbx.Text = (ClientsGrid.SelectedItem as DataRowView)["Phone"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, new string[1] {PhoneTbx.Text}, new string[3] {SurnameTbx.Text,
                FirstnameTbx.Text, MiddleNameTbx.Text}, null, new string[1] {PassportTbx.Text}))
            {
                MainWindow.tables.Clients.InsertClients(Convert.ToInt32(PassportTbx.Text),
                    SurnameTbx.Text, FirstnameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text);
                ClientsGrid.ItemsSource = MainWindow.tables.Clients.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(ClientsGrid.SelectedItem, new string[1] { PhoneTbx.Text },
                new string[3] {SurnameTbx.Text, FirstnameTbx.Text, MiddleNameTbx.Text}, null,
                new string[1] { PassportTbx.Text }))
            {
                MainWindow.tables.Clients.UpdateClients(
                    Convert.ToInt32(PassportTbx.Text), SurnameTbx.Text,
                    FirstnameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text,
                    Convert.ToInt32((ClientsGrid.SelectedItem as DataRowView)["Passport"]),
                    $"{SurnameTbx.Text} {FirstnameTbx.Text} {MiddleNameTbx.Text}");
                ClientsGrid.ItemsSource = MainWindow.tables.Clients.GetData();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if ( window.BaseCheck(ClientsGrid.SelectedItem))
            {
                MainWindow.tables.Clients.DeleteClients(
                    Convert.ToInt32((ClientsGrid.SelectedItem as DataRowView)["Passport"]));
                ClientsGrid.ItemsSource = MainWindow.tables.Clients.GetData();
            }
        }
    }
}