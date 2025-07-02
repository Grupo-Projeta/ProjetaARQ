using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using ProjetaARQ.Features.WordExport.MVVM;
using System.Windows.Media;
using System.Windows.Controls;
using ProjetaARQ.Core.UI;
using System.Windows.Controls.Primitives;

namespace ProjetaARQ.Features.WordExport.Services
{
    internal class DeleteDropHandler : ObservableObject, IDropTarget, IDragSource
    {
        // Uma referência à lista de regras principal
        private readonly ObservableCollection<RuleCardModel> _collection;

        private bool _addBorder = true;
        public bool AddBorder
        {
            get => _addBorder;
            set
            {
                if (_addBorder != value)
                {
                    _addBorder = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _deleteBorder = false;
        public bool DeleteBorder
        {
            get => _deleteBorder;
            set
            {
                if (_deleteBorder != value)
                {
                    _deleteBorder = value;
                    OnPropertyChanged();
                }
            }
        }

        #region TargetInterface
        internal DeleteDropHandler(ObservableCollection<RuleCardModel> collection)
        {
            _collection = collection;
        }

        public void DragEnter(IDropInfo dropInfo)
        {

        }

        public void DragLeave(IDropInfo dropInfo)
        {
            
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is RuleCardModel)
                dropInfo.Effects = DragDropEffects.Move;

        }

        public void Drop(IDropInfo dropInfo)
        {
            // Quando o item é solto aqui, ele é removido da coleção.
            RuleCardModel itemToRemove = dropInfo.Data as RuleCardModel;

            if (itemToRemove != null)
                _collection.Remove(itemToRemove);

            AddBorder = true;
            DeleteBorder = false;
        }

        public void DropHint(IDropHintInfo dropHintInfo)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region SourceInterface
        public void StartDrag(IDragInfo dragInfo)
        {
            AddBorder = false;
            DeleteBorder = true;
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            return true;
        }

        public void Dropped(IDropInfo dropInfo)
        {

        }

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {

        }

        public void DragCancelled()
        {

        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
