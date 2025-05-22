using ControlzEx.Theming;
using Microsoft.WindowsAPICodePack.Dialogs;
using ProjetaARQ.Core.UI;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ProjetaARQ.Features.FamiliesPanel.MVVM
{
    internal class FamiliesViewModel : ObservableObject
    {
        public ObservableCollection<FolderItem> SubFolders { get; set; } = new ObservableCollection<FolderItem>();

        private FamiliesView _familiesWindow;
        public FamiliesView FamiliesWindow
        {
            get => _familiesWindow;
            set
            {
                if (_familiesWindow != value)
                {
                    _familiesWindow = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _rootPath = "B:\\005. Implementação\\Arquitetura e Urbanismo\\Fernanda Farah\\01. Criação de Famílias\\02. Famílias para Clickup\\01. Portas";
        public string RootPath
        {
            get => _rootPath;
            set
            {
                if (_rootPath != value)
                {
                    _rootPath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _currentPath;
        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                if (_currentPath != value)
                {
                    _currentPath = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (_isDarkTheme != value)
                {
                    _isDarkTheme = value;
                    OnPropertyChanged();

                    string theme = value ? "Dark.Crimson" : "Light.Crimson";
                    ThemeManager.Current.ChangeTheme(_familiesWindow, theme);

                    _eggIcon =  IsDarkTheme ? "eggprojeta-darktheme.png" : "eggprojeta.png";
                    OnPropertyChanged(nameof(EggIcon));
                }
            }
        }

        private string _eggIcon = "eggprojeta.png";
        public string EggIcon
        {
            get => _eggIcon;
            set
            {
                if (_eggIcon != value)
                {
                    _eggIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand ChangePathCommand { get; }


        public FamiliesViewModel()
        {
            ChangePathCommand = new RelayCommand(_ => ChangePath(RootPath));
            GetDirectories();
        }

        public string ChangePath(string rootPath)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                InitialDirectory = rootPath,
                Title = "Selecione a pasta raiz",
                EnsurePathExists = true,
                AllowNonFileSystemItems = false,
                AddToMostRecentlyUsedList = false,
                ShowPlacesList = true,
                NavigateToShortcut = true,
                // Aqui está a chave: desabilita o botão "Nova Pasta"
                AllowPropertyEditing = false
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                CurrentPath = dialog.FileName;
                GetDirectories();
                return dialog.FileName;
            }

            return null;
        }

        public void GetDirectories()
        {
            if (Directory.Exists(CurrentPath))
            {
                string[] dirs = Directory.GetDirectories(CurrentPath);
                SubFolders.Clear();

                foreach (string dir in dirs)
                {
                    FolderItem folder = new FolderItem
                    {
                        Name = Regex.Replace(Path.GetFileName(dir), @"^\d+\.\s*", ""),
                        Path = dir
                    };

                    SubFolders.Add(folder);
                }
                    
            }
            OnPropertyChanged(nameof(SubFolders));
        }
    }
}
