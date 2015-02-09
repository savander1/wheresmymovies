using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheresMyMovies.Entities;
using Environment = NHibernate.Cfg.Environment;

namespace WheresMyMovies
{
    public class WheresMyMoviesRegistry : Registry
    {
        public WheresMyMoviesRegistry()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString("") )
                .Mappings(mappings => mappings.FluentMappings.AddFromAssemblyOf<User>())
                .BuildSessionFactory();

            // ISessionFactory is expensive to initialize, so create it as a singleton.
            For<ISessionFactory>()
                .Singleton()
                .Use(sessionFactory);

            // Cache each ISession per web request. Remember to dispose this!
            For<ISession>()
                .Use(context => context.GetInstance<ISessionFactory>().OpenSession());
        }
    }
}