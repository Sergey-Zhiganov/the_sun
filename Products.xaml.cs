using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class Products : Window
    {
        MainWindow window = new MainWindow();
        public Products()
        {
            InitializeComponent();
            ProductsGrid.ItemsSource = MainWindow.tables.Products.GetProducts(0);
            WareCbx.ItemsSource = MainWindow.tables.Wares.GetData();
            ServiceCbx.ItemsSource = MainWindow.tables.Agency_Services.GetData();
            OfficeCbx.ItemsSource = MainWindow.tables.Offices.GetData(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void ProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGrid.SelectedItem as DataRowView != null)
            {
                WareCbx.Text = (ProductsGrid.SelectedItem as DataRowView)["Title"].ToString();
                ServiceCbx.Text = (ProductsGrid.SelectedItem as DataRowView)["Title1"].ToString();
                OfficeCbx.Text = (ProductsGrid.SelectedItem as DataRowView)["Title2"].ToString();
                AmountTbx.Text = (ProductsGrid.SelectedItem as DataRowView)["Amount"].ToString();
                PriceTbx.Text = (ProductsGrid.SelectedItem as DataRowView)["Price"].ToString();
            }
        }
        private bool Check(int type)
        {
            if (type == 0)
            {
                if (WareCbx.SelectedItem == ServiceCbx.SelectedItem ||
                WareCbx.SelectedItem != null && ServiceCbx.SelectedItem != null)
                {
                    MessageBox.Show("Нужно выбрать что-то одно: товар или услугу");
                    return false;
                }
            }
            else
            {
                if (WareCbx.SelectedItem != null &&
                    Convert.ToInt32(AmountTbx.Text) < 0)
                {
                    MessageBox.Show("Количество товара не может быть меньше 0");
                    return false;
                }
                else if (Convert.ToInt32(AmountTbx.Text) < -1)
                {
                    MessageBox.Show("Количество доступных услуг не может быть меньше -1 (неограничено)");
                    return false;
                }
            }
            return true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            if (window.BaseCheck(null, null, null, new object[1] {OfficeCbx.SelectedItem},
                new string[2] { AmountTbx.Text, PriceTbx.Text }) && Check(0))
            {
                if (Check(1))
                {
                    MainWindow.tables.Products.InsertProducts(
                        Convert.ToInt32((WareCbx.SelectedItem as DataRowView)["ID_Ware"]),
                        Convert.ToInt32((ServiceCbx.SelectedItem as DataRowView)["ID_Service"]),
                        Convert.ToInt32((OfficeCbx.SelectedItem as DataRowView)["ID_Office"]),
                        Convert.ToInt32(AmountTbx.Text), Convert.ToInt32(PriceTbx.Text));
                    ProductsGrid.ItemsSource = MainWindow.tables.Products.GetProducts(0);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(null, null, null, new object[1] { OfficeCbx.SelectedItem },
                new string[2] { AmountTbx.Text, PriceTbx.Text }) && Check(0))
            {
                if (Check(1))
                {
                    MainWindow.tables.Products.UpdateProducts(
                        Convert.ToInt32((WareCbx.SelectedItem as DataRowView)["ID_Ware"]),
                        Convert.ToInt32((ServiceCbx.SelectedItem as DataRowView)["ID_Service"]),
                        Convert.ToInt32((OfficeCbx.SelectedItem as DataRowView)["ID_Office"]),
                        Convert.ToInt32(AmountTbx.Text), Convert.ToInt32(PriceTbx.Text),
                        Convert.ToInt32((ProductsGrid.SelectedItem as DataRowView)["ID_Product"]));
                    ProductsGrid.ItemsSource = MainWindow.tables.Products.GetProducts(0);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(ProductsGrid.SelectedItem))
            {
                MainWindow.tables.Products.DeleteProducts(
                    Convert.ToInt32((ProductsGrid.SelectedItem as DataRowView)["ID_Product"]));
                ProductsGrid.ItemsSource = MainWindow.tables.Products.GetProducts(0);
            }
        }
    }
}