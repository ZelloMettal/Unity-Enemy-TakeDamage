using System.Linq;
using UnityEngine;

//Подвязываем компоненты к скрипту
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]

//Скрипт атаки
public class PlayerAttackBandit : MonoBehaviour
{

    [SerializeField] private PlayerBandit _playerBandit;        // Объект главного класса персонажа
    [SerializeField] private PlayerRunBandit _playerRunBandit;  // Объект класса перемещения персонажа

    //Свойства скрипта 
    private const string Attack = nameof(Attack);       // Именование переменных  
    private CircleCollider2D _circleCollider;           // Компонент
    private Animator _animator;                         // 
    private const float _radiusCircleCollider = 1.2f;   // Радиус круга триггера коллайдера

    private void Awake()
    {
        //Получаем компонент
        _playerRunBandit = GetComponent<PlayerRunBandit>();
        _playerBandit = GetComponent<PlayerBandit>();
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.enabled = false;                // Отключаем коллайдер круга пока не будет совершон удар
    }

    void Update()
    {        
        Hit();
    }

    //Метод удара
    private void Hit()
    {        
        bool isHit = _playerBandit.GetIsAttack();                   // Получаем состояние атаки из родительского класса
        float directionPlayer = _playerRunBandit.GetDirection();    // Получаем получаем направление из класса перемещения
        float directionCircleColliderX = 0f;                        // Х координата круга коллайдера атаки

        //Производим удар
        if (isHit)
        {
            //Определяем направления круга коллайдера атаки
            if (directionPlayer > 0)            
                directionCircleColliderX = transform.position.x - _circleCollider.offset.x;            
            else            
                directionCircleColliderX = transform.position.x + _circleCollider.offset.x;

            //Получаем массив коллайдеров с которыми пересёкся коллайдер атаки
            Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(directionCircleColliderX, transform.position.y), _radiusCircleCollider);
            _animator.SetBool(Attack, true);    // Анимация атаки
            
            //Перебираем массив коллайдеров и ищем целевой коллайдер
            foreach (Collider2D target in hits)
            {
                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {                    
                    enemy.SetTakeDamage();  // Вызываем соответствующий метод получения урона у противника
                }
            }
        }
        else 
        {
            ColliderAttackDisable();
            _animator.SetBool(Attack, false);
        }     
    }

    //Метод включения коллайдера атаки
    public void ColliderAttackEnable()
    {
        _circleCollider.enabled = true;
    }
    //Метод отключения коллайдера атаки
    public void ColliderAttackDisable()
    {
        _circleCollider.enabled = false;
    }
}
