using UnityEngine;
using Cysharp.Threading.Tasks;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private GameContext gameContext;
    [SerializeField] private Paddle paddlePrefab;
    [SerializeField] private BallFactory ballFactory;
    [SerializeField] private BrickFactory brickFactory;
    [SerializeField] private WallBuilder wallBuilder;
    [SerializeField] private AudioPlayer audioPlayer;

    private GameStateTracker _stateTracker;

    private async void Awake()
    {
        _stateTracker = GetComponent<GameStateTracker>() ?? gameObject.AddComponent<GameStateTracker>();
        GameContext.Instance.Initialize(null, _stateTracker, ballFactory, levelSettings,audioPlayer);
        await InitializeLevel();
    }

    private async UniTask InitializeLevel()
    {
        wallBuilder.CreateWalls();
        var paddle = Instantiate(paddlePrefab, levelSettings.paddleStartPosition, Quaternion.identity);
        paddle.SetPaddleSpeed(levelSettings.paddleInitialSpeed);
        gameContext.Paddle = paddle;

        var bricks = await brickFactory.CreateBricksGrid(levelSettings);
        _stateTracker.Initialize(bricks);

        await _stateTracker.SpawnBall(levelSettings.ballStartPosition, new Vector2(0, 1));

        await UniTask.Delay(1000);
    }
}