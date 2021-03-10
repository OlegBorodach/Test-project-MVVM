using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.ViewModel;

namespace UI.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditEmployeeView.xaml
    /// </summary>
    public partial class AddEditEmployeeView : UserControl
    {
        public AddEditEmployeeView()
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
