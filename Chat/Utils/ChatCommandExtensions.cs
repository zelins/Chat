using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands.Abstracts;

namespace Utils
{
    public static class ChatCommandExtensions
    {
        public static void Execute(this IChatCommand command, IChatCommandsResolver resolver)
        {
            var handler = resolver.Resolve(command);
            handler.Execute(command);
        }
    }
}
