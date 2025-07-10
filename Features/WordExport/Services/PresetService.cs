using ProjetaARQ.Features.WordExport.Models;
using System.IO;
using System.Text.Json;


namespace ProjetaARQ.Features.WordExport.Services
{
    internal class PresetService
    {
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
    }
}
