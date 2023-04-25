using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Investigate.Intefaces;

namespace WebApp_Investigate.Services
{
    public class ServiceImplementation : ISomeService
    {
        public ServiceImplementation()
        {
            ServiceValue = "Injected from Startup";
        }

        public string ServiceValue { get; set; }
    }
}
