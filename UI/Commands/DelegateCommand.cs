using System;
using System.Windows.Input;

namespace UI.Commands
{

    /// <summary>
    /// Класс реализующий интерфейс ICommand
    /// Позволяет делегировать логику команд методам без параметров
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Делегат инкапсулирует метод, который не принимает ни одного параметра и не возвращает значений.
        /// </summary>
        private readonly Action _executeMethod;

        /// <summary>
        ///Делегат инкапсулирует метод без параметров и возвращает значение типа bool
        /// </summary>
        private readonly Func<bool> _canExecuteMethod;

        /// <summary>
        ///  Конструктор
        /// </summary>
        public DelegateCommand(Action executeMethod)
            : this(executeMethod, null)
        {
        }

        /// <summary>
        ///  Конструктор
        /// </summary>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Метод определяет может ли команда быть выполнена
        /// </summary>
        public bool CanExecute()
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod();
            }
            return true;
        }

        /// <summary>
        /// Метод выполняемый командой
        /// </summary>
        public void Execute()
        {
            if (_executeMethod != null)
            {
                _executeMethod();
            }
        }

        #region  Реализация интерфейса ICommand

        /// <summary>
        ///  Реализация ICommand.CanExecuteChanged 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        #endregion
    }

    /// <summary>
    /// Класс реализующий интерфейс ICommand
    /// Позволяет делегировать логику команд методам с параметрами
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _executeMethod;

        private readonly Func<T, bool> _canExecuteMethod;

        /// <summary>
        ///  Конструктор
        /// </summary>
        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        /// <summary>
        ///  Конструктор
        /// </summary>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Метод, реализующий проверку - может ли команда быть выполнена
        /// </summary>
        public bool CanExecute(T parameter)
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod(parameter);
            }
            return true;
        }

        /// <summary>
        /// Метод, выполняемый командой
        /// </summary>
        public void Execute(T parameter)
        {
            if (_executeMethod != null)
            {
                _executeMethod(parameter);
            }
        }

        #region  Реализация интерфейса ICommand

        /// <summary>
        ///  Реализация ICommand.CanExecuteChanged 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion


    }
}
