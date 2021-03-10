using System;
using System.Windows;
using System.Windows.Controls;
using UI.ViewModel;

namespace UI.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditDepartmentView.xaml
    /// </summary>
    public partial class AddEditOrderView : UserControl
    {
        public AddEditOrderView()
        {
            InitializeComponent();
            Loaded += (sender, args) => ((ViewModelBase)DataContext).HasException += OnHasException;
        }

        private void OnHasException(object sender, Exception e)
        {
            MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
