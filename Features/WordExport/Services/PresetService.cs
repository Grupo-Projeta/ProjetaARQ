using ProjetaARQ.Features.WordExport.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace ProjetaARQ.Features.WordExport.Services
{
    internal class PresetService
    {
        private readonly string _presetsFolderPath = "C:\\Users\\Usuario\\Desktop\\Ricardo - ED\\ProjetaARQ\\testes";

        public void SavePreset(PresetModel preset, string filePath)
        {
            // Opções para que o JSON fique formatado e fácil de ler (indentado)
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Converte o objeto C# para uma string JSON
            string jsonString = JsonSerializer.Serialize(preset, options);

            // Salva a string no ficheiro
            File.WriteAllText(filePath, jsonString);
        }

        public PresetModel LoadPreset(string filePath)
        {
            if (!File.Exists(filePath))
                // Poderia lançar uma exceção ou retornar nulo, dependendo da sua lógica
                return null;


            // Lê todo o conteúdo do ficheiro
            string jsonString = File.ReadAllText(filePath);

            // Converte a string JSON de volta para um objeto C#
            return JsonSerializer.Deserialize<PresetModel>(jsonString);
        }

        public List<string> GetAllPresetPaths()
        {
            // Procura por todos os ficheiros que terminam em .json na nossa pasta de presets
            return Directory.GetFiles(_presetsFolderPath, "*.Json").ToList();
        }
    }
}
