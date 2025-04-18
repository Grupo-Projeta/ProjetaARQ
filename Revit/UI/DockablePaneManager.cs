﻿using Autodesk.Revit.UI;
using ProjetaARQ.Features.FamiliesPanel.MVVM;
using ProjetaARQ.Revit.Base;
using ProjetaARQ.Revit.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Revit.UI
{
    internal class DockablePaneManager : IDockablePaneManager
    {
        internal readonly static DockablePaneId FamiliesPaneId = new DockablePaneId(new Guid("12299BB2-1EC7-4E84-8988-97A8C6339A44"));
        private readonly UIControlledApplication _app;

        internal DockablePaneManager(UIControlledApplication app)
        {
            _app = app;
        }
        
        public void RegisterPanes()
        {
            #region FamiliesPane

            var viewModel = new FamiliesViewModel();
            var view = new FamiliesView(viewModel);
            _app.RegisterDockablePane(FamiliesPaneId, "ShowRoom", view as IDockablePaneProvider);

            #endregion
        }

    }
}
