using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Model;
using UI.Commands;
using UI.Data;

namespace UI.ViewModel
{
    public class AddEditDepartmentViewModel : ViewModelBase
    {
        private readonly DataService _dataService;

        private ICommand _saveCommand;

        private readonly bool _edit;

        public ICommand SaveCommand
        {
            get => _saveCommand ??= new DelegateCommand(OnSaveEntity,CanSave);
            set => _saveCommand = value;
        }

        private bool CanSave()
        {
            return string.IsNullOrEmpty(CurrentEntity.Error);
        }

        public Department CurrentEntity { get; set; }

        public List<Employee> Employees => _dataService.EmployeeRepository.GetAll();


        public AddEditDepartmentViewModel(Department department, DataService dataService)
        {
            _edit = true;
            CurrentEntity = department;
            _dataService = dataService;
        }

        public AddEditDepartmentViewModel(DataService dataService)
            : this(new Department(), dataService)
        {
            _edit = false;
        }

        private void OnSaveEntity()
        {
            try
            {
                var departmentRepository = _dataService.DepartmentRepository;
                if (_edit)
                {
                    departmentRepository.Update(CurrentEntity);
                }
                else
                {
                    var entityAdded = departmentRepository.Add(CurrentEntity);
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
