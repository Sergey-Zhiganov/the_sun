using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Data;

namespace up01_01
{
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
            OrdersGrid.ItemsSource = MainWindow.tables.Orders.GetOrders();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }
        public void AddRecipe(int order_id, int total_cost , int received, DataRowCollection rows)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                string filePath = System.IO.Path.Combine(dialog.FileName, $"Чек №{order_id}.txt");
                string recipe = $"\t\t\tПохоронное агенство \"Солнышко\"\n\t\t\tКассовый чек №{order_id}\n";
                foreach (DataRow row in rows)
                {
                    if (row["Title"].ToString() == "")
                    {
                        recipe += $"\n\t{row["Title1"]}\t-\t{row["Price"]}";
                    }
                    else
                    {
                        recipe += $"\n\t{row["Title"]}\t-\t{row["Price"]}";
                    }
                }
                recipe += $"\n\nИтого к оплате: {total_cost}\nВнесено: {received}\nСдача: {received - total_cost}";
                File.WriteAllText(filePath, recipe);
                MessageBox.Show("Чек успешно выгружен");
            }
        }

        private void OrdersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrdersProductsGrid.ItemsSource = MainWindow.tables.OrdersProducts.GetOrderProducts(
                Convert.ToInt32((OrdersGrid.SelectedItem as DataRowView)["ID_Order"]));
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrdersProducts ordersProducts = new OrdersProducts();
            ordersProducts.Show();
            Close();
        }

        private void GetRecipe_Click(object sender, RoutedEventArgs e)
        {
            DataRowView orders = OrdersGrid.SelectedItem as DataRowView;
            if (orders != null)
            {
                AddRecipe((int)orders["ID_Order"], (int)orders["Total_price"],
                    (int)orders["Received"],
                    (OrdersProductsGrid.ItemsSource as DataTable).Rows);
            }
        }
    }
}
