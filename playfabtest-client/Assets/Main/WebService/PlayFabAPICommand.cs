using PlayFab;

public class HttpErrorCommandResult : CommandResult { }

public abstract class PlayFabAPICommand : Command
{
    protected virtual void OnHttpError(PlayFabError error)
    {
        Callback?
            .Invoke(new HttpErrorCommandResult
            {
                CommandResultType = CommandResultType.Error,
                ErrorMessage = error.ErrorMessage
            });
    }
}
