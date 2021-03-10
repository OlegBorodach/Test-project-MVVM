using System.Security.Cryptography.X509Certificates;
using Autofac;
using DataAccess;
using MvvmDialogs;
using UI.Data;
using UI.Data.Interfaces;
using UI.ViewModel;

namespace UI.Startup
{
    public class Initializer
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestDBContext>().AsSelf();
            builder.RegisterType<DataService>().AsSelf();
            builder.RegisterType<DepartmentViewModel>().AsSelf(); ;
            builder.RegisterType<EmployeeViewModel>().AsSelf(); ;
            builder.RegisterType<OrdersViewModel>().AsSelf(); ;
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<MainWindow>().AsSelf();
            return builder.Build();
        }
    }
}
