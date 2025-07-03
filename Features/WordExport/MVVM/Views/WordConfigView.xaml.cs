using MahApps.Metro.Controls;
using ProjetaARQ.Features.WordExport.MVVM.ViewModels;

namespace ProjetaARQ.Features.WordExport.MVVM.Views
{
    /// <summary>
    /// Interaction logic for DevView.xaml
    /// </summary>
    public partial class WordConfigView : MetroWindow
    {
        internal WordConfigView(WordConfigViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
