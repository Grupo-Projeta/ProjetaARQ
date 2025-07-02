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
    internal class AddRuleCommand : IUndoableCommand
    {
        private readonly ObservableCollection<RuleCardModel> _rulesList;
        private readonly RuleCardModel _newRule;

        public AddRuleCommand(ObservableCollection<RuleCardModel> rulesList, RuleCardModel newRule)
        {
            _rulesList = rulesList;
            _newRule = newRule;
        }
        public void Execute() => _rulesList.Add(_newRule);
        public void Unexecute() => _rulesList.Remove(_newRule);
    }
}
