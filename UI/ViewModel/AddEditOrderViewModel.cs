using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Model;
using UI.Commands;
using UI.Data;
using UI.Data.Interfaces;

namespace UI.ViewModel
{
   public class AddEditOrderViewModel : ViewModelBase
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

        public Order CurrentEntity { get; set; }

        public List<Employee> Employees => _dataService.EmployeeRepository.GetAll();


        public AddEditOrderViewModel(Order employee, DataService dataService)
        {
            _edit = true;
            CurrentEntity = employee;
            _dataService = dataService;
        }

        public AddEditOrderViewModel(DataService dataService)
            : this(new Order {Date = DateTime.Now}, dataService)
        {
            _edit = false;
        }

        private void OnSaveEntity()
        {
            try
            {
                var orderRepository = _dataService.OrderRepository;
                if (_edit)
                {
                    orderRepository.Update(CurrentEntity);
                }
                else
                {
                    var entityAdded = orderRepository.Add(CurrentEntity);
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
