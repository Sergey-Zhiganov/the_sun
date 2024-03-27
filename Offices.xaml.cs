using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class Offices : Window
    {
        MainWindow window = new MainWindow();
        public Offices()
        {
            InitializeComponent();
            OfficesGrid.ItemsSource = MainWindow.tables.Offices.GetData(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void OfficesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OfficesGrid.SelectedItem as DataRowView != null)
            {
                TitleTbx.Text = (OfficesGrid.SelectedItem as DataRowView)["Title"].ToString();
                PlaceTbx.Text = (OfficesGrid.SelectedItem as DataRowView)["Place"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, new string[1] { TitleTbx.Text }))
            {
                MainWindow.tables.Offices.InsertOffices(TitleTbx.Text, PlaceTbx.Text);
                OfficesGrid.ItemsSource = MainWindow.tables.Offices.GetData(0);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(OfficesGrid.SelectedItem, new string[1] { TitleTbx.Text }))
            {
                MainWindow.tables.Offices.UpdateOffices(TitleTbx.Text, PlaceTbx.Text,
                    Convert.ToInt32((OfficesGrid.SelectedItem as DataRowView)["ID_Office"]));
                OfficesGrid.ItemsSource = MainWindow.tables.Offices.GetData(0);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(OfficesGrid.SelectedItem))
            {
                MainWindow.tables.Offices.DeleteOffices(
                    Convert.ToInt32((OfficesGrid.SelectedItem as DataRowView)["ID_Office"]));
                OfficesGrid.ItemsSource = MainWindow.tables.Offices.GetData(0);
            }
        }
    }
}