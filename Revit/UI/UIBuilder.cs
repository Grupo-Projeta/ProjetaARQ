using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace ProjetaARQ.Revit.UI
{
    internal class UIBuilder
    {
        private readonly string _tabName = "ProjetaARQ";

        UIControlledApplication _app;
        RibbonManager _ribbonManager;

        public UIBuilder(UIControlledApplication app)
        {
           _app = app;
        }

        public void Build()
        {
            _app.CreateRibbonTab(_tabName);

        }
    }
}
