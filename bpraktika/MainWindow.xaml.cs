using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using GSF;

namespace bpraktika
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<data> myObjects;
        public ICollectionView emtickView { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            myObjects = new ObservableCollection<data>()


           {
            new data { Name = "Кирилл", Surname = "Кириллов", Clas = "8Б", Propuski=1},
             new data { Name = "Cемен", Surname = "Семенов", Clas = "8Б", Propuski=4},
           new data { Name = "Игорь", Surname = "Игорев", Clas = "8Б",Propuski=10 },
            new data { Name = "Вася", Surname = "Пупкин", Clas = "8Б", Propuski = 1 },
            new data { Name = "Дениса", Surname = "Сгибнев", Clas = "9А", Propuski=10},
             new data { Name = "Игорь", Surname = "Капустников", Clas = "10Б", Propuski=17 },
           new data { Name = "Степан", Surname = "Исаев", Clas = "11Г", Propuski=4, },
         new data { Name = "Александер", Surname = "Стреликов", Clas = "9А", Propuski = 10 },


            };
            PDG.ItemsSource = myObjects;
            DataContext = myObjects;
            emtickView = CollectionViewSource.GetDefaultView(myObjects);
        }



        private void admbtт_Click(object sender, RoutedEventArgs e)
        {
            ltbx.IsReadOnly = false;
            ptbx.IsReadOnly = false;
            ltbx.Visibility = Visibility.Visible;
            ptbx.Visibility = Visibility.Visible;
            lg.Visibility = Visibility.Visible;
            ps.Visibility = Visibility.Visible;
            login1.Visibility = Visibility.Visible;
        }

        private void nadmbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void login1_Click(object sender, RoutedEventArgs e)
        {
            string login = "admin";
            string password = "12345";
            if (ltbx.Text != login || ptbx.Text != password)
            {
                MessageBox.Show("Неправильно введен логин или пароль");
                ltbx.Visibility = Visibility.Hidden;
                ptbx.Visibility = Visibility.Hidden;
                lg.Visibility = Visibility.Hidden;
                ps.Visibility = Visibility.Hidden;
                login1.Visibility = Visibility.Hidden;

            }
            else
            {
                MessageBox.Show("Извини братан спортсмены");
                ltbx.IsReadOnly = true;
                ptbx.IsReadOnly = true;
                ltbx.Visibility = Visibility.Hidden;
                ptbx.Visibility = Visibility.Hidden;
                lg.Visibility = Visibility.Hidden;
                ps.Visibility = Visibility.Hidden;
                login1.Visibility = Visibility.Hidden;
                admbtт.Visibility = Visibility.Hidden;
                exit.Visibility = Visibility.Visible;
                admpanel.Visibility = Visibility.Visible;
                cngcolumn.Visibility = Visibility.Visible;
                delcolumn.Visibility = Visibility.Visible;
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            admbtт.Visibility = Visibility.Visible;
            PDG.IsReadOnly = true;
            exit.Visibility = Visibility.Hidden;
            admpanel.Visibility = Visibility.Hidden;
            cngcolumn.Visibility = Visibility.Hidden;
            delcolumn.Visibility = Visibility.Hidden;
        }

        private void tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = tbx.Text.ToLower();
            emtickView.Filter = namik =>
            {
                if (namik is data d)
                {
                    return d.Name.ToLower().Contains(filterText);
                }
                return false;
            };
        }

        private void tbx1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = tbx1.Text.ToLower();
            emtickView.Filter = namik =>
            {
                if (namik is data d)
                {
                    return d.Surname.ToLower().Contains(filterText);
                }
                return false;
            };
        }

        private void tbx2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = tbx2.Text.ToLower();
            emtickView.Filter = namik =>
            {
                if (namik is data d)
                {
                    return d.Clas.ToLower().Contains(filterText);
                }
                return false;
            };
        }





        private void delbtn_Click(object sender, RoutedEventArgs e)
        {
            data delObkect = PDG.SelectedItem as data;
            if (PDG.SelectedIndex >= 0)
            {
                var res = MessageBox.Show("Ты внатуре хочешь удалить запись?", "подтверждение внатуре", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (MessageBoxResult.Yes == res)
                {
                    myObjects.Remove(delObkect);
                }
            }

        }


        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                data myObject = new data() { Name = adName.Text, Surname = adSurname.Text, Clas = adKlas.Text, Propuski = Convert.ToInt32(adPropuski.Text) };
                myObjects.Add(myObject);
            }
            catch {
                MessageBox.Show("Ты накосячил где-то, возможно у тебя пустые значения, \nну или ты в поле с пропусками буквы ввел еблан", "Косяк", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void chgbttn_Click(object sender, RoutedEventArgs e)
        {
           if(PDG.SelectedItem is data selecteddata)
            {
                try
                {
                    selecteddata.Name = adName.Text;
                    selecteddata.Surname = adSurname.Text;
                    selecteddata.Clas = adKlas.Text;
                    selecteddata.Propuski = Convert.ToInt32(adPropuski.Text);
                    PDG.Items.Refresh();
                }
                catch
                {
                    MessageBox.Show("Ты накосячил где-то, возможно у тебя пустые значения, \nну или ты в поле с пропусками буквы ввел еблан", "Косяк", MessageBoxButton.OK, MessageBoxImage.Error);
                }
              
            }
            
        }
    }
}
