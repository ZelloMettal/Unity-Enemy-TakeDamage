using UnityEngine;

//Подвязываем компоненты к скрипту
[RequireComponent(typeof(Animator))]

//скрипт противника
public class Enemy : MonoBehaviour
{
    //Свойства скрипта
    private const string Damage = nameof(Damage);   //Именование переменных
    private Animator _animator;                     //Компонент аниматора
    private bool _isTakeDamage = false;             //Состояние получение урона

    private void Awake()
    {
        _animator = GetComponent<Animator>();       //Получаем компонент аниматора
    }

    private void Update()
    {
        TakeDamage();
    }

    //Метод обработки получения урона
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

    //Сеттер состояния получения урона
    public void SetTakeDamage()
    {
        _isTakeDamage = true;
    }
}
