using ProjetaARQ.Features.WordExport.Enums;
using System.Collections.Generic;

namespace ProjetaARQ.Features.WordExport.Models
{
    public class RuleModel
    {
        public string Name { get; set; }
        public ConditionType Condition { get; set; }
        public RuleActionType Action { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
