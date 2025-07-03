using ProjetaARQ.Features.WordExport.Interfaces;
using ProjetaARQ.Features.WordExport.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.Services.UndoableCommands
{
    internal class MoveRuleCommand : IUndoableCommand
    {
        ObservableCollection<RuleCardModel> _rulesList;
        int _oldIndex;
        int _newIndex;

        public MoveRuleCommand(ObservableCollection<RuleCardModel> rulesList, int oldIndex, int newIndex)
        {
            _rulesList = rulesList;
            _oldIndex = oldIndex;
            _newIndex = newIndex;
        }

        public void Execute() => _rulesList.Move(_oldIndex, _newIndex);

        public void Unexecute() => _rulesList.Move(_newIndex, _oldIndex);
    }
}
