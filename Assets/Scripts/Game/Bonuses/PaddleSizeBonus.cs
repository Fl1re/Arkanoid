public class PaddleSizeUpBonus : Bonus
{
    public override async void ApplyBonus(GameContext context)
    {
        await context.Paddle.AnimateSizeChange(2f, 0.5f);
    }
}