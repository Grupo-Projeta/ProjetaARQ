using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.FamiliesPanel.MVVM;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.MVVM
{
    internal class RuleEditorViewModel : ObservableObject
    {
        private readonly UndoRedoManager _undoRedoManager = new UndoRedoManager();
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }

        public ObservableCollection<RuleCardModel> RulesList { get; set; } = new ObservableCollection<RuleCardModel>();
        public RelayCommand AddRuleCommand { get; }

        public RuleEditorViewModel()
        {
            UndoCommand = new RelayCommand(p => _undoRedoManager.Undo());
            RedoCommand = new RelayCommand(p => _undoRedoManager.Redo());

            AddRuleCommand = new RelayCommand(x => AddRule());
            RulesList.Add(new RuleCardModel(_undoRedoManager));
            
        }

        private void AddRule()
        {
            var ruleToAdd = new RuleCardModel(_undoRedoManager);
            var command = new AddRuleCommand(RulesList, ruleToAdd);
            _undoRedoManager.Do(command);
        }
    }
}
