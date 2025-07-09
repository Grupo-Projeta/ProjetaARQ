using System.ComponentModel;
using ProjetaARQ.Features.WordExport.Enums;
using ProjetaARQ.Features.WordExport.Services;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels.Actions
{
    internal class ReplaceTextViewModel : ActionViewModelBase
    {

        private readonly UndoRedoManager _undoRedoManager;

        public string MyProperty { get; set; }

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

        private ConditionType _selectedConditon = ConditionType.None;
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

        private string _textBoxToReplace;
        public string TextBoxToReplace
        {
            get => _textBoxToReplace;
            set
            {
                if (_textBoxToReplace != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                        oldText => _textBoxToReplace = oldText,     // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(TextBoxToReplace)),
                        _textBoxToReplace,                                    // O valor antigo
                        value                                               // O novo valor
                    );

                    // Executa e registra o comando no gerenciador de Undo/Redo
                    _undoRedoManager.Do(command);
                }
            }
        }

        private string _replacementTextBox;
        public string ReplacementTextBox
        {
            get => _replacementTextBox;
            set
            {
                if (_replacementTextBox != value)
                {
                    // Cria um comando para esta mudança específica
                    var command = new ChangePropertyCommand<string>(
                        newText => _replacementTextBox = newText,     // A ação de como setar o valor
                        () => OnPropertyChanged(nameof(ReplacementTextBox)),
                        _replacementTextBox,                                    // O valor antigo
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

        private bool _isTextToReplaceCollapsed = true;
        public bool IsTextToReplaceCollapsed
        {
            get => _isTextToReplaceCollapsed;
            set => SetProperty(ref _isTextToReplaceCollapsed, value);
        }

        private bool _isNewTextCollapsed = true;
        public bool IsNewTextCollapsed
        {
            get => _isNewTextCollapsed;
            set => SetProperty(ref _isNewTextCollapsed, value);
        }

        private bool _isCheckBoxConditionCollapsed = true;
        public bool IsCheckBoxConditionCollapsed
        {
            get => _isCheckBoxConditionCollapsed;
            set => SetProperty(ref _isCheckBoxConditionCollapsed, value);
        }

        private bool _isTextBlockOptionsCollapsed = true;
        public bool IsTextBlockOptionsCollapsed
        {
            get => _isTextBlockOptionsCollapsed;
            set => SetProperty(ref _isTextBlockOptionsCollapsed, value);
        }

        private bool _isSetRevitParameterCollapsed = true;
        public bool IsSetRevitPararameterCollapsed
        {
            get => _isSetRevitParameterCollapsed;
            set => SetProperty(ref _isSetRevitParameterCollapsed, value);
        }

        private bool _isDataSourceEnabled = false;
        public bool IsDataSourceEnabled
        {
            get => _isDataSourceEnabled;
            set => SetProperty(ref _isDataSourceEnabled, value);
        }


        internal ReplaceTextViewModel(UndoRedoManager undoRedoManager)
        {
            _undoRedoManager = undoRedoManager;
            PropertyChanged += OnViewModelPropertyChanged;
        }



        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Verifica se a propriedade que mudou foi a que nos interessa
            if (e.PropertyName == nameof(SelectedEditMode))
            {
                switch (SelectedEditMode)
                {
                    case EditModeType.ReplaceAll:
                        IsTextToReplaceCollapsed = true;
                        IsDataSourceEnabled = true;
                        break;

                    case EditModeType.DeleteSection:
                        IsTextToReplaceCollapsed = true;
                        IsDataSourceEnabled = false;
                        IsTextBlockOptionsCollapsed = true;
                        IsSetRevitPararameterCollapsed = true;
                        IsNewTextCollapsed = true;
                        break;

                    case EditModeType.ReplaceIn:
                        IsTextToReplaceCollapsed = false;
                        IsDataSourceEnabled = true;
                        break;
                }

                return;
            }

            if(e.PropertyName == nameof(SelectedDataSource))
            {
                switch (SelectedDataSource)
                {
                    case DataSourceType.RevitParameter:
                        IsSetRevitPararameterCollapsed = false;
                        IsTextBlockOptionsCollapsed = true;
                        IsNewTextCollapsed = true;
                        break;

                    case DataSourceType.TextBlock:
                        IsSetRevitPararameterCollapsed = true;
                        IsTextBlockOptionsCollapsed = false;
                        IsNewTextCollapsed = true;
                        break;

                    case DataSourceType.WriteText:
                        IsSetRevitPararameterCollapsed = true;
                        IsTextBlockOptionsCollapsed = true;
                        IsNewTextCollapsed = false;
                        break;
                }

                return;
            }

            if (e.PropertyName == nameof(SelectedCondition))
            {
                switch (SelectedCondition)
                {
                    case ConditionType.None:
                        IsCheckBoxConditionCollapsed = true;
                        break;

                    case ConditionType.CheckBox:
                        IsCheckBoxConditionCollapsed = false;
                        break;
                }

                return;
            }
        }
    }
}
