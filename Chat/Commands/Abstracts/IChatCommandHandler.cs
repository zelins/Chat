namespace Commands.Abstracts
{
    public interface IChatCommandHandler
    {
        void Execute(IChatCommand command, object parameter = null);
    }
}