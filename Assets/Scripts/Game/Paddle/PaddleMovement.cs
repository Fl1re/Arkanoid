using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _speed;
    private float _leftBound;
    private float _rightBound;
    private float _halfPaddleWidth;
    private float _targetX;

    public void Initialize(Rigidbody2D rb, float speed, float leftBound, float rightBound, float halfWidth)
    {
        _rb = rb;
        _speed = speed;
        _leftBound = leftBound;
        _rightBound = rightBound;
        _halfPaddleWidth = halfWidth;
    }

    public void UpdateSpeed(float speed) { _speed = speed; }
    public void UpdateHalfWidth(float halfWidth) { _halfPaddleWidth = halfWidth; }

    public void SetTargetX(float targetX)
    {
        _targetX = Mathf.Clamp(targetX, _leftBound + _halfPaddleWidth, _rightBound - _halfPaddleWidth);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = Vector2.zero;
        float deltaX = _targetX - transform.position.x;
        velocity.x = deltaX * _speed;
        _rb.velocity = velocity;

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, _leftBound + _halfPaddleWidth, _rightBound - _halfPaddleWidth);
        transform.position = position;
    }
}