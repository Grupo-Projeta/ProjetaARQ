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
                editor.ReplaceTextInContentControl("Nome Do Projeto", "ESCOLA CEMPA");
                editor.ReplaceTextInContentControl("Projetista", "Ricardo Braga");
                editor.ReplaceTextInContentControl("Lista De Desenhos", "TITULO TESTE");
                editor.ReplaceTextInContentControl("TITULO DESENHO", "PLANTA BAIXA TESTE");
                editor.ReplaceTextInContentControl("MES ANO", "JUNHO/2025");
                editor.ReplaceTextInContentControl("Nome Obra", "NOME DA OBRA MODIFICADO");
                editor.DeleteParagraphByContentControlTag("AdotadasDois");
                editor.DeleteSectionByTag("AdotadasUm");

                editor.ReplaceImage("Elab", "ProjetaARQ.Features.WordExport.Resources.diamante.png");
                editor.ReplaceImage("consorcio", "ProjetaARQ.Features.WordExport.Resources.diamante.png");
            }


            fileHandler.OpenFile(savePath);

            return Result.Succeeded;
        }
    }
}
