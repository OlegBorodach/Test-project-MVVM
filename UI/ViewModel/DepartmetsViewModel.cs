using System;
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
    public class DepartmentViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private readonly IDialogService _dialogService;

        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _addCommand;

        public ICommand EditCommand
        {
            get => _editCommand ??= new DelegateCommand<Department>(OnEditDepartment);
            set => _editCommand = value;
        }

        public ICommand DeleteCommand
        {
            get => _deleteCommand ??= new DelegateCommand<Department>(OnDeleteDepartment);
            set => _deleteCommand = value;
        }

        public ICommand AddCommand
        {
            get => _addCommand ??= new DelegateCommand(OnAddDepartment);
            set => _addCommand = value;
        }

        public ObservableCollection<Department> Departments => new ObservableCollection<Department>(_dataService.DepartmentRepository.GetAll("Employee"));

        public DepartmentViewModel(IDialogService dialogService, DataService dataService)
        {
            _dialogService = dialogService;
            _dataService = dataService;
        }

        private void OnEditDepartment(Department department)
        {
            var editVm = new AddEditDepartmentViewModel(department, _dataService)
            {
                PrevViewModel = this
            };
            OnViewModelChanged(editVm);
        }

        private void OnDeleteDepartment(Department department)
        {
            var result = _dialogService.ShowMessageBox(this,
                $"Удалить подразделение №{department.DepartmentId}",
                "Удаление",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            var departmentRepository = _dataService.DepartmentRepository;
            var objFromDb = departmentRepository.GetById(department.DepartmentId);
            if (objFromDb == null) return;
            try
            {
                departmentRepository.Remove(department);
                Departments.Remove(department);
            }
            catch (InvalidOperationException e)
            {
                OnHasException(e);
            }
        }

        private void OnAddDepartment()
        {
            var addVm = new AddEditDepartmentViewModel(_dataService)
            {
                PrevViewModel = this
            };
            OnViewModelChanged(addVm);
        }
    }
}
