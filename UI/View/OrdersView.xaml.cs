using System;
using System.Windows;
using System.Windows.Controls;
using UI.ViewModel;

namespace UI.View
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
           Loaded+=OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ((ViewModelBase)DataContext).HasException+=OnHasException;
        }

        private void OnHasException(object sender, Exception e)
        {
            MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
