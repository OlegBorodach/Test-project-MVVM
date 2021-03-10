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
    public class OrdersViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private readonly IDialogService _dialogService;

        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private ICommand _addCommand;

        public ICommand EditCommand
        {
            get => _editCommand ??= new DelegateCommand<Order>(OnEditOrder);
            set => _editCommand = value;
        }

        public ICommand DeleteCommand
        {
            get => _deleteCommand ??= new DelegateCommand<Order>(OnDeleteOrder);
            set => _deleteCommand = value;
        }

        public ICommand AddCommand
        {
            get => _addCommand ??= new DelegateCommand(OnAddOrder);
            set => _addCommand = value;
        }

        public ObservableCollection<Order> Orders =>
            new ObservableCollection<Order>(_dataService.OrderRepository.GetAll("Employee"));

        public OrdersViewModel(IDialogService dialogService, DataService dataService)
        {

            _dialogService = dialogService;
            _dataService = dataService;
        }

        private void OnEditOrder(Order order)
        {
            var editVm = new AddEditOrderViewModel(order, _dataService)
            {
                PrevViewModel = this
            };
            OnViewModelChanged(editVm);
        }

        private void OnDeleteOrder(Order order)
        {
            var result = _dialogService.ShowMessageBox(this,
                 $"Удалить заказ №{order.EmployeeId}",
                 "Удаление",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;


            var orderRepository = _dataService.OrderRepository;

            var objFromDb = orderRepository.GetById(order.EmployeeId);
            if (objFromDb == null) return;
            try
            {
                orderRepository.Remove(order);
            }
            catch (InvalidOperationException e)
            {
                OnHasException(e);
            }
        }

        private void OnAddOrder()
        {
            var addVm = new AddEditOrderViewModel(_dataService)
            {
                PrevViewModel = this
            };
            OnViewModelChanged(addVm);
        }
    }
}
