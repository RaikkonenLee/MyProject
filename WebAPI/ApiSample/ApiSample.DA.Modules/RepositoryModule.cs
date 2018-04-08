using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ApiSample.DA.Interfaces;
using ApiSample.DA.Repositories;
using System.Reflection;
using ApiSample.DA.Tables;

namespace ApiSample.DA.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //第一個測試範例
            //builder.RegisterType<SampleRepository>().As<ISampleRepository>();
            var repository = Assembly.Load("ApiSample.DA.Repositories");
            builder.RegisterAssemblyTypes(repository).AsImplementedInterfaces();
            builder.RegisterType<ShopContext>().As<ShopContext>();
        }
    }
}
