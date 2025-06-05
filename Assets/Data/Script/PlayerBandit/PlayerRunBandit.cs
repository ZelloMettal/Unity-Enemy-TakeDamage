using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

//Подвязываем компоненты к скрипту
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]

//Скрипт перемещения 
public class PlayerRunBandit : MonoBehaviour
{
    //Именование переменных
    private const string Horizontal = nameof(Horizontal);
    private const string Speed = nameof(Speed);

    //Свойства скрипта
    private Rigidbody2D _rigidbody;                     //
    private Animator _animator;                         // Компоненты
    private SpriteRenderer _spriteRenderer;             //      
    private CircleCollider2D _circleCollider;           //      
    private float _direction;                           // Направление персонажа
    private float _directionCircleCollider;             // Направление коллайдера сферы
    private const float DistanceCircleCollider = 0.8f;  // Расстояние от персонажа до круга коллайдера удара

    //Свойсво для Инспектора
    [SerializeField] private float _speed;                  // Скорость персонажа
    [SerializeField] private PlayerBandit _playerBandit;    // Объект главного класса персонажа

    private void Awake()
    {
        //Получаем компоненты
        _playerBandit = GetComponent<PlayerBandit>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();     
    }
    void Update()
    {
        Move();
    }

    //Метод перемещения персонажа
    private void Move()
    {
        //Определяем направление движения
        _direction = Input.GetAxis(Horizontal) * _speed * Time.deltaTime;       
        _rigidbody.velocity = new Vector2(_direction, _rigidbody.velocity.y);  

        if (_rigidbody.velocity.x > 0) // Движение в право
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0); // Перемещенеи персонажа
            _animator.SetFloat(Speed, 1); // Анимация движения
            Flip();
        }
        else if (_rigidbody.velocity.x < 0)
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0); // Перемещенеи персонажа
            _animator.SetFloat(Speed, 1); // Анимация движения
            Flip();
        }
        else
        {
            _animator.SetFloat(Speed, 0); // Анимация покоя
        }
    }

    //Метод поворота персонажа в сторону перемещения
    private void Flip()
    {
        // Определяем направление персонажа. Переворачиваем в зависимоости от изменения координат
        if (_direction > 0)
        {
            _spriteRenderer.flipX = true;
            _directionCircleCollider = _rigidbody.velocity.x + DistanceCircleCollider; // Перемещаем круг коллайдера удара согласно направления персонажа
            _circleCollider.offset = new Vector2(_directionCircleCollider, _circleCollider.offset.y);
        }
        else if (_direction < 0)
        {
            _spriteRenderer.flipX = false;
            _directionCircleCollider = _rigidbody.velocity.x - DistanceCircleCollider; // Перемещаем круг коллайдера удара согласно направления персонажа
            _circleCollider.offset = new Vector2(_directionCircleCollider, _circleCollider.offset.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
        }
    }

    //Геттор направления персонажа
    public float GetDirection()
    { 
        return _direction;
    }
}
