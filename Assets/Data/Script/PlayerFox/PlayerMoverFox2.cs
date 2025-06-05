using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerMoverFox2 : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Speed = nameof(Speed);
    private const string Jump = nameof(Jump);
    private const string SpeedUpDown = nameof(SpeedUpDown);

    [SerializeField] private float _speed; // �������� ���������
    [SerializeField] private float _jumpForse; // ���� ������
    [SerializeField] private KeyCode _jumpKeyCode; // ����� ������� ������

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private float _direction;
    private bool _isGround = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
       _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jumping();
    }

    //����� ��������
    private void Move()
    {
        _direction = Input.GetAxis(Horizontal) * _speed * Time.deltaTime;
        _rigidbody.velocity = new Vector2(_direction, _rigidbody.velocity.y);       

        if (_rigidbody.velocity.x > 0) // �������� � �����
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0); // ����������� ���������
            _animator.SetFloat(Speed, 1); // �������� ��������
            Flip();
        }        
        else if (_rigidbody.velocity.x < 0)
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0); // ����������� ���������
            _animator.SetFloat(Speed, 1); // �������� ��������
            Flip();
        }
        else
        {
            _animator.SetFloat(Speed, 0); // �������� �����
        }
    }

    //����� ������
    private void Jumping()
    {
        if (Input.GetKeyDown(_jumpKeyCode) && _isGround) // ������������ ������
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForse); // ������ ���������
            _animator.SetBool(Jump, true);
            _animator.SetFloat(SpeedUpDown, _rigidbody.velocity.y);
            Flip();
            _isGround = false;
        }
        else if (_rigidbody.velocity.y < 0) // ������������ ������� ���������
        {
            _animator.SetFloat(SpeedUpDown, _rigidbody.velocity.y);
        }         
    }

    //����� �������� ���������
    private void Flip()
    {
        if (_direction > 0)
        {
            _renderer.flipX = false;
        }
        else if (_direction < 0)
        {
            _renderer.flipX = true;
        }
        else 
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
        }
    }

    public void HitPublic()
    {
        Debug.Log("����");
    }

    private void HitPrivate()
    {
        // ���������� ���������� ����� ������� �������� ����� ��� �������
        // ���� ��������� ����� �� �������
        // ����� ����� ������ ��������� ������� ����

        //Collider2D[] hits = Physics2D.OverlapCapsuleAll(/*������ ��������������� �����*/, /*������ �����*/);

        //foreach (Collider2D target in hits) 
        //{
        //    if (target.TryGetComponent<Enemy>(out Enemy enemy))
        //    {
        //        enemy.TakeDamage();
        //    }
        //}
    }

    //����� ������� ����������� ���� ��� ��� � ������������� � ����� �����������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGround = true;
        _animator.SetBool(Jump, false);
        _animator.SetFloat(Speed, 1);
        _animator.SetFloat(SpeedUpDown, 0); // �������� ��������
        //Debug.Log("�����"); // ����� ��� �������
    }

    //����� ������� ������� �������� ��������� ���� �������� ����� � ���� �������� ����������  
    private void OnCollisionStay2D(Collision2D collision) {}

    //����� ������� ������� ����������� ���� ��� ����� �������� ������� ���� ����������
    private void OnCollisionExit2D(Collision2D collision) {}
}
