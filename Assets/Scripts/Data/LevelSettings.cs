using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Arkanoid/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [System.Serializable]
    public class BrickConfig
    {
        public int health = 1;
        public float bonusDropChance = 0.2f;
        public List<BonusType> possibleBonuses;
    }

    public enum BonusType
    {
        PaddleSizeUp,
        MultiBall
    }

    [Header("Конфигурация кирпичей")] 
    [SerializeField] private List<BrickConfig> bricks = new();
    public IReadOnlyList<BrickConfig> Bricks => bricks;
    [Space]
    
    [Header("Набор спрайтов-состояний для кирпичей")]
    [Tooltip("0 - целый кирпич, 4 - почти разрушенный")]
    public Sprite[] damageSprites;
    [Space]
   
    [Header("Настройки стартовой скорости и позиции")]
    public float ballInitialSpeed;
    public float paddleInitialSpeed;
    public Vector2 paddleStartPosition;
    public Vector2 ballStartPosition;
    [Space]
    
    [Header("Настройки бонусов")]
    public int multiBallExtraCount = 2;
    public float bonusFallTime = 5f;
}