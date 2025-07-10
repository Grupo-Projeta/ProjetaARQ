using ProjetaARQ.Features.WordExport.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProjetaARQ.Features.WordExport.Models
{
    public class RuleModel
    {
        public string Name { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConditionType Condition { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RuleActionType Action { get; set; }

        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
