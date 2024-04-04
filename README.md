![image](https://github.com/DumSp1ro/cruDozo/assets/146105715/2e9cbe1d-cb89-4981-b229-746ca78ad3ec)
![image](https://github.com/DumSp1ro/cruDozo/assets/146105715/281bf572-57e7-4ccb-a48b-18d980c1a9fa)

        private void Delete(object sender, RoutedEventArgs e)
        {
            var ZayavDell = ZayavBD.SelectedItems.Cast<Zayavka>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {ZayavDell.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    user11Entities.GetContext().Zayavka.RemoveRange(ZayavDell);
                    user11Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    // Обновление ObservableCollection, что автоматически обновит DataGrid
                    foreach (var merch in ZayavDell)
                    {
                        ZayavkaCollection.Remove(merch);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
