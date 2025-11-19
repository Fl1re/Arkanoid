using Cysharp.Threading.Tasks;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    public async UniTask<Ball> CreateBall(Vector2 position, Vector2 direction,LevelSettings levelSettings)
    {
        Ball newBall = Instantiate(ballPrefab, position, Quaternion.identity);
        newBall.SetInitialSpeed(levelSettings.ballInitialSpeed);
        newBall.Launch(direction);
        return newBall;
    }
}
