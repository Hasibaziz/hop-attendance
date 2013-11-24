using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using NHibernate.Cfg;
using NHibernate;

namespace ERP
{
    public class Bootstrapper
    {
        public static void Setup()
        {
            ConfigureNHibernate();
            AutoMapperInit();
        }


        private static void ConfigureNHibernate()
        {
            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration().Configure();
            cfg.Properties["connection.connection_string"] = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            //cfg.Properties["connection.connection_string"] = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
            //cfg.Properties["connection.connection_string"] = ConfigurationManager.ConnectionStrings["sql01"].ConnectionString;
           
        }


        private static void AutoMapperInit()
        {
            //Mapper.CreateMap<ContactModel, SystemContact>();
            //Mapper.CreateMap<SystemUserModel, SystemUser>();
            //Mapper.CreateMap<SystemContact, ContactModel>();
            //Mapper.CreateMap<SystemUser, SystemUserModel>();
        }

    }
}