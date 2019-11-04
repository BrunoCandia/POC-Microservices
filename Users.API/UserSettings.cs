﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API
{
    public class UserSettings
    {
        public string ExternalCatalogBaseUrl { get; set; }
        public string EventBusConnection { get; set; }
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}