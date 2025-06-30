using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.FamiliesPanel.MVVM;

namespace ProjetaARQ.Features.WordExport.MVVM
{
    internal class RuleEditorViewModel : ObservableObject
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

        public ObservableCollection<RuleCardModel> RulesList { get; set; } = new ObservableCollection<RuleCardModel>();

        public RelayCommand ToggleMenuCommand { get; }

        public RuleEditorViewModel()
        {
            ToggleMenuCommand = new RelayCommand(x => ToggleMenu());
            RulesList.Add(new RuleCardModel { });
            RulesList.Add(new RuleCardModel { });
            RulesList.Add(new RuleCardModel { });
            
        }

        private void ToggleMenu()
        {
            IsMenuExpanded = !IsMenuExpanded;
        }
    }
}
