using GongSolutions.Wpf.DragDrop;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.WordExport.Models;
using ProjetaARQ.Features.WordExport.MVVM.ViewModels.Actions;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Features.WordExport.Services.GongHandlers;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;
using System;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.IO;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class RuleEditorViewModel : ObservableObject
    {
        private readonly PresetService _presetService = new PresetService();
        string _templatePath;

        public IDropTarget DeleteDropHandler { get; }
        public IDragSource RuleDragHandler { get; }
        public IDropTarget RuleDropHandler { get; }



        private readonly UndoRedoManager _undoRedoManager = new UndoRedoManager();
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }

        public ObservableCollection<RuleCardViewModel> RulesList { get; set; } = new ObservableCollection<RuleCardViewModel>();
        public RelayCommand AddRuleCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand SaveCommand { get; }



        public RuleEditorViewModel()
        {
            DeleteDropHandler = new DeleteDropHandler(RulesList, _undoRedoManager);
            RuleDragHandler = new RuleDragHandler(this);
            RuleDropHandler = new RuleDropHandler(_undoRedoManager);

            UndoCommand = new RelayCommand(p => _undoRedoManager.Undo());
            RedoCommand = new RelayCommand(p => _undoRedoManager.Redo());

            AddRuleCommand = new RelayCommand(x => AddRule());
            ExportCommand = new RelayCommand(x => ExportWord());
            SaveCommand = new RelayCommand(x => SaveCurrentPreset());

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
            //FileHandler fileHandler = new FileHandler();

            //string savePath = fileHandler.GetSavePath();
            //fileHandler.CreateNewFile(savePath);

            //using (WordEditor editor = new WordEditor(savePath))
            //{
            //    foreach (var ruleCard in RulesList)
            //    {
            //        var ruleCardViewModel = ruleCard.CurrentActionViewModel as ReplaceTextViewModel;

            //        editor.ReplaceTextInContentControl(ruleCardViewModel.ContentTag, ruleCardViewModel.NewText);
            //    }
            //}


            //fileHandler.OpenFile(savePath);
        }


        private void SaveCurrentPreset()
        {
            FileServices fileHandler = new FileServices();
            string filePath = fileHandler.GetSavePath("Salvar em", "{Name}", ".Json");
            if (string.IsNullOrEmpty(filePath))
                return; // O usuário cancelou
            

            var presetToSave = new PresetModel
            {
                PresetName = Path.GetFileNameWithoutExtension(filePath),
                WordTemplatePath = _templatePath 
            };

            foreach (var ruleCardVM in RulesList)
            {
                var ruleModel = new RuleModel
                {
                    Name = ruleCardVM.RuleName,
                    Action = ruleCardVM.SelectedAction,
                };

                if (ruleCardVM.CurrentActionViewModel is ReplaceTextViewModel replaceTextVM)
                {
                    ruleModel.Condition = replaceTextVM.SelectedCondition;
                    ruleModel.Parameters["TargetTag"] = replaceTextVM.ContentTag;
                    ruleModel.Parameters["ReplacementText"] = replaceTextVM.ReplacementTextBox;
                }

                presetToSave.Rules.Add(ruleModel);
            }

            // 5. Usa o PresetService para salvar o objeto completo no ficheiro JSON
            try
            {
                _presetService.SavePreset(presetToSave, filePath);
                // Opcional: Mostrar uma mensagem de sucesso ao usuário
            }
            catch (Exception ex)
            {
                // Opcional: Mostrar uma mensagem de erro ao usuário
            }
        }
    }
}
