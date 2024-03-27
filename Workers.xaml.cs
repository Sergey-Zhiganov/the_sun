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
    public partial class Workers : Window
    {
        MainWindow window = new MainWindow();
        public Workers()
        {
            InitializeComponent();
            WorkersGrid.ItemsSource = AddPasswordColumn();
            OfficeCbx.ItemsSource = MainWindow.tables.Offices.GetData(0);
            RoleCbx.ItemsSource = MainWindow.tables.Roles.GetData();
        }

        public System.Collections.IEnumerable AddPasswordColumn()
        {
            DataTable workers = MainWindow.tables.Workers.GetWorkers();
            workers.Columns.Add("PasswordHidden");
            foreach (DataRow worker in workers.Rows)
            {
                worker["PasswordHidden"] = "Скрыт";
            }
            return (System.Collections.IEnumerable)workers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTables listTables = new ListTables();
            listTables.Show();
            Close();
        }

        private void WorkersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkersGrid.SelectedItem as DataRowView != null)
            {
                PassportTbx.Text = (WorkersGrid.SelectedItem as DataRowView)["Passport"].ToString();
                SurnameTbx.Text = (WorkersGrid.SelectedItem as DataRowView)["Surname"].ToString();
                FirstnameTbx.Text = (WorkersGrid.SelectedItem as DataRowView)["Firstname"].ToString();
                MiddleNameTbx.Text = (WorkersGrid.SelectedItem as DataRowView)["MiddleName"].ToString();
                PostTbx.Text = (WorkersGrid.SelectedItem as DataRowView)["Post"].ToString();
                OfficeCbx.Text = (WorkersGrid.SelectedItem as DataRowView)["Title"].ToString();
                User_loginTbx.Text = (WorkersGrid.SelectedItem as DataRowView)["User_login"].ToString();
                User_passwordPbx.Password = (WorkersGrid.SelectedItem as DataRowView)["User_password"].ToString();
                RoleCbx.Text = (WorkersGrid.SelectedItem as DataRowView)["Title1"].ToString();
                PhoneTbx.Text = (WorkersGrid.SelectedItem as DataRowView)["Phone"].ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string middleName = MiddleNameTbx.Text;
            if (middleName.Length == 0)
            {
                middleName = " ";
            }
            DataRowView office = OfficeCbx.SelectedItem as DataRowView;
            if (office == null)
            {
                office = OfficeCbx.Items[0] as DataRowView;
            }

            if (window.BaseCheck(null, new string[4] {PostTbx.Text, User_loginTbx.Text,
                User_passwordPbx.Password, PhoneTbx.Text},
                new string[3] { SurnameTbx.Text, FirstnameTbx.Text, middleName },
                new object[2] { office, RoleCbx.SelectedItem },
                new string[1] {PassportTbx.Text}))
            {
                int? office_id;
                if (OfficeCbx.SelectedItem as DataRowView == null)
                {
                    office_id = null;
                }
                else
                {
                    office_id = Convert.ToInt32(office["ID_Office"]);
                }
                MainWindow.tables.Workers.InsertWorkers(Convert.ToInt32(PassportTbx.Text), SurnameTbx.Text,
                    FirstnameTbx.Text, MiddleNameTbx.Text, PostTbx.Text, office_id,
                    User_loginTbx.Text, User_passwordPbx.Password,
                    Convert.ToInt32((RoleCbx.SelectedItem as DataRowView)["ID_Role"]),
                    PhoneTbx.Text);
                WorkersGrid.ItemsSource = AddPasswordColumn();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string middleName = MiddleNameTbx.Text;
            if (middleName.Length == 0)
            {
                middleName = " ";
            }
            if (window.BaseCheck(WorkersGrid.SelectedItem, new string[6] {SurnameTbx.Text, FirstnameTbx.Text,
                PostTbx.Text, User_loginTbx.Text, User_passwordPbx.Password, PhoneTbx.Text},
                new string[3] { SurnameTbx.Text, FirstnameTbx.Text, middleName },
                new object[2] { OfficeCbx.SelectedItem, RoleCbx.SelectedItem },
                new string[1] { PassportTbx.Text }))
            {
                MainWindow.tables.Workers.UpdateWorkers(
                    Convert.ToInt32(PassportTbx.Text), SurnameTbx.Text,
                    FirstnameTbx.Text, MiddleNameTbx.Text, PostTbx.Text,
                    Convert.ToInt32((OfficeCbx.SelectedItem as DataRowView)["ID_Office"]),
                    User_loginTbx.Text, User_passwordPbx.Password,
                    Convert.ToInt32((RoleCbx.SelectedItem as DataRowView)["ID_Role"]),
                    PhoneTbx.Text,
                    Convert.ToInt32((WorkersGrid.SelectedItem as DataRowView)["Passport"]),
                    $"{SurnameTbx.Text} {FirstnameTbx.Text} {MiddleNameTbx.Text}");
                WorkersGrid.ItemsSource = AddPasswordColumn();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (window.BaseCheck(WorkersGrid.SelectedItem))
            {
                MainWindow.tables.Workers.DeleteWorkers(
                    Convert.ToInt32((WorkersGrid.SelectedItem as DataRowView)["Passport"]));
                WorkersGrid.ItemsSource = AddPasswordColumn();
            }
        }
    }
}
