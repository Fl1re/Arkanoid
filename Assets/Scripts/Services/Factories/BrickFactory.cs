using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class BrickFactory : MonoBehaviour
{
    [SerializeField] private Brick brickPrefab;
    [SerializeField] private BonusSpawner bonusSpawner;
    [SerializeField] private float horizontalSpacing = 0.1f;
    [SerializeField] private float verticalSpacing = 0.1f;
    [SerializeField] private float sideMargin = 0.5f;
    [SerializeField] private float topMargin = 1f;

    public async UniTask<List<Brick>> CreateBricksGrid(LevelSettings levelSettings)
    {
        List<Brick> createdBricks = new List<Brick>();

        int numBricks = levelSettings.Bricks.Count;
        if (numBricks == 0) return createdBricks;

        SpriteRenderer brickRenderer = brickPrefab.GetComponent<SpriteRenderer>();
        Vector2 brickSize = brickRenderer != null ? brickRenderer.bounds.size : Vector2.one;

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        float availableWidth = screenWidth - 2f * sideMargin;

        int maxCols = Mathf.FloorToInt((availableWidth + horizontalSpacing) / (brickSize.x + horizontalSpacing));
        if (maxCols < 1) maxCols = 1;

        int numCols = Mathf.Min(maxCols, numBricks);

        float gridWidth = numCols * brickSize.x + (numCols - 1) * horizontalSpacing;

        float leftX = -gridWidth / 2f + brickSize.x / 2f;

        float topY = screenHeight / 2f - topMargin - brickSize.y / 2f;

        int brickIndex = 0;
        foreach (var config in levelSettings.Bricks)
        {
            int row = brickIndex / numCols;
            int col = brickIndex % numCols;

            float x = leftX + col * (brickSize.x + horizontalSpacing);
            float y = topY - row * (brickSize.y + verticalSpacing);
            Vector3 position = new Vector3(x, y, 0f);

            Brick brick = Instantiate(brickPrefab, position, Quaternion.identity);
            brick.SetHealth(config.health);
            brick.SetBonusChance(config.bonusDropChance);
            brick.SetBonuses(config.possibleBonuses);
            brick.SetBonusSpawner(bonusSpawner);
            brick.SetStateSprites(levelSettings.damageSprites);

            createdBricks.Add(brick);

            brickIndex++;
        }

        return createdBricks;
    }
}