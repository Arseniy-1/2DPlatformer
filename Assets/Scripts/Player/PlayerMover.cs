using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;
    private float _horizontalDirection;

    private string _horizontalAxis = "Horizontal";
    private string _jumpButton = "Jump";
    private string _runningTrigger = "IsRunning";

    private bool _isGrounded => Physics2D.Raycast(_groundPoint.position, Vector2.down, 0.03f, _groundLayer);
    private bool _facingRight = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalDirection = Input.GetAxisRaw(_horizontalAxis);
        float currentHorizontalSpeed = _horizontalDirection * _speed;

        _rigidbody.velocity = new Vector2(currentHorizontalSpeed, _rigidbody.velocity.y);
        _animator.SetBool(_runningTrigger, currentHorizontalSpeed != 0);


        if (Input.GetButtonDown(_jumpButton) && _isGrounded)
            _rigidbody.AddForce(new Vector2(0, _jumpForce));

        CorrectFlip();
    }

    private void CorrectFlip()
    {
        if (!_facingRight && _horizontalDirection > 0)
            Flip();
        else if (_facingRight && _horizontalDirection < 0)
            Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
