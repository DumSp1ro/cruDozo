![image](https://github.com/DumSp1ro/cruDozo/assets/146105715/c4443e4f-245a-40b6-b0a1-1a3523ab3c3c)</br>
        
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new editzayavki((sender as Button).DataContext as Zayavka));
        }
![image](https://github.com/DumSp1ro/cruDozo/assets/146105715/4a74109d-af40-4fe7-bfc0-d475104a2ea4)</br>


![image](https://github.com/DumSp1ro/cruDozo/assets/146105715/e57383d9-5908-4caf-b7c3-0c9bd25fd0c0)</br>

        <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
            <TextBlock>Статус</TextBlock>
            <TextBox Text="{Binding StatusID}"></TextBox>
            <TextBlock>Описание проблемы</TextBlock>
            <TextBox Text="{Binding OpisanieProblem}"></TextBox>
            <TextBlock>Исполнитель</TextBlock>
            <TextBox Text="{Binding IspolnitelID}"></TextBox>
            <TextBlock>Комментарии</TextBlock>
            <TextBox Text="{Binding Comment}"></TextBox>
            <Button Click="Save">Сохранить</Button>
        </StackPanel>

![image](https://github.com/DumSp1ro/cruDozo/assets/146105715/a0dfeccd-a0ee-402e-bc31-0aa99b9483ce)

        private Zayavka currentzayav = new Zayavka();
        public editzayavki(Zayavka zav)
        {
            InitializeComponent();
            if (zav != null)
            {
                currentzayav = zav;
            }
            DataContext = currentzayav;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (currentzayav.ID == 0)
            {
                user11Entities.GetContext().Zayavka.Add(currentzayav);
            }

            DbContextTransaction dbContextTransaction = null;

            try
            {
                if (currentzayav.ID == 0)
                {
                    user11Entities.GetContext().Zayavka.Add(currentzayav);
                }

                dbContextTransaction = user11Entities.GetContext().Database.BeginTransaction();

                user11Entities.GetContext().SaveChanges();

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










----
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


        <DataGrid.Columns>
                <DataGridTextColumn Header="Номер" Binding="{Binding ID}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Дата" Binding="{Binding Data,StringFormat={}{0:dd/MM/yyyy}}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Оборудование" Binding="{Binding Oborud.NameOborud}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Тип неиправности" Binding="{Binding Neisp.TypeNeisp}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Описание проблемы" Binding="{Binding OpisanieProblem}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Клиент" Binding="{Binding Client.NameClient}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Статус" Binding="{Binding Status.StatusZayavki}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Исполнитель" Binding="{Binding Ispolnitel.NameIspolnitel}" Width="auto" ></DataGridTextColumn>
                <DataGridTextColumn Header="Комментарий" Binding="{Binding Comment}" Width="auto" ></DataGridTextColumn>
                <DataGridTemplateColumn Width="auto">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <Button Name="BntDell" Click="Save" Width="150">Сохранить</Button>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
                <DataGridTemplateColumn Width="auto">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <Button Name="BntEdit" Click="Edit_Click" Width="150">Редактировать</Button>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
                <DataGridTemplateColumn Width="auto">
                    <DataGridTemplateColumn.CellTemplate>
                        <DataTemplate>
                            <Button Name="BntDell" Click="Delete" Width="150">Удалить</Button>
                        </DataTemplate>
                    </DataGridTemplateColumn.CellTemplate>
                </DataGridTemplateColumn>
            </DataGrid.Columns>
