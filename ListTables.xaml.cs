using System;
using System.Windows;
using System.Windows.Controls;
using static up01_01.up01_01DataSet;

namespace up01_01
{
    public partial class ListTables : Window
    {
        static public WorkersDataTable user;
        public ListTables()
        {
            InitializeComponent();
            Button[] buttons = new Button[10] {MaterialsButton, SizesButton, WiresButton, ServicesButton,
                OfficesButton, ProductsButton, RolesButton, WorkersButton, ClientsButton, OrdersButton };
            string access = user.Rows[0]["Access"].ToString();
            for (int i = 0; i < buttons.Length - 1; i++)
            {
                buttons[i].IsEnabled = Convert.ToBoolean(Convert.ToInt32(access[i]) - 48);
            }
        }

        private void Materials_Click(object sender, RoutedEventArgs e)
        {
            Materials materialsWindow = new Materials();
            materialsWindow.Show();
            Close();
        }

        private void Sizes_Click(object sender, RoutedEventArgs e)
        {
            Sizes sizesWindow = new Sizes();
            sizesWindow.Show();
            Close();
        }

        private void Wires_Click(object sender, RoutedEventArgs e)
        {
            Wares waresWindow = new Wares();
            waresWindow.Show();
            Close();
        }

        private void Services_Click(object sender, RoutedEventArgs e)
        {
            Agency_Services agency_Services = new Agency_Services();
            agency_Services.Show();
            Close();
        }

        private void Offices_Click(object sender, RoutedEventArgs e)
        {
            Offices officesWindow = new Offices();
            officesWindow.Show();
            Close();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            Products productsWindow = new Products();
            productsWindow.Show();
            Close();
        }

        private void Roles_Click(object sender, RoutedEventArgs e)
        {
            Roles roles = new Roles();
            roles.Show();
            Close();
        }

        private void Workers_Click(object sender, RoutedEventArgs e)
        {
            Workers workers = new Workers();
            workers.Show();
            Close();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            Clients clients = new Clients();
            clients.Show();
            Close();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Orders orders = new Orders();
            orders.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}