using System;
using System.Collections.Generic;
using System.Data;
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

namespace up01_01
{
    /// <summary>
    /// Логика взаимодействия для Wares.xaml
    /// </summary>
    public partial class Wares : Window
    {
        MainWindow window = new MainWindow();
        public Wares()
        {
            InitializeComponent();
            WaresGrid.ItemsSource = MainWindow.tables.Wares.GetWares();
            MaterialCbx.ItemsSource = MainWindow.tables.Materials.GetData();
            SizeCbx.ItemsSource = MainWindow.tables.Sizes.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void WaresGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WaresGrid.SelectedItem as DataRowView != null)
            {
                TitleTbx.Text = (WaresGrid.SelectedItem as DataRowView)["Title"].ToString();
                MaterialCbx.Text = (WaresGrid.SelectedItem as DataRowView)["Title1"].ToString();
                SizeCbx.Text = (WaresGrid.SelectedItem as DataRowView)["Size"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(WaresGrid.SelectedItem, new string[1] { TitleTbx.Text }, null,
                new object[2] { MaterialCbx.SelectedItem, SizeCbx.SelectedItem }))
            {
                MainWindow.tables.Wares.InsertWares(TitleTbx.Text,
                Convert.ToInt32((MaterialCbx.SelectedItem as DataRowView)["ID_Material"]),
                Convert.ToInt32((SizeCbx.SelectedItem as DataRowView)["ID_Size"]));
                WaresGrid.ItemsSource = MainWindow.tables.Wares.GetWares();
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(WaresGrid.SelectedItem, new string[1] { TitleTbx.Text }, null,
                new object[2] { MaterialCbx.SelectedItem, MaterialCbx.SelectedItem }))
            {
                MainWindow.tables.Wares.UpdateWares(TitleTbx.Text,
                Convert.ToInt32((MaterialCbx.SelectedItem as DataRowView)["ID_Material"]),
                Convert.ToInt32((SizeCbx.SelectedItem as DataRowView)["ID_Size"]),
                Convert.ToInt32((WaresGrid.SelectedItem as DataRowView)["ID_Ware"]));
                WaresGrid.ItemsSource = MainWindow.tables.Wares.GetWares();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(WaresGrid.SelectedItem))
            {
                MainWindow.tables.Wares.DeleteWares(
                    Convert.ToInt32((WaresGrid.SelectedItem as DataRowView)["ID_Ware"]));
                WaresGrid.ItemsSource = MainWindow.tables.Wares.GetWares();
            }
        }
    }
}
