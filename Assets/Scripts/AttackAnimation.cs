using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ShowAnimation()
    {
        _animator.SetTrigger("Attacked");
    }
}
