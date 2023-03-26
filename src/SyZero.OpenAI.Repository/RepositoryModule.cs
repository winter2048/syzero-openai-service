using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using SyZero;
using SyZero.Domain.Repository;
using SyZero.SqlSugar;

namespace SyZero.OpenAI.Repository
{
    public class RepositoryModule : Module
    {
        public RepositoryModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            // 首先注册 options，供 DbContext 服务初始化使用
            builder.AddSyZeroSqlSugar<DbContext>();
            //注册仓储泛型
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope().PropertiesAutowired();
            ////注册持久化
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}

