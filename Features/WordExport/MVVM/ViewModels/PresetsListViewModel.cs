using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.WordExport.MVVM.ViewModels.Actions;

namespace ProjetaARQ.Features.WordExport.MVVM.ViewModels
{
    internal class PresetsListViewModel : ObservableObject
    {
        private bool _exemplo;
        public bool Exemplo
        {
            get => _exemplo;
            set => SetProperty(ref _exemplo, value);
        }


        public PresetsListViewModel()
        {
            
        }
    }
}
