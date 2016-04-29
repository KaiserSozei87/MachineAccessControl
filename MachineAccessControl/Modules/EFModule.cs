using Autofac;
using MachineAccessControl.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MachineAccessControl.Modules
{
    public class EFModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType(typeof(MachineAccessControlContext)).As(typeof(IContext)).InstancePerLifetimeScope();

        }


    }
}