﻿using GongSolutions.Wpf.DragDrop;
using ProjetaARQ.Core.UI;
using ProjetaARQ.Features.WordExport.MVVM.ViewModels;
using System;
using System.Windows;

namespace ProjetaARQ.Features.WordExport.Services.GongHandlers
{
    internal class RuleDragHandler : ObservableObject, IDragSource
    {

        #region Properties
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

        private readonly RuleEditorViewModel _viewModel;

        #endregion



        public RuleDragHandler(RuleEditorViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        #region DragSourceInterface
        public bool CanStartDrag(IDragInfo dragInfo) => true;

        public void DragCancelled()
        {
            AddBorder = true;
            DeleteBorder = false;
        }

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            AddBorder = true;
            DeleteBorder = false;
        }

        public void Dropped(IDropInfo dropInfo)
        {
            AddBorder = true;
            DeleteBorder = false;
        }

        public void StartDrag(IDragInfo dragInfo)
        {
            dragInfo.Effects = DragDropEffects.Move;
            dragInfo.Data = dragInfo.SourceItem; 

            AddBorder = false;
            DeleteBorder = true;
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
