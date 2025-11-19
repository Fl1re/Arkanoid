using System;
using UnityEngine;
using DG.Tweening;

public abstract class Bonus : MonoBehaviour, IBonus
{
    private float _fallSpeed;

    private void Awake()
    {
        _fallSpeed = GameContext.Instance.LevelSettings.bonusFallTime;
    }

    protected virtual void Start()
    {
        transform.DOMoveY(-10f, _fallSpeed).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Paddle>(out var paddle))
        {
            ApplyBonus(GameContext.Instance);
            Destroy(gameObject);
        }
    }

    public abstract void ApplyBonus(GameContext context);
}