using System.Windows;
using System.Windows.Input;
using MvvmDialogs;
using UI.Commands;

namespace UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private ICommand _getDepartmentsCommand;
        private ICommand _getEmployeesCommand;
        private ICommand _getOrdersCommand;

        public ViewModelBase CurrentViewModel { get; set; }

        private readonly DepartmentViewModel _departmentViewModel;
        private readonly EmployeeViewModel _employeeViewModel;
        private readonly OrdersViewModel _ordersViewModel;

        public MainViewModel(IDialogService dialogService, 
            DepartmentViewModel departmentViewModel, 
            EmployeeViewModel employeeViewModel, 
            OrdersViewModel ordersViewModel)
        {
            
            _dialogService = dialogService;
            _departmentViewModel = departmentViewModel;
            _employeeViewModel = employeeViewModel;
            _ordersViewModel = ordersViewModel;
            CurrentViewModel = this;
            ViewModelChanged += OnViewModelChanged;
        }

        private void OnViewModelChanged(object sender, ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public ICommand GetDepartmentsCommand
        {
            get => _getDepartmentsCommand ??= new DelegateCommand(OnGetDepartments);
            set => _getDepartmentsCommand = value;
        }

        public ICommand GetEmployeesCommand
        {
            get => _getEmployeesCommand ??= new DelegateCommand(OnGetEmployees);
            set => _getEmployeesCommand = value;
        }

        public ICommand GetOrdersCommand
        {
            get => _getOrdersCommand ??= new DelegateCommand(OnGetOrders);
            set => _getOrdersCommand = value;
        }

        private void OnGetOrders()
        {
            _ordersViewModel.PrevViewModel = this;
            OnViewModelChanged(_ordersViewModel);
        }

        private void OnGetEmployees()
        {
            _employeeViewModel.PrevViewModel = this;
            OnViewModelChanged(_employeeViewModel);
        }

        private void OnGetDepartments()
        {
            _departmentViewModel.PrevViewModel = this;
            OnViewModelChanged(_departmentViewModel);
        }
    }
}
