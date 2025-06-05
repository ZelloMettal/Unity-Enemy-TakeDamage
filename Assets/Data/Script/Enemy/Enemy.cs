using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    private const string Damage = nameof(Damage);
    private Animator _animator;
    private bool _isTakeDamage = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        if (_isTakeDamage)
        {
            _animator.SetBool(Damage, true);
            _isTakeDamage = false;
        }
        else
        { 
            _animator.SetBool(Damage, false);        
        }
    }

    public void SetTakeDamage()
    {
        _isTakeDamage = true;
    }
}
