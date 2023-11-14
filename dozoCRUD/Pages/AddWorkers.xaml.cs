using dozoCRUD.Classes;
using dozoCRUD.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для AddWorkers.xaml
    /// </summary>
    public partial class AddWorkers : Page
    {
        public OpenFileDialog ofd = new OpenFileDialog();
        private string newsourthpath = string.Empty;
        string path = "";
        private bool flag = false;
        private workers currentworker = new workers();
        //public DateTime datetoday = DateTime.Now;
        public AddWorkers(workers sellectedWorkers)
        {
            InitializeComponent();
            if (sellectedWorkers != null)
            {
                currentworker = sellectedWorkers;
            }
            DataContext = currentworker;
        }

        private void Save(object sender, RoutedEventArgs e)
        {

            //currentworker.data = datetoday;
            //currentworker.time = datetoday.TimeOfDay;
            
            if (currentworker.id == 0)
            {
                dozoEntities.GetContext().workers.Add(currentworker);
            }

            DbContextTransaction dbContextTransaction = null;

            try
            {
                if (currentworker.id == 0)
                {
                    dozoEntities.GetContext().workers.Add(currentworker);
                }
                
                dbContextTransaction = dozoEntities.GetContext().Database.BeginTransaction();

                dozoEntities.GetContext().SaveChanges();

                MessageBox.Show("Информация сохранена!");
                dbContextTransaction.Commit();
                Manager.MainFrame.GoBack();
            }
            catch (DbUpdateException ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }

                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    MessageBox.Show($"Внутреннее исключение: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                MessageBox.Show("Ошибка при сохранении изменений. Дополнительные сведения в внутреннем исключении.");
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }

                MessageBox.Show($"Ошибка при обновлении записей. Дополнительные сведения: {ex.Message}");
            }
            finally
            {
                dbContextTransaction?.Dispose();
            }
        }

        private void Foto(object sender, RoutedEventArgs e)
        {
            string Source = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == true) 
            {
                flag = true;
                string sourthpath = ofd.SafeFileName;
                newsourthpath = Source.Replace("/bin/Debug", "/img/") + sourthpath;
                PreviewImage.Source = new BitmapImage(new Uri(ofd.FileName));
                path = ofd.FileName;
                currentworker.photo = $"/img/{ofd.SafeFileName}";
            }
        }

        //private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Обработка изменения выбранной даты
        //    // Пример: обновление TextBlock с именем "SelectedDateTextBlock"
        //    Data.Text = DatePicker.SelectedDate?.ToShortDateString();
        //}
    }
}

