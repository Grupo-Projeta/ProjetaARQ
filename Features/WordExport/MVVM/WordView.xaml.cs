using MahApps.Metro.Controls;

namespace ProjetaARQ.Features.WordExport.MVVM
{
    /// <summary>
    /// Interaction logic for DevView.xaml
    /// </summary>
    public partial class WordView : MetroWindow
    {
        internal WordView(WordViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
