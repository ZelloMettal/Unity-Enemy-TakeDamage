﻿using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;

//Подвязываем компоненты к скрипту 
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]

//Глобальныый скрипт персонажа
public class PlayerBandit : MonoBehaviour
{  
    //Определения клавиши управления
    private const int MouseLeftClick = 0;       // Левый клик мыши

    //Свойства скрипта
    private Rigidbody2D _rigidbody;             // 
    private Vector2 _moveVector;                // 
    private BoxCollider2D _boxCollider;         // Копмоненты
    private Animator _animator;                 //
    private CircleCollider2D _circleCollider;   //
    private bool _isRuning = false;             //Свойство состояния передвижения
    private bool _isAttack = false;             //Свойство состояния атаки

    void Start()
    { 
        //Получаем компоненты
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    
    void Update()
    {
        Run();      
        Hit();       
    }

    //Метод проверки переещения персонажа
    private void Run()
    {       
        if (_rigidbody.velocity.x != 0)        
            _isRuning = true;        
        else
            _isRuning = false;        
    }

    //Метод проверки удара персонажа
    private void Hit()
    {        
        if (Input.GetMouseButton(MouseLeftClick))        
            _isAttack = true;        
        else         
            _isAttack = false;        
    }    

    //Геттор состояния бега
    public bool GetIsRuning()
    {
        return _isRuning;
    }

    //Геттор состояния атаки
    public bool GetIsAttack()
    { 
        return _isAttack;
    }    
}
