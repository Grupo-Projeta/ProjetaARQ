﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace ProjetaARQ
{
    public class AddinApplication : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }



        public Result OnShutdown(UIControlledApplication application)
            => Result.Succeeded;

    }
}
