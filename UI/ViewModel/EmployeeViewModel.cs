using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Model;
using MvvmDialogs;
using UI.Commands;
using UI.Data;
using UI.Data.Interfaces;

namespace UI.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private readonly IDialogService _dialogService;

        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _addCommand;

        public ICommand EditCommand
        {
            get => _editCommand ??= new DelegateCommand<Employee>(OnEditEmployee);
            set => _editCommand = value;
        }

        public ICommand DeleteCommand
        {
            get => _deleteCommand ??= new DelegateCommand<Employee>(OnDeleteEmployee);
            set => _deleteCommand = value;
        }

        public ICommand AddCommand
        {
            get => _addCommand ??= new DelegateCommand(OnAddEmployee);
            set => _addCommand = value;
        }

        public ObservableCollection<Employee> Employees => new ObservableCollection<Employee>(_dataService.EmployeeRepository.GetAll("Department"));

        public EmployeeViewModel(IDialogService dialogService, DataService dataService)
        {
            _dialogService = dialogService;
            _dataService = dataService;
        }

        private void OnEditEmployee(Employee employee)
        {
            var editVm = new AddEditEmployeeViewModel(employee, _dataService)
            {
                PrevViewModel = this
            };
            OnViewModelChanged(editVm);
        }

        private async void OnDeleteEmployee(Employee employee)
        {
            var result = _dialogService.ShowMessageBox(this,
                $"Удалить сотрудника №{employee.EmployeeId}",
                "Удаление",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            
            var employeeRepository = _dataService.EmployeeRepository;
            
            var objFromDb =  employeeRepository.GetById(employee.EmployeeId);
            if (objFromDb == null) return;
            try
            {
                employeeRepository.Remove(employee);
                Employees.Remove(employee);
            }
            catch (InvalidOperationException e)
            {
                OnHasException(e);
            }
        }

        private void OnAddEmployee()
        {
            var addVm = new AddEditEmployeeViewModel(_dataService)
            {
                PrevViewModel = this
            };
            OnViewModelChanged(addVm);
        }
    }
}
