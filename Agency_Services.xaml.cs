using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class Agency_Services : Window
    {
        MainWindow window = new MainWindow();
        public Agency_Services()
        {
            InitializeComponent();
            Agency_ServicesGrid.ItemsSource = MainWindow.tables.Agency_Services.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void Agency_ServicesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Agency_ServicesGrid.SelectedItem as DataRowView != null)
            {
                TitleTbx.Text = (Agency_ServicesGrid.SelectedItem as DataRowView)["Title"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, new string[1] {TitleTbx.Text }))
            {
                MainWindow.tables.Agency_Services.InsertAgency_Services(TitleTbx.Text);
                Agency_ServicesGrid.ItemsSource = MainWindow.tables.Agency_Services.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(Agency_ServicesGrid.SelectedItem, new string[1] { TitleTbx.Text }))
            {
                MainWindow.tables.Agency_Services.UpdateAgency_Services(TitleTbx.Text,
                    Convert.ToInt32((Agency_ServicesGrid.SelectedItem as DataRowView)["ID_Service"]));
                Agency_ServicesGrid.ItemsSource = MainWindow.tables.Agency_Services.GetData();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(Agency_ServicesGrid.SelectedItem))
            {
                MainWindow.tables.Agency_Services.DeleteAgency_Services(
                    Convert.ToInt32((Agency_ServicesGrid.SelectedItem as DataRowView)["ID_Service"]));
                Agency_ServicesGrid.ItemsSource = MainWindow.tables.Agency_Services.GetData();
            }
        }
    }
}