using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmDialogs;
using UI.Commands;

namespace UI.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие для смены контента
        /// </summary>
        public static event EventHandler<ViewModelBase> ViewModelChanged;

        public event EventHandler<Exception> HasException;

        public ViewModelBase PrevViewModel { get; set; }

        private ICommand _backVmCommand;

        public ICommand BackVmCommand
        {
            get => _backVmCommand ??= new DelegateCommand(OnBackVm);
            set => _backVmCommand = value;
        }

        private void OnBackVm()
        {
            if (PrevViewModel != null)
            {
                OnViewModelChanged(PrevViewModel);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnHasException(Exception e)
        {
            HasException?.Invoke(this, e);
        }

        public virtual void Reload()
        {

        }

        protected static void OnViewModelChanged(ViewModelBase e)
        {
            ViewModelChanged?.Invoke(null, e);
        }
    }
}
