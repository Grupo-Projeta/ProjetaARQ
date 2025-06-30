using MahApps.Metro.Controls;

namespace ProjetaARQ.Features.WordExport.MVVM
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
