namespace SignalRDemo.Models
{
    public interface IGameState
    {
        Game GameInfo { get; }

        Player CreatePlayer(string userName);
        Player GetPlayer(string userName);
    }
}