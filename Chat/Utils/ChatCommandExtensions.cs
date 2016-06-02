using Commands.Abstracts;

namespace Utils
{
    public static class ChatCommandExtensions
    {
        public static void Execute(this IChatCommand command, IChatCommandsResolver resolver, object parameter = null)
        {
            var handler = resolver.Resolve(command);
            handler.Execute(command, parameter);
        }
    }
}