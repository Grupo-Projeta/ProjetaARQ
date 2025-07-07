using ProjetaARQ.Features.WordExport.Enums;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels.Actions
{
    internal class ReplaceTextViewModel : ActionViewModelBase
    {

        private readonly UndoRedoManager _undoRedoManager;

        private EditModeType _selectedEditMode;
        public EditModeType SelectedEditMode
        {
            get => _selectedEditMode;
            set
            {
                if (_selectedEditMode != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<EditModeType>(
                        newSelection => _selectedEditMode = newSelection,    // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(SelectedEditMode)),
                        _selectedEditMode,                                   // O valor antigo
                        value                                                // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private DataSourceType _selectedDataSource;
        public DataSourceType SelectedDataSource
        {
            get => _selectedDataSource;
            set
            {
                if (_selectedDataSource != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<DataSourceType>(
                        newSelection => _selectedDataSource = newSelection,     // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(SelectedDataSource)),
                        _selectedDataSource,                                    // O valor antigo
                        value                                                   // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private ConditionType _selectedConditon;
        public ConditionType SelectedCondition
        {
            get => _selectedConditon;
            set
            {
                if (_selectedConditon != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<ConditionType>(
                        newSelection => _selectedConditon = newSelection,       // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(SelectedCondition)),
                        _selectedConditon,                                      // O valor antigo
                        value                                                   // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private string _textoToReplace;
        public string TextToReplace
        {
            get => _textoToReplace;
            set
            {
                if (_textoToReplace != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                        oldText => _textoToReplace = oldText,     // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(TextToReplace)),
                        _textoToReplace,                                    // O valor antigo
                        value                                               // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private string _newText;
        public string NewText
        {
            get => _newText;
            set
            {
                if (_newText != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                        newText => _newText = newText,     // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(NewText)),
                        _newText,                                    // O valor antigo
                        value                                               // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private string _contentTag;
        public string ContentTag
        {
            get => _contentTag;
            set
            {
                if (_contentTag != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                       contentTagParameter => _contentTag = contentTagParameter,     // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(ContentTag)),
                        _contentTag,                                    // O valor antigo
                        value                                               // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private string _revitParameterName;
        public string RevitParameterName
        {
            get => _revitParameterName;
            set
            {
                if (_revitParameterName != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                       revitParameter => _revitParameterName = revitParameter,     // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(RevitParameterName)),
                        _revitParameterName,                                    // O valor antigo
                        value                                               // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }


        internal ReplaceTextViewModel(UndoRedoManager undoRedoManager)
        {
            _undoRedoManager = undoRedoManager;
        }
    }
}
