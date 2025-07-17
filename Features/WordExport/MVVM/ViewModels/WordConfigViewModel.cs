using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.FamiliesPanel.MVVM;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class WordConfigViewModel : ObservableObject
    {
        #region Properties

        private Object _currentView;
        public Object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

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

        public RuleEditorViewModel RuleEditorVM { get; set; }
        public PresetsListViewModel PresetsListVM { get; set; }




        public RelayCommand RuleEditorViewCommand { get; }
        public RelayCommand PresetsListViewCommand { get; }

        public RelayCommand ToggleMenuCommand { get; }
        #endregion

        public WordConfigViewModel()
        {
            RuleEditorVM = new RuleEditorViewModel();
            PresetsListVM = new PresetsListViewModel();

            RuleEditorViewCommand = new RelayCommand(o =>
            {
                CurrentView = RuleEditorVM;
            });

            PresetsListViewCommand = new RelayCommand(o =>
            {
                CurrentView = PresetsListVM;
            });

            ToggleMenuCommand = new RelayCommand(x => ToggleMenu());
        }

        private void ToggleMenu()
        {
            IsMenuExpanded = !IsMenuExpanded;
        }
    }
}
