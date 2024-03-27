using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class Sizes : Window
    {
        MainWindow window = new MainWindow();
        public Sizes()
        {
            InitializeComponent();
            SizesGrid.ItemsSource = MainWindow.tables.Sizes.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void SizesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SizesGrid.SelectedItem as DataRowView != null)
            {
                SizeTbx.Text = (SizesGrid.SelectedItem as DataRowView)["Size"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, new string[1] {SizeTbx.Text }))
            {
                MainWindow.tables.Sizes.InsertSizes(SizeTbx.Text);
                SizesGrid.ItemsSource = MainWindow.tables.Sizes.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, new string[1] { SizeTbx.Text }))
            {
                MainWindow.tables.Sizes.UpdateSizes(SizeTbx.Text,
                    Convert.ToInt32((SizesGrid.SelectedItem as DataRowView)["ID_Material"]));
                SizesGrid.ItemsSource = MainWindow.tables.Sizes.GetData();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(SizesGrid.SelectedItem))
            {
                MainWindow.tables.Sizes.DeleteSizes(
                    Convert.ToInt32((SizesGrid.SelectedItem as DataRowView)["ID_Material"]));
                SizesGrid.ItemsSource = MainWindow.tables.Sizes.GetData();
            }
        }
    }

}
