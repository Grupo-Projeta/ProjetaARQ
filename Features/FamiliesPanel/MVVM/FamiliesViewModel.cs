using ControlzEx.Theming;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Revit.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.FamiliesPanel.MVVM
{
    internal class FamiliesViewModel : ObservableObject
    {

        private FamiliesView _familiesWindow;
        public FamiliesView FamiliesWindow
        {
            get => _familiesWindow;
            set
            {
                if (_familiesWindow != value)
                {
                    _familiesWindow = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (_isDarkTheme != value)
                {
                    _isDarkTheme = value;
                    OnPropertyChanged();

                    string theme = value ? "Dark.Crimson" : "Light.Crimson";
                    ThemeManager.Current.ChangeTheme(_familiesWindow, theme);

                    _eggIcon =  IsDarkTheme ? "eggprojeta-darktheme.png" : "eggprojeta.png";
                    OnPropertyChanged(nameof(EggIcon));
                }
            }
        }

        private string _eggIcon = "eggprojeta.png";
        public string EggIcon
        {
            get => _eggIcon;
            set
            {
                if (_eggIcon != value)
                {
                    _eggIcon = value;
                    OnPropertyChanged();
                }
            }
        }
             

        public FamiliesViewModel()
        {
        

            
        }
    }
}
