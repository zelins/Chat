namespace Commands.Abstracts
{
    public interface IChatCommandsResolver
    {
        IChatCommandHandler Resolve(IChatCommand command);
    }
}