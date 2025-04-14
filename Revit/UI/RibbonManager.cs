using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using ProjetaARQ.Core.Services;
using ProjetaARQ.Revit.UI.Interfaces;

namespace ProjetaARQ.Revit.UI
{
    internal class RibbonManager : IRibbonManager
    {
        private readonly string _thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

        public PushButton AddPushButton(
            string nomeInterno,
            string nomeExibido,
            string nomeClasse,
            RibbonPanel painel,
            string dica,
            string nomeImagem,
            bool enableOption = false)
        {

            PushButtonData pushButtonData = new PushButtonData(nomeInterno, nomeExibido, _thisAssemblyPath, nomeClasse);

            if (painel == null || pushButtonData == null)
            {
                Debug.WriteLine($"Não foi possível criar o botão {nomeInterno}");
                return null;
            }

            PushButton pushButton = painel.AddItem(pushButtonData) as PushButton;
            pushButton.Enabled = enableOption;
            pushButton.ToolTip = dica;

            // Cria a imagem do ícone
            BitmapImage bitmap = BitmapConverter.GetIcon(nomeImagem);

            // Define a imagem como o ícone do botão
            pushButton.LargeImage = bitmap;

            return pushButton;
        }

        public RibbonPanel CreatePanel(UIControlledApplication application, string nomePainel)
        {
            return null;
        }
    }
}
