using UnityEngine;

[CreateAssetMenu(fileName = "GameContext", menuName = "Arkanoid/GameContext")]
public class GameContext : ScriptableObject
{
    private static GameContext _instance;
    public static GameContext Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GameContext>("GameContext");
            }
            return _instance;
        }
    }

    public Paddle Paddle { get; set; }
    public GameStateTracker StateTracker { get; private set; }
    public BallFactory BallFactory { get; private set; }
    public LevelSettings LevelSettings { get; private set; }
    public IAudioPlayer AudioPlayer { get; private set; }

    public void Initialize(Paddle paddle, GameStateTracker tracker, BallFactory ballFactory, LevelSettings settings,IAudioPlayer audioPlayer)
    {
        Paddle = paddle;
        StateTracker = tracker;
        BallFactory = ballFactory;
        LevelSettings = settings;
        AudioPlayer = audioPlayer;
    }
}