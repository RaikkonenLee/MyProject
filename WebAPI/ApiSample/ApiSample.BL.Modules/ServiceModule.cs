using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ApiSample.BL.Services;
using ApiSample.BL.Interfaces;
using System.Reflection;

namespace ApiSample.BL.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //第一個測試範例
            //builder.RegisterType<SampleService>().As<ISampleService>();
            var service = Assembly.Load("ApiSample.BL.Services");
            builder.RegisterAssemblyTypes(service).AsImplementedInterfaces();
        }
    }
}
