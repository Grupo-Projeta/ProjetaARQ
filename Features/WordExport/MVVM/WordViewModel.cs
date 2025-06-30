using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.FamiliesPanel.MVVM;

namespace ProjetaARQ.Features.WordExport.MVVM
{
    internal class WordViewModel : ObservableObject
    {
        private bool _isMenuExpanded = false;
        public bool IsMenuExpanded
        {
            get => _isMenuExpanded;
            set
            {
                if (_isMenuExpanded != value)
                {
                    _isMenuExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand ToggleMenuCommand { get; }

        public WordViewModel()
        {
            ToggleMenuCommand = new RelayCommand(x => ToggleMenu());
        }

        private void ToggleMenu()
        {
            IsMenuExpanded = !IsMenuExpanded;
        }
    }
}
