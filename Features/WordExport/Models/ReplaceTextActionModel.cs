using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetaARQ.Features.WordExport.Enums;

namespace ProjetaARQ.Features.WordExport.Models
{
    internal class ReplaceTextActionModel : ActionModelBase
    {
        public ReplaceTextModeType EditMode { get; set; }

        public string TextToReplace { get; set; }

        public DataSourceType DataSource { get; set; }

        public string ReplacementValue { get; set; }
    }
}
