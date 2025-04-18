using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetaARQ.Features.FamiliesPanel.MVVM
{
    /// <summary>
    /// Interaction logic for FamiliesView.xaml
    /// </summary>
    public partial class FamiliesView : Page, IDockablePaneProvider
    {
        internal FamiliesView(FamiliesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.VisibleByDefault = false;
            data.FrameworkElement = (FrameworkElement)this; // Define a UI que será exibida no painel
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right, // Define a posição do painel
                MinimumWidth = 300
            };
        }
    }
}
