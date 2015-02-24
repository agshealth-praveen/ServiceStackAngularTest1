﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Funq;
using vhsConnectMonitor.ServiceInterface;
using ServiceStack.Razor;
using ServiceStack;
using vhsConnectMonitor.ServiceModel;

namespace vhsConnectMonitor
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("vhsConnectMonitor", typeof(MyServices).Assembly)
        {

        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            this.Plugins.Add(new RazorFormat());


            container.Register<ISimpleRepository<WsInstance>>(new ListBasedRepository<WsInstance>(
                new WsInstance { ClientId = "vhs1", Status = new WsStatus{DbRunning=true,MqRunning=true,Version="1"}},
                new WsInstance { ClientId = "vhs2", Status = new WsStatus { DbRunning = true, MqRunning = true, Version = "2" } },
                new WsInstance { ClientId = "vhs3", Status = new WsStatus { DbRunning = true, MqRunning = false, Version = "1" } }
                )
            );
        }
    }
}