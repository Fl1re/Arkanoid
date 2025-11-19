using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class GameStateTracker : MonoBehaviour
{
    private List<Ball> _activeBalls = new();
    private List<Brick> _bricks = new();
    private int _remainingBricks;

    public void Initialize(List<Brick> bricks)
    {
        _bricks = bricks;
        _remainingBricks = _bricks.Count;

        foreach (var brick in _bricks)
        {
            brick.onDestroyed.AddListener(OnBrickDestroyed);
        }
    }

    public async UniTask SpawnBall(Vector2 position, Vector2 direction)
    {
        var ball = await gameObject.GetComponent<BallFactory>().CreateBall(position, direction, GameContext.Instance.LevelSettings);
        ball.OnLost.AddListener(OnBallLost);
        _activeBalls.Add(ball);
    }

    private void OnBrickDestroyed(Brick brick)
    {
        _bricks.Remove(brick);
        _remainingBricks--;
        if (_remainingBricks <= 0)
        {
            GameSignals.Victory.SendSignal();
        }
    }

    private void OnBallLost(Ball lostBall)
    {
        _activeBalls.Remove(lostBall);
        Destroy(lostBall.gameObject);

        if (_activeBalls.Count == 0)
        {
            GameSignals.Defeat.SendSignal();
        }
    }
}