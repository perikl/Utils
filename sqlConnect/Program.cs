using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using AutoCADTest;

namespace sqlConnect
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(); 
            CRMHelper.connectDB("server=.\SQLEXPRESS;Database=mongosoft;uid=test;pwd=test");
        }
    }
}
