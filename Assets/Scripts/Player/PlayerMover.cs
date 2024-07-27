using UnityEngine;

public class PlayerMover : Mover
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Health _health;

    private float _rayDistance = 0.1f;
    private Rigidbody2D _rigidbody;
    private string _horizontalAxis = "Horizontal";
    private string _jumpButton = "Jump";
    private string _runningTrigger = "IsRunning";
    
    private bool IsGrounded => Physics2D.Raycast(_groundPoint.position, Vector2.down, _rayDistance, _groundLayer);

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalDirection = Input.GetAxisRaw(_horizontalAxis);
        float currentHorizontalSpeed = HorizontalDirection * _speed;

        _rigidbody.velocity = new Vector2(currentHorizontalSpeed, _rigidbody.velocity.y);
        _animator.SetBool(_runningTrigger, currentHorizontalSpeed != 0);

        if (Input.GetButtonDown(_jumpButton) && IsGrounded)
            _rigidbody.AddForce(new Vector2(0, _jumpForce));
    }
}
