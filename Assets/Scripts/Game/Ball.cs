using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private float _speed;
    private const float AdditionalSpeedOnStack = 0.2f;

    private Rigidbody2D _rb;

    public UnityEvent<Ball> OnLost = new();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        _rb.velocity = direction.normalized * _speed;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rb.velocity;
        velocity = PreventStacking(velocity);
        _rb.velocity = velocity.normalized * _speed;
    }

    private Vector2 PreventStacking(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.y) < 0.1f)
        {
            float additionalY = transform.position.y >= 0 ? -AdditionalSpeedOnStack : AdditionalSpeedOnStack;
            velocity.y = additionalY;
        }

        if (Mathf.Abs(velocity.x) < 0.1f)
        {
            float additionalX = transform.position.x >= 0 ? -AdditionalSpeedOnStack : AdditionalSpeedOnStack;
            velocity.x = additionalX;
        }
        return velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDestroyable>(out var destroyable))
        {
            destroyable.TakeDamage(1);
        }
        if (collision.gameObject.TryGetComponent<Paddle>(out var paddle))
        {
            float paddleHalfWidth = paddle.GetComponent<Collider2D>().bounds.extents.x;
            float hitFactor = (transform.position.x - paddle.transform.position.x) / paddleHalfWidth;
            Vector2 dir = new Vector2(hitFactor * 1.0f, 1f).normalized;
            _rb.velocity = dir * _speed;
        }
        else
        {
            _rb.velocity = _rb.velocity.normalized * _speed;
        }
        
        GameContext.Instance.AudioPlayer.PlayBallHit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BottomWall"))
        {
            OnLost.Invoke(this);
        }
    }

    public void SetInitialSpeed(float speed)
    {
        _speed = speed;
    }
}