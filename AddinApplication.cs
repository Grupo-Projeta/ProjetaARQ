using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using ProjetaARQ.Revit.UI;
using ProjetaARQ.Revit.UI.Interfaces;

namespace ProjetaARQ
{
    public class AddinApplication : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            // 1 - Carrega os pacotes externos na pasta addins/ProjetaARQ/
            ConfigureAssemblyResolve(application);

            // 2 - Inicializa o plugin na UI
            IRibbonManager ribbonManager = new RibbonManager();
            IDockablePaneManager dockablePaneManager = new DockablePaneManager(application);

            UIBuilder uiBuilder = new UIBuilder(application, ribbonManager, dockablePaneManager);
            uiBuilder.Build();


            return Result.Succeeded;
        }


        public Result OnShutdown(UIControlledApplication application)
            => Result.Succeeded;


        /// <summary>
        /// Define onde estarão as dependencias e pacotes externos
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureAssemblyResolve(UIControlledApplication app)
        {
            // 1. Pega a versão do Revit dinamicamente (ex: "2024")
            string revitVersion = app.ControlledApplication.VersionNumber;

            // 2. Pega o caminho da pasta do add-in 
            string addinPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Autodesk",
                "Revit",
                "Addins",
                revitVersion,
                "ProjetaARQ"
            );

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string assemblyName = new AssemblyName(args.Name).Name;
                string assemblyPath = Path.Combine(addinPath, $"{assemblyName}.dll");

                return File.Exists(assemblyPath)
                    ? Assembly.LoadFrom(assemblyPath)
                    : null;
            };
        }
    }
}
