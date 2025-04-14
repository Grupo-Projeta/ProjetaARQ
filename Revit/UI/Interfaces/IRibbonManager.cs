using Autodesk.Revit.UI;

namespace ProjetaARQ.Revit.UI.Interfaces
{
    public interface IRibbonManager
    {
        RibbonPanel CreatePanel(UIControlledApplication application, string nomePainel);

        PushButton AddPushButton(
            string nomeInterno, 
            string nomeExibido,
            string nomeClasse, 
            RibbonPanel painel,
            string dica, 
            string nomeImagem,
            bool enableOption = false);
    }
}
