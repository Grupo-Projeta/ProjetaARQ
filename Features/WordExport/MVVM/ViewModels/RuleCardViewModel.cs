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

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class RuleCardViewModel : ObservableObject
    {

        private readonly UndoRedoManager _undoRedoManager;

        private string _ruleName;
        public string RuleName
        {
            get => _ruleName;
            set
            {
                if (_ruleName != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                        newText => _ruleName = newText, // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(RuleName)),
                        _ruleName,                        // O valor antigo
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
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public RuleCardViewModel(UndoRedoManager undoRedoManager)
        {
            _undoRedoManager = undoRedoManager;
        }
    }
}
