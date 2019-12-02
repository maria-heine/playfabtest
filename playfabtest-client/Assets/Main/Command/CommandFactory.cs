public class CommandFactory
{
    public static CommandFactory Instance { get; } = new CommandFactory();

    public TCommand CreateCommand <TCommand>(CommandArgs args)
        where TCommand : Command, new()
    {
        TCommand command = new TCommand();
        command.Initialize(args);
        return command;
    }
}
