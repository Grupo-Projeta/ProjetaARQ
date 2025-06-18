using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ProjetaARQ.Features.Test.MVVM;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Revit.Base;
using ProjetaARQ.Revit.UI;

namespace ProjetaARQ.Features.WordExport.Commands
{
    [Transaction(TransactionMode.Manual)]
    internal class WordExport : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            FileHandler fileHandler = new FileHandler();

            string savePath = fileHandler.GetSavePath();
            fileHandler.CreateNewFile(savePath);

            using (WordEditor editor = new WordEditor(savePath))
            {

                editor.ReplaceTextInContentControl("teste", "funfando");
                editor.ReplaceTextInContentControl("projeteDeAguaFria", "Novo Texto");
                editor.ReplaceTextInContentControl("projetoArquitetonico", "projeto de urbanismo");
                editor.ReplaceImage("consorcio", "ProjetaARQ.Common.Images.sun-theme.png");
            }


            fileHandler.OpenFile(savePath);

            return Result.Succeeded;
        }
    }
}
