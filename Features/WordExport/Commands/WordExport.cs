using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ProjetaARQ.Features.Test.MVVM;
using ProjetaARQ.Revit.Base;
using ProjetaARQ.Revit.UI;

namespace ProjetaARQ.Features.WordExport.Commands
{
    [Transaction(TransactionMode.Manual)]
    internal class WordExport : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            

            return Result.Succeeded;
        }
    }
}
