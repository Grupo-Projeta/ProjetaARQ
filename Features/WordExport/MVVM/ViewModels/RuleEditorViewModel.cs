using GongSolutions.Wpf.DragDrop;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.FamiliesPanel.MVVM;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Features.WordExport.Services.GongHandlers;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class RuleEditorViewModel : ObservableObject
    {
        public IDropTarget DeleteDropHandler { get; }
        public IDragSource RuleDragHandler { get; }
        public IDropTarget RuleDropHandler { get; }



        private readonly UndoRedoManager _undoRedoManager = new UndoRedoManager();
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }

        public ObservableCollection<RuleCardViewModel> RulesList { get; set; } = new ObservableCollection<RuleCardViewModel>();
        public RelayCommand AddRuleCommand { get; }

        public RuleEditorViewModel()
        {
            DeleteDropHandler = new DeleteDropHandler(RulesList, _undoRedoManager);
            RuleDragHandler = new RuleDragHandler(this);
            RuleDropHandler = new RuleDropHandler(_undoRedoManager);

            UndoCommand = new RelayCommand(p => _undoRedoManager.Undo());
            RedoCommand = new RelayCommand(p => _undoRedoManager.Redo());

            AddRuleCommand = new RelayCommand(x => AddRule());
            RulesList.Add(new RuleCardViewModel(_undoRedoManager));
            
        }

        private void AddRule()
        {
            var ruleToAdd = new RuleCardViewModel(_undoRedoManager);
            var command = new AddRuleCommand(RulesList, ruleToAdd);
            _undoRedoManager.Do(command);
        }
    }
}
