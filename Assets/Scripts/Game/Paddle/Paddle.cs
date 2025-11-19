using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    private float _speed;
    private float _leftBound;
    private float _rightBound;
    private float _halfPaddleWidth;

    private Rigidbody2D _rb;
    private PaddleInput _input;
    private PaddleMovement _movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PaddleInput>() ?? gameObject.AddComponent<PaddleInput>();
        _movement = GetComponent<PaddleMovement>() ?? gameObject.AddComponent<PaddleMovement>();

        _movement.Initialize(_rb, _speed, _leftBound, _rightBound, _halfPaddleWidth);
        _input.OnTargetUpdated.AddListener(_movement.SetTargetX);
    }

    private void Start()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        _leftBound = -halfWidth;
        _rightBound = halfWidth;
        _halfPaddleWidth = GetComponent<Collider2D>().bounds.extents.x;

        _movement.Initialize(_rb, _speed, _leftBound, _rightBound, _halfPaddleWidth);
    }

    public async UniTask AnimateSizeChange(float newSize, float duration)
    {
        await transform.DOScaleX(newSize, duration).SetEase(Ease.InOutSine).ToUniTask();
        _halfPaddleWidth = GetComponent<Collider2D>().bounds.extents.x;
        _movement.UpdateHalfWidth(_halfPaddleWidth);
    }

    public void SetPaddleSpeed(float paddleSpeed)
    {
        _speed = paddleSpeed;
        _movement.UpdateSpeed(_speed);
    }
}