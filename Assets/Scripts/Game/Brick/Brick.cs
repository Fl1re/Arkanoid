using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Brick : MonoBehaviour, IDestroyable
{
    private int _maxHealth;
    private int _health;
    private float _bonusDropChance;
    private List<LevelSettings.BonusType> _possibleBonuses = new();
    private BonusSpawner _bonusSpawner;
    private BrickVisualizer _visualizer;

    public bool IsDestroyed { get; private set; }
    public UnityEvent<Brick> onDestroyed = new();

    private void Awake()
    {
        _visualizer = GetComponent<BrickVisualizer>() ?? gameObject.AddComponent<BrickVisualizer>();
    }

    public async void TakeDamage(int damage)
    {
        _health -= damage;
        await _visualizer.AnimateDamage();
        if (_health <= 0)
        {
            DestroyBrick();
        }
        _visualizer.UpdateSprite(_health, _maxHealth);
    }

    private void DestroyBrick()
    {
        IsDestroyed = true;
        gameObject.SetActive(false);

        if (Random.value <= _bonusDropChance && _possibleBonuses.Count > 0)
        {
            var randomType = _possibleBonuses[Random.Range(0, _possibleBonuses.Count)];
            _bonusSpawner.SpawnBonus(transform.position, randomType);
        }

        onDestroyed.Invoke(this);
        GameContext.Instance.AudioPlayer.PlayBrickDestroy();
    }
    
    public void SetHealth(int maxHealth) { _maxHealth = maxHealth; _health = maxHealth; _visualizer.UpdateSprite(_health, _maxHealth);}
    public void SetBonusChance(float chance) { _bonusDropChance = chance; }
    public void SetBonuses(List<LevelSettings.BonusType> bonuses) { _possibleBonuses = new List<LevelSettings.BonusType>(bonuses); }
    public void SetBonusSpawner(BonusSpawner spawner) { _bonusSpawner = spawner; }
    public void SetStateSprites(Sprite[] states) { _visualizer.SetSprites(states); }
}