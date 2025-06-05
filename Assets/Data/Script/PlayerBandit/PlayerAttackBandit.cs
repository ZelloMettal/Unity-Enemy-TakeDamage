using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]

//Скрипт атаки
public class PlayerAttackBandit : MonoBehaviour
{
    [SerializeField] private PlayerBandit _playerBandit;
    [SerializeField] private PlayerRunBandit _playerRunBandit;

    //Свойства скрипта 
    private const string Attack = nameof(Attack); // Параметр для аниматора    
    private Animator _animator;                 // Компонент
    private CircleCollider2D _circleCollider;   // 
    private const float _radiusCircleCollider = 1.2f;

    private void Awake()
    {
        //Получаем компонент
        _playerRunBandit = GetComponent<PlayerRunBandit>();
        _playerBandit = GetComponent<PlayerBandit>();
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.enabled = false;
    }

    void Update()
    {        
        Hit();
    }

    //Метод удара
    private void Hit()
    {
        //Получаем состояние из родительского класса
        bool isHit = _playerBandit.GetIsAttack();
        float directionPlayer = _playerRunBandit.GetDirection();
        float directionCircleCollider = 0f;

        //Производим удар
        if (isHit)
        {
            if (directionPlayer > 0)            
                directionCircleCollider = transform.position.x - _circleCollider.offset.x;            
            else            
                directionCircleCollider = transform.position.x + _circleCollider.offset.x;

            Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(directionCircleCollider, transform.position.y), _radiusCircleCollider);
            _animator.SetBool(Attack, true);            
            foreach (Collider2D target in hits)
            {
                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {                    
                    enemy.SetTakeDamage();
                }
            }
        }
        else 
        {
            ColliderDisablee();
            _animator.SetBool(Attack, false);
        }     
    }

    public void ColliderEnable()
    {
        _circleCollider.enabled = true;
    }

    public void ColliderDisablee()
    {
        _circleCollider.enabled = false;
    }
}
