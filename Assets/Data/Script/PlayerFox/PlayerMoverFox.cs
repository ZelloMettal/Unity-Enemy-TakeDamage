using UnityEditor.Tilemaps;
using UnityEngine;

//Подвязываем компоненты к скрипту
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMoverFox : MonoBehaviour
{
    //Определение направления движения персонажа
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    //Поля для Инспектора
    [SerializeField] private float _speed;  //Скорость персонажа

    //Свойства скрипта
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _moveVector;
    private string _floatMoveAnimation = "Speed"; //Свойство для работы с анимацией

    void Start()
    {
        //Получаем компоненты
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();        
    }
    
    void Update()
    {
        Move();
    }

    //Метод движения персонажа
    private void Move()
    {
        //Получаем данные нажатия кнопок
        _moveVector.x = Input.GetAxis(Horizontal);
        _moveVector.y = Input.GetAxis(Vertical);

        //Производим смещение персонажа
        _rigidbody.velocity = new Vector2(_moveVector.x * _speed * Time.deltaTime, _moveVector.y * _speed * Time.deltaTime);

        if (_rigidbody.velocity.x != 0 || _rigidbody.velocity.y != 0)
        {
            _animator.SetFloat(_floatMoveAnimation, 1);
            FlipX();
        }
        else
        { 
            _animator.SetFloat(_floatMoveAnimation, 0);        
        }
    }

    private void FlipX()
    {
        if (_moveVector.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if(_moveVector.x < 0)
        { 
            _spriteRenderer.flipX = true;        
        }
        else
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
        }
    }
}
