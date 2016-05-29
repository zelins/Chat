using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Abstracts
{
    public interface IChatCommandsResolver
    {
        IChatCommandHandler Resolve(IChatCommand command);
    }
}
