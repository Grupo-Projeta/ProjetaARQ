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
using System.Linq;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class RuleEditorViewModel : ObservableObject
    {
        public string _templatePath;
        private readonly PresetService _presetService = new PresetService();
        private readonly PresetModel _presetModel;

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



        public RuleEditorViewModel(PresetModel presetToEdit)
        {
            _presetModel = presetToEdit;

            foreach (RuleCardModel ruleModel in presetToEdit.RuleCards)
            {
                var ruleCard = new RuleCardViewModel(_undoRedoManager)
                {
                    RuleCardName = ruleModel.CardName,
                    SelectedAction = ruleModel.Action
                };

                OnPropertyChanged(nameof(ruleCard.SelectedAction));

                if (ruleCard.SelectedAction == Enums.RuleActionType.ReplaceText)
                {
                    ruleCard.CurrentActionViewModel = new ReplaceTextViewModel(_undoRedoManager)
                    {
                        SelectedCondition = ruleModel.Condition,
                        ContentTag = ruleModel.Parameters.ContainsKey("TargetTag") ? ruleModel.Parameters["TargetTag"] : string.Empty,
                        TextBoxToReplace = ruleModel.Parameters.ContainsKey("ToReplaceText") ? ruleModel.Parameters["ToReplaceText"] : string.Empty,
                        SelectedDataSource = Enum.TryParse(ruleModel.Parameters["DataSource"], out Enums.DataSourceType dataSource) ? dataSource : Enums.DataSourceType.Void,

                    };

                    ReplaceTextViewModel replaceTextViewModel = ruleCard.CurrentActionViewModel as ReplaceTextViewModel;

                    //if (replaceTextViewModel.SelectedDataSource == Enums.DataSourceType.WriteText)
                        replaceTextViewModel.TextBoxToReplace = ruleModel.Parameters.ContainsKey("ReplacementeText") ? ruleModel.Parameters["ReplacementeText"] : string.Empty;

                    replaceTextViewModel.SelectedEditMode = Enum.TryParse(ruleModel.Parameters["EditMode"], out Enums.ReplaceTextModeType editMode) ? editMode : Enums.ReplaceTextModeType.ReplaceAll;
                    
                    
                }

                RulesList.Add(ruleCard);
            }

            DeleteDropHandler = new DeleteDropHandler(RulesList, _undoRedoManager);
            RuleDragHandler = new RuleDragHandler(this);
            RuleDropHandler = new RuleDropHandler(_undoRedoManager);

            UndoCommand = new RelayCommand(p => _undoRedoManager.Undo());
            RedoCommand = new RelayCommand(p => _undoRedoManager.Redo());

            AddRuleCommand = new RelayCommand(x => AddRule());
            ExportCommand = new RelayCommand(x => ExportWord());
            SaveCommand = new RelayCommand(x => SaveCurrentPreset());
            
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
            string filePath = fileHandler.GetSavePath("Salvar em", "{Name}", ".json");
            if (string.IsNullOrEmpty(filePath))
                return; // O usuário cancelou
            

            var presetToSave = new PresetModel
            {
                PresetName = Path.GetFileNameWithoutExtension(filePath),
                WordTemplatePath = _templatePath 
            };

            foreach (var ruleCardVM in RulesList)
            {
                // Cria um novo RuleModel para ser salvo
                var ruleModel = new RuleCardModel
                {
                    // Mapeia as propriedades universais
                    CardName = ruleCardVM.RuleCardName,
                    CardAction = ruleCardVM.SelectedAction,
                };
                switch (ruleCardVM.CurrentActionViewModel)
                {
                    // Se for um ViewModel de "Substituir Texto"...
                    case ReplaceTextViewModel replaceVm:

                        ruleModel.RuleCardValues = new ReplaceTextActionModel
                        {
                            TargetTag = replaceVm.ContentTag,
                            EditMode = replaceVm.SelectedEditMode,
                            TextToReplace = replaceVm.TextBoxToReplace,
                            ReplacementValue = replaceVm.ReplacementText
                        };
                        break;
                }

                // Adiciona a regra completa (com a sua ação específica) à lista do preset
                presetToSave.RuleCards.Add(ruleModel);
            }
        }
    }
}
