using System;
using System.Collections.ObjectModel;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using ProjetaARQ.Features.WordExport.MVVM;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.WordExport.Services.UndoableCommands;

namespace ProjetaARQ.Features.WordExport.Services.GongHandlers
{
    internal class DeleteDropHandler : ObservableObject, IDropTarget
    {
        // Uma referência à lista de regras principal
        private readonly ObservableCollection<RuleCardModel> _collection;
        private readonly UndoRedoManager _undoRedoManager;

        internal DeleteDropHandler(ObservableCollection<RuleCardModel> collection, UndoRedoManager undoRedoManager)
        {
            _collection = collection;
            _undoRedoManager = undoRedoManager;
        }

        #region TargetInterface
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

            if (itemToRemove == null)
                return;
            
            var command = new DeleteRuleCommand(_collection, itemToRemove);
            _undoRedoManager.Do(command);
        }

        public void DropHint(IDropHintInfo dropHintInfo)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
