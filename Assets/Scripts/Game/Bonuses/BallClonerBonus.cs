using UnityEngine;

public class BallClonerBonus : Bonus
{
    public override void ApplyBonus(GameContext context)
    {
        int extraCount = context.LevelSettings.multiBallExtraCount;
        for (int i = 0; i < extraCount; i++)
        {
            Vector2 randomDir = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
            context.StateTracker.SpawnBall(context.Paddle.transform.position + new Vector3(0, 1f, 0), randomDir);
        }
    }
}