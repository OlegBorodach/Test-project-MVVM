using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Model;
using UI.Commands;
using UI.Data;
using UI.Data.Interfaces;

namespace UI.ViewModel
{
    public class AddEditEmployeeViewModel : ViewModelBase
    {
        private readonly DataService _dataService;

        private ICommand _saveCommand;

        private readonly bool _edit;

        public ICommand SaveCommand
        {
            get => _saveCommand ??= new DelegateCommand(OnSaveEntity, CanSave);
            set => _saveCommand = value;
        }

        private bool CanSave()
        {
            return string.IsNullOrEmpty(CurrentEntity.Error);
        }

        public Employee CurrentEntity { get; set; }

        public List<Department> Departments => new List<Department>(_dataService.DepartmentRepository.GetAll());

        public AddEditEmployeeViewModel(Employee employee, DataService dataService)
        {
            _edit = true;
            CurrentEntity = employee;
            _dataService = dataService;
        }

        public AddEditEmployeeViewModel(DataService dataService)
            : this(new Employee() { DateOfBirth = DateTime.Now }, dataService)
        {
            _edit = false;
        }

        private void OnSaveEntity()
        {
            try
            {
                var employeeRepository = _dataService.EmployeeRepository;
                if (_edit)
                {
                    employeeRepository.Update(CurrentEntity);
                }
                else
                {
                    var entityAdded = employeeRepository.Add(CurrentEntity);
                }
                OnViewModelChanged(PrevViewModel);
            }
            catch (DbUpdateException e)
            {
                OnHasException(e.InnerException);
            }
            catch (InvalidOperationException e)
            {
                OnHasException(e);
            }
        }
    }
}
