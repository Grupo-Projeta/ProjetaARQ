using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ProjetaARQ.Features.WordExport.MVVM
{
    public partial class RuleCard : UserControl
    {
        public RuleCard()
        {
            InitializeComponent();
            // Altura inicial do cartão
            this.Height = 150; 
        }

        // --- Lógica de Redimensionamento ---

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            // Captura o mouse para garantir que o redimensionamento seja fluido
            (sender as Thumb)?.CaptureMouse();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            // 'this' se refere à instância do RuleCard que está sendo arrastada
            double newHeight = this.ActualHeight + e.VerticalChange;
            if (newHeight >= this.MinHeight && newHeight <= this.MaxHeight)
            {
                this.Height = newHeight;
            }
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            // Libera o mouse ao final da operação
            (sender as Thumb)?.ReleaseMouseCapture();
        }
    }
}