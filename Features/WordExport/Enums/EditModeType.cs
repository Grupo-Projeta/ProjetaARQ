using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.Enums
{
    public enum EditModeType
    {
        [Description("Substituir tudo")]
        ReplaceAll,

        [Description("Substituir parte")]
        ReplaceIn,

        [Description("Deletar Seção")]
        DeletarSecao
    }
}
