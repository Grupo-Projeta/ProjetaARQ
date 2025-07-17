﻿using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.FamiliesPanel.MVVM;
using ProjetaARQ.Features.WordExport.Enums;
using ProjetaARQ.Features.WordExport.MVVM.ViewModels.Actions;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class RuleCardViewModel : ObservableObject
    {
        private readonly UndoRedoManager _undoRedoManager;

        private string _ruleCardName;
        public string RuleCardName
        {
            get => _ruleCardName;
            set
            {
                if (_ruleCardName != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                        newText => _ruleCardName = newText, // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(RuleCardName)),
                        _ruleCardName,                        // O valor antigo
                        value                             // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<bool>(
                        boolValue => _isExpanded = boolValue,    // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(IsExpanded)),
                        _isExpanded,                                   // O valor antigo
                        value                                                // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private ActionViewModelBase _currentActionViewModel;
        public ActionViewModelBase CurrentActionViewModel
        {
            get => _currentActionViewModel;
            set => SetProperty(ref _currentActionViewModel, value);
        }

        private readonly Dictionary<RuleActionType, Func<ActionViewModelBase>> _actionFactory;

        private RuleActionType _selectedAction;
        public RuleActionType SelectedAction
        {
            get => _selectedAction;
            set
            {
                if (value == RuleActionType.Void)
                    return;

                if (_selectedAction != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<RuleActionType>(
                        newSelection => _selectedAction = newSelection,    // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(SelectedAction)),
                        _selectedAction,                                   // O valor antigo
                        value                                                // O novo valor
                    );
                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);

                    CurrentActionViewModel = _actionFactory[_selectedAction]();
                    OnPropertyChanged(nameof(CurrentActionViewModel));

                    IsExpanded = true;
                }
            }
        }


        public RuleCardViewModel(UndoRedoManager undoRedoManager)
        {
            _actionFactory = new Dictionary<RuleActionType, Func<ActionViewModelBase>>
            {
                //{ RuleActionType.InitialText, () => new ReplaceTextViewModel() },
                { RuleActionType.ReplaceText, () => new ReplaceTextViewModel(_undoRedoManager) },
                { RuleActionType.ReplaceImage, () => new ReplaceTextViewModel(_undoRedoManager) },
            };

            _undoRedoManager = undoRedoManager;
        }
    }
}
