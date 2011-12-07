using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;

namespace Momntz.UI.Web.Injection
{
    public class Startable : IStartableBus
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IBus Start(Action startupAction)
        {
            throw new NotImplementedException();
        }

        public IBus Start()
        {
            throw new NotImplementedException();
        }

        public event EventHandler Started;
    }
}