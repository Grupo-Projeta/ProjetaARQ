using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.Enums
{
    public enum ConditionType
    {
        [Description("Executar Sempre")]
        None,
        [Description("Executar Sempre")]
        AlwaysExecute,
        [Description("Sim/Não do Usuário")]
        CheckBox,

    }
}
