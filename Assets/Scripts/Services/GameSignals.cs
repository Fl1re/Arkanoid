using Doozy.Runtime.Signals;

public static class GameSignals
{
    public static readonly SignalStream Victory = SignalsService.GetStream("Game", "Victory");
    public static readonly SignalStream Defeat  = SignalsService.GetStream("Game", "Defeat");
}