using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using ProjetaARQ.Revit.UI.Interfaces;

namespace ProjetaARQ.Revit.UI
{
    internal class UIBuilder
    {
        private readonly string _tabName = "ProjetaARQ";

        private readonly UIControlledApplication _app;
        private readonly IRibbonManager _ribbonManager;

        public UIBuilder(UIControlledApplication app, IRibbonManager ribbonManager)
        {
            _app = app;
            _ribbonManager = ribbonManager;
        }

        /// <summary>
        /// Cria os elementos na UI do revit, criando os itens e alocando na ordem
        /// </summary>
        public void Build()
        {
            #region Tab

            _app.CreateRibbonTab(_tabName);

            #endregion

            #region Panels

            RibbonPanel mainPanel = _ribbonManager.CreatePanel(_app, _tabName, "Main");
            
            #endregion

            #region Buttons



            #endregion

        }
    }
}
