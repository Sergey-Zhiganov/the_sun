using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class OrdersProducts : Window
    {
        int total_cost = 0;
        public OrdersProducts()
        {
            InitializeComponent();
            OfficeCbx.ItemsSource = MainWindow.tables.Offices.GetData(0);
            ClientCbx.ItemsSource = MainWindow.tables.Clients.GetData();
            try
            {
                OfficeCbx.Text = MainWindow.tables.Offices.GetData(
                    Convert.ToInt32(ListTables.user[0]["Office_ID"]))[0]["Title"].ToString();
                OfficeCbx.IsEnabled = false;
            }
            catch
            {
                OfficeCbx.Text = MainWindow.tables.Offices.GetData(0)[0]["Title"].ToString();
            }
        }
        private void OfficeCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedOrdersProductsGrid.ItemsSource = null;
            total_cost = 0;
            Total_sum.Content = $"Сумма: {total_cost}";
            OrdersProductsGrid.ItemsSource = MainWindow.tables.Products.GetProducts(
                Convert.ToInt32((OfficeCbx.SelectedItem as DataRowView)["ID_Office"]));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Orders orders = new Orders();
            orders.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (OrdersProductsGrid.SelectedItem != null)
            {
                DataRowView current = OrdersProductsGrid.SelectedItem as DataRowView;
                int amount = (int)current["Amount"];
                int id = (int)current["ID_Product"];
                int count = 0;
                DataView selected = SelectedOrdersProductsGrid.ItemsSource as DataView;
                if (selected != null)
                {
                    foreach (DataRow row in selected.Table.Rows)
                    {
                        if ((int)row["ID_Product"] == id)
                        {
                            count++;
                        }
                    }
                    if (count < amount || amount == -1)
                    {
                        DataRow newRow = selected.Table.NewRow();
                        foreach (DataColumn column in selected.Table.Columns)
                        {
                            newRow[column.ColumnName] = current.Row[column.ColumnName];
                        }
                        total_cost += (int)newRow["Price"];
                        Total_sum.Content = $"Сумма: {total_cost}";
                        selected.Table.Rows.Add(newRow);
                    }
                }
                else
                {
                    DataTable newTable = new DataTable();
                    foreach (DataColumn column in (OrdersProductsGrid.ItemsSource as DataTable).Columns)
                    {
                        newTable.Columns.Add(column.ColumnName, column.DataType);
                    }
                    DataRow newRow = newTable.NewRow();
                    foreach (DataColumn column in newTable.Columns)
                    {
                        newRow[column.ColumnName] = current.Row[column.ColumnName];
                    }
                    total_cost += (int)newRow["Price"];
                    Total_sum.Content = $"Сумма: {total_cost}";
                    newTable.Rows.Add(newRow);
                    SelectedOrdersProductsGrid.ItemsSource = newTable.DefaultView;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SelectedOrdersProductsGrid.SelectedItem != null)
            {
                total_cost -= (int)(SelectedOrdersProductsGrid.SelectedItem as DataRowView).Row["Price"];
                Total_sum.Content = $"Сумма: {total_cost}";
                (SelectedOrdersProductsGrid.ItemsSource as DataView).Table.Rows.Remove(
                    (SelectedOrdersProductsGrid.SelectedItem as DataRowView).Row);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow.tables.Orders.InsertOrders(Convert.ToInt32(ListTables.user[0]["Passport"]),
                Convert.ToInt32((ClientCbx.SelectedItem as DataRowView)["Passport"]),
                Convert.ToInt32((OfficeCbx.SelectedItem as DataRowView)["ID_Office"]),
                total_cost, DateTime.Now,
                (ClientCbx.SelectedItem as DataRowView)["FullName"].ToString(),
                $"{ListTables.user[0]["Surname"]} {ListTables.user[0]["Firstname"]} {ListTables.user[0]["MiddleName"]}",
                Convert.ToInt32(ReceivedTbx.Text));
            DataRowCollection data = MainWindow.tables.Orders.GetData().Rows;
            int order_id = (int)data[data.Count - 1]["ID_Order"];
            foreach (DataRow row in (SelectedOrdersProductsGrid.ItemsSource as DataView).Table.Rows)
            {
                if (row["Title"].ToString() != "")
                {
                    MainWindow.tables.Products.ReduceAmount((int)row["ID_Product"]);
                }
                MainWindow.tables.OrdersProducts.InsertOrdersProducts(order_id,
                    (int)row["ID_Product"], (int)row["Price"]);
            }
            
            Orders orders = new Orders();
            orders.AddRecipe(order_id, total_cost, Convert.ToInt32(ReceivedTbx.Text),
                    (SelectedOrdersProductsGrid.ItemsSource as DataView).Table.Rows);
            orders.Show();
            Close();
        }
    }
}