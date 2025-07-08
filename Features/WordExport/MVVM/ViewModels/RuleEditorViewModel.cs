using GongSolutions.Wpf.DragDrop;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.WordExport.MVVM.ViewModels.Actions;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Features.WordExport.Services.GongHandlers;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;
using System.Collections.ObjectModel;
using System.Data.Linq;

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
        public RelayCommand ExportCommand { get; }

        public RuleEditorViewModel()
        {
            DeleteDropHandler = new DeleteDropHandler(RulesList, _undoRedoManager);
            RuleDragHandler = new RuleDragHandler(this);
            RuleDropHandler = new RuleDropHandler(_undoRedoManager);

            UndoCommand = new RelayCommand(p => _undoRedoManager.Undo());
            RedoCommand = new RelayCommand(p => _undoRedoManager.Redo());

            AddRuleCommand = new RelayCommand(x => AddRule());
            ExportCommand = new RelayCommand(x => ExportWord());

            RulesList.Add(new RuleCardViewModel(_undoRedoManager));
            
        }

        private void AddRule()
        {
            var ruleToAdd = new RuleCardViewModel(_undoRedoManager);
            var command = new AddRuleCommand(RulesList, ruleToAdd);
            _undoRedoManager.Do(command);
        }

        private void ExportWord()
        {
            FileHandler fileHandler = new FileHandler();

            string savePath = fileHandler.GetSavePath();
            fileHandler.CreateNewFile(savePath);

            using (WordEditor editor = new WordEditor(savePath))
            {
                foreach (var ruleCard in RulesList)
                {
                    var ruleCardViewModel = ruleCard.CurrentActionViewModel as ReplaceTextViewModel;

                    editor.ReplaceTextInContentControl("Nome Do Projeto", ruleCardViewModel.NewText);
                }
            }


            fileHandler.OpenFile(savePath);
        }
    }
}
