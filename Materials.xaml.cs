using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class Materials : Window
    {
        MainWindow window = new MainWindow();
        public Materials()
        {
            InitializeComponent();
            MaterialsGrid.ItemsSource = MainWindow.tables.Materials.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void MaterialsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialsGrid.SelectedItem as DataRowView != null)
            {
                TitleTbx.Text = (MaterialsGrid.SelectedItem as DataRowView)["Title"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, new string[1] { TitleTbx.Text }, null, new object[0]))
            {
                MainWindow.tables.Materials.InsertMaterials(TitleTbx.Text);
                MaterialsGrid.ItemsSource = MainWindow.tables.Materials.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(MaterialsGrid.SelectedItem, new string[1] { TitleTbx.Text }, null, new object[0]))
            {
                MainWindow.tables.Materials.UpdateMaterials(TitleTbx.Text,
                    Convert.ToInt32((MaterialsGrid.SelectedItem as DataRowView)["ID_Material"]));
                MaterialsGrid.ItemsSource = MainWindow.tables.Materials.GetData();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(MaterialsGrid.SelectedItem))
            {
                MainWindow.tables.Materials.DeleteMaterials(
                    Convert.ToInt32((MaterialsGrid.SelectedItem as DataRowView)["ID_Material"]));
                MaterialsGrid.ItemsSource = MainWindow.tables.Materials.GetData();
            }
        }
    }
}