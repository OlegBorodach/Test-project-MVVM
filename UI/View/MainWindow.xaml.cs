using System.Windows;
using UI.ViewModel;


namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; set; }

        public MainWindow(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            InitializeComponent();
        }
    }
}
