using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Abstracts
{
    public interface IChatCommandHandler
    {
        void Execute(IChatCommand command, object parameter = null);
    }
}
