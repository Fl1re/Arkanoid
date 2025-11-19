using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private List<BonusStruct> bonusPrefabs;

    public void SpawnBonus(Vector2 position, LevelSettings.BonusType type)
    {
        Instantiate(bonusPrefabs.First(b => b.type == type).bonusPrefab, position, Quaternion.identity);
    }
    
    
    [Serializable]
    private struct BonusStruct
    {
        public LevelSettings.BonusType type;
        public Bonus bonusPrefab;
    }
}