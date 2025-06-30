using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ProjetaARQ.Features.WordExport.MVVM // Garanta que o namespace esteja correto
{
    public partial class RuleCard : UserControl
    {
        public RuleCard()
        {
            InitializeComponent();
            this.Height = 120; // Altura inicial
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = this.ActualHeight + e.VerticalChange;
            if (newHeight >= this.MinHeight && newHeight <= this.MaxHeight)
            {
                this.Height = newHeight;
            }
        }
    }
}