using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace up01_01
{
    public partial class MainWindow : Window
    {
        public static Tables tables = new Tables();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (User_login.Text.Length == 0)
            {
                error = "Не введен логин\n";
            }
            if (User_password.Password.Length == 0)
            {
                error += "Не введен пароль";
            }
            if (error != "")
            {
                MessageBox.Show(error);
                return;
            }
            ListTables.user = tables.Workers.Login(User_login.Text, User_password.Password);
            if (ListTables.user.Count == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }
            ListTables listtables = new ListTables();
            listtables.Show();
            Close();
        }
        public bool BaseCheck(object grid, string[] text = null, string[] text1 = null, object[] objects = null,
            string[] ints = null)
        {
            if (grid != null)
            {
                if (grid == null)
                {
                    return false;
                }
            }
            if (text != null)
            {
                if (text.Length == 0)
                {
                    MessageBox.Show("Все поля обязательны для заполнения");
                    return false;
                }
                if (Check(text, 0))
                {
                    MessageBox.Show("Обнаружен недопустимый символ/эмодзи");
                    return false;
                }
            }
            if (text1 != null)
            {
                if (text.Length == 0)
                {
                    MessageBox.Show("Все поля обязательны для заполнения");
                    return false;
                }
                if (Check(text, 1))
                {
                    MessageBox.Show("Обнаружен недопустимый символ/эмодзи");
                    return false;
                }
            }
            if (objects != null)
            {
                foreach (object obj in objects)
                {
                    if (obj == null)
                    {
                        MessageBox.Show("Все поля обязательны для заполнения");
                        return false;
                    }
                }
            }
            if (ints != null)
            {
                foreach(string i in ints)
                {
                    int a = 0;
                    try
                    {
                        a = Convert.ToInt32(i);
                    }
                    catch
                    {
                        MessageBox.Show("Обнаружен запрещенный символ (не цифры)");
                        return false;
                    }
                    if (a < 0)
                    {
                        MessageBox.Show("Число не может быть меньше 0");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool Check(string[] text, int type)
        {
            foreach (string s in text)
            {
                if (type == 0)
                {
                    if (Regex.IsMatch(s, @"\p{Cs}"))
                    {
                        return true;
                    }
                }
                else
                {
                    if (Regex.IsMatch(s, @"\p{N}|\p{Cs}"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}