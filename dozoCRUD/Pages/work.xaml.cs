using dozoCRUD.Classes;
using dozoCRUD.Models;
using System;
using System.Collections.Generic;
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

namespace dozoCRUD.Pages
{
    /// <summary>
    /// Логика взаимодействия для work.xaml
    /// </summary>
    public partial class work : Page
    {
        public work()
        {
            InitializeComponent();
            BDWorkers.ItemsSource = dozoEntities.GetContext().workers.ToList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddWorkers((sender as Button).DataContext as workers));
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddWorkers(null));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var workersDell = BDWorkers.SelectedItems.Cast<workers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {workersDell.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dozoEntities.GetContext().workers.RemoveRange(workersDell);
                    dozoEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    BDWorkers.ItemsSource = dozoEntities.GetContext().workers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
