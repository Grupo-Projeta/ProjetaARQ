using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetaARQ.Features.WordExport.Interfaces
{
    public interface IUndoableCommand
    {
        void Execute();   // O que fazer
        void Unexecute(); // Como desfazer
    }
}
