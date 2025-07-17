using ProjetaARQ.Features.WordExport.Enums;
using System.Text.Json.Serialization;

namespace ProjetaARQ.Features.WordExport.Models
{
    public class RuleCardModel
    {
        public string CardName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RuleActionType CardAction { get; set; }

        public ActionModelBase RuleCardValues { get; set; }
    }
}
