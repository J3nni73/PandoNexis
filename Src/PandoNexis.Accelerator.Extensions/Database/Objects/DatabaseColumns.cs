﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Database.Objects
{
    public class DatabaseColumns
    {
        public string Name = string.Empty;
        public string Type = string.Empty;
        public string Attribute = string.Empty;
        public bool IsIdentity = false;
        public object Value { get; set; }

    }
}
