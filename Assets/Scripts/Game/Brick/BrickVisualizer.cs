using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class BrickVisualizer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Sprite[] _damageSprites;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprites(Sprite[] states)
    {
        _damageSprites = states;
    }

    public async UniTask AnimateDamage()
    {
        await transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f).SetEase(Ease.InOutBounce).ToUniTask();
    }

    public void UpdateSprite(int currentHealth, int maxHealth)
    {
        if (_damageSprites == null || _damageSprites.Length == 0)
            return;

        int totalStates = _damageSprites.Length;
        int spriteIndex;

        if (maxHealth <= totalStates)
        {
            int healthLost = maxHealth - currentHealth;

            spriteIndex = Mathf.Clamp(healthLost, 0, totalStates - 1);
        }
        else
        {
            float healthPercent = (float)currentHealth / maxHealth;

            spriteIndex = Mathf.FloorToInt((1f - healthPercent) * totalStates);

            spriteIndex = Mathf.Clamp(spriteIndex, 0, totalStates - 1);
        }

        _spriteRenderer.sprite = _damageSprites[spriteIndex];
    }
}