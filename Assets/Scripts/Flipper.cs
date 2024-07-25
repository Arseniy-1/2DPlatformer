using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private bool _facingRight = true;

    private void FixedUpdate()
    {
        CorrectFlip();
    }

    private void CorrectFlip()
    {
        if (!_facingRight && _mover.HorizontalDirection > 0)
            Flip();
        else if (_facingRight && _mover.HorizontalDirection < 0)
            Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
