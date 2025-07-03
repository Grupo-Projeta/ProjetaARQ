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
    internal class DeleteRuleCommand : IUndoableCommand
    {
        ObservableCollection<RuleCardModel> _rulesList;
        RuleCardModel _ruleToDelete;
        int _oldIndex;

        public DeleteRuleCommand(ObservableCollection<RuleCardModel> rulesList, RuleCardModel ruleToDelete)
        {
            _rulesList = rulesList;
            _ruleToDelete = ruleToDelete;
            _oldIndex = _rulesList.IndexOf(ruleToDelete); // Para garantir a inserção no local antigo
        }

        public void Execute() => _rulesList.Remove(_ruleToDelete);

        public void Unexecute() => _rulesList.Insert(_oldIndex,_ruleToDelete);
    }
}
