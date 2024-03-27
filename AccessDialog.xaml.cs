using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace up01_01
{
    public partial class AccessDialog : Window
    {
        public AccessDialog()
        {
            InitializeComponent();
            List<CheckBox> list = new List<CheckBox>()
            {
                Check0, Check1, Check2, Check3, Check4, Check5,
                Check6, Check7, Check8, Check9, Check10
            };
            for (int i = 0; i < Roles.access.Length - 1; i++)
            {
                list[i].IsChecked = Convert.ToBoolean(Convert.ToInt32(Roles.access[i]) - 48);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<CheckBox> list = new List<CheckBox>()
            {
                Check0, Check1, Check2, Check3, Check4, Check5,
                Check6, Check7, Check8, Check9, Check10
            };
            Roles.access = "";
            foreach (CheckBox c in list)
            {
                Roles.access += Convert.ToInt32(c.IsChecked);
            }
            Close();
        }
    }
}