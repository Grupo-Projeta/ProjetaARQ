using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.WordExport.Models;
using ProjetaARQ.Features.WordExport.MVVM.ViewModels.Actions;
using ProjetaARQ.Features.WordExport.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class PresetsListViewModel : ObservableObject
    {

        private PresetModel _selectedPreset;
        public PresetModel SelectedPreset
        {
            get => _selectedPreset;
            set => SetProperty(ref _selectedPreset, value);
        }

        // O serviço que sabe como encontrar e ler os ficheiros .json
        private readonly PresetService _presetService;

        // A coleção que será ligada à ListBox ou DataGrid na View
        public ObservableCollection<PresetModel> Presets { get; }

        public PresetsListViewModel()
        {
            _presetService = new PresetService();
            Presets = new ObservableCollection<PresetModel>();

            

            // Carrega todos os presets do disco ao iniciar
            LoadAllPresets();
        }


        /// <summary>
        /// Usa o PresetService para encontrar, carregar e exibir todos os presets.
        /// </summary>
        public void LoadAllPresets()
        {
            // Limpa a lista atual para garantir que não haja duplicatas
            Presets.Clear();

            // Pede ao serviço a lista de todos os caminhos de ficheiro .json
            var presetPaths = _presetService.GetAllPresetPaths();

            // Para cada caminho, carrega o preset e o adiciona à nossa coleção
            foreach (var path in presetPaths)
            {
                var preset = _presetService.LoadPreset(path);
                if (preset != null)
                {
                    Presets.Add(preset);
                }
            }

            // Opcional: seleciona o primeiro item da lista por padrão
            SelectedPreset = Presets.FirstOrDefault();
        }

        private void CreateNew()
        {
            // AQUI, no futuro, você irá comunicar-se com o ViewModel principal
            // para navegar para a tela do RuleEditorView com um preset em branco.
            // Ex: MainViewModel.NavigateToEditor(new PresetModel());
        }

        private void EditSelected()
        {
            // AQUI, você irá comunicar-se com o ViewModel principal
            // para navegar para a tela do RuleEditorView, passando o preset selecionado.
            // Ex: MainViewModel.NavigateToEditor(SelectedPreset);
        }

        private void DeleteSelected()
        {
            // AQUI, você usaria o PresetService para apagar o ficheiro .json
            // e depois removeria o item da coleção 'Presets'.
            // Ex: _presetService.DeletePreset(SelectedPreset);
            //      Presets.Remove(SelectedPreset);
        }
    }
}
