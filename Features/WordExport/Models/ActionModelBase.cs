using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ProjetaARQ.Features.WordExport.Enums;

namespace ProjetaARQ.Features.WordExport.Models
{

    [JsonDerivedType(typeof(ReplaceTextActionModel), typeDiscriminator: "ReplaceText")]
    public abstract class ActionModelBase
    {
        public string TargetTag { get; set; }
        public string CheckBoxConditionText { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExecuteConditionType ExecuteCondition { get; set; }
    }
}
