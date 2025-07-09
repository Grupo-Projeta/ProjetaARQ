using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.Models
{
    internal class PresetModel
    {
        public string PresetName { get; set; }
        public string WordTemplatePath { get; set; }
        public List<RuleModel> Rules { get; set; } = new List<RuleModel>();
    }
}
