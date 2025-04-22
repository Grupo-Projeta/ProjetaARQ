using Autodesk.Revit.UI;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

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

        private void ThemeToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;

            if (toggleSwitch == null)
                return;
            
            if (toggleSwitch.IsOn)
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Crimson");
                return;
            }

            ThemeManager.Current.ChangeTheme(this, "Light.Crimson");
        }
    }
}
