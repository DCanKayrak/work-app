using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Module = Autofac.Module;
using Core.Utilities.Interceptors;
using Business.Concrete;
using Business.Abstract;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterType<PomodoroManager>().As<IPomodoroService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<TaskManager>().As<ITaskService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<OperationClaimManager>().As<IRoleService>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<TaskCollectionManager>().As<ITaskCollectionService>();
            builder.RegisterType<FollowerManager>().As<IFollowerService>();

            // Repositories

            builder.RegisterType<EfUserDal>().As<IUserRepository>();
            builder.RegisterType<EfPomodoroDal>().As<IPomodoroRepository>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimRepository>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimRepository>();
            builder.RegisterType<EfTaskCollectionDal>().As<ITaskCollectionRepository>();
            builder.RegisterType<EfTaskDal>().As<ITaskRepository>();
            builder.RegisterType<EfFollowerDal>().As<IFollowerRepository>();
            
            // Others
            builder.RegisterType<EfDbContext>().As<EfDbContext>();
            
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}