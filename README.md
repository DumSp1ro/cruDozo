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
