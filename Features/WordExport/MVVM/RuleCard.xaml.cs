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
    }
}