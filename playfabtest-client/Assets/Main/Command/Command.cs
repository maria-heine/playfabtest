using System;

public abstract class CommandArgs
{
    public Action<CommandResult> Callback { get; set; }
}

public enum CommandResultType { Success, Error }

public abstract class CommandResult
{
    public CommandResult() { }
    public CommandResultType CommandResultType { get; set; }
    public string ErrorMessage { get; set; }
}

public abstract class Command
{
    protected Type _expectedCommandArgsType;
    protected CommandArgs _args;

    public Command() { }        

    public Action<CommandResult> Callback { get; set; }

    public virtual void Initialize(CommandArgs args)
    {
        SetExpectedTypes();
        CheckArgs(args);
        _args = args;
        Callback = args.Callback;
    }

    public abstract void SetExpectedTypes();
    public abstract void Execute();

    private void CheckArgs(CommandArgs args)
    {
        if (args.GetType() != _expectedCommandArgsType)
        {
            // Ummm, use string builder or custom exception?
            throw new ArgumentException("Incorrect command args type: " +
                args.GetType() + " passed to: " + this.GetType());
        }
        else if (args.Callback == null)
        {
            throw new ArgumentException("Initialized command of " + GetType() + " type is missing a Callback");
        }
    }
}
