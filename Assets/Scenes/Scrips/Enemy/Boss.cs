using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]private Rigidbody _rb;
    private PlayerController _player;
    private float _hp;
    private float _speed;
    private float _damageClose; 
    public float DamageCloseEnemy { get => _damageClose; set => _damageClose = value; }
    private float _damageLong;
    public float DamageLongEnemy { get => _damageLong; set => _damageLong = value; }

    [Header("khoang cach tan con tam gan")]
    [Range(0, 10)][SerializeField] private float fixCloseDistance;
    [Range(10, 30)] [SerializeField] private float _closeDistance;
    [Header("khoang cach tan con tam xa")]
    [Range(10, 100)] [SerializeField] private float _longDistance;

    [SerializeField] private Transform _spawnBulletLeft;
    [SerializeField] private Transform _spawnBulletRight;
    [SerializeField] private GameObject prefebBullet;
    [SerializeField] private Animator _animator;
    private Transform _posCam;
    private bool shooting = true;
    private float _currentDistance;
    /*private Transform target;
    private bool isMove = false;*/
    public Vector3 _orientation;

    private bool isMove = true;
    private void Start()
    {
        _posCam = GamaManager.Instance.PosCam;
        _player = GamaManager.Instance.Player;
        //target = _player.transform;
        _hp = GamaManager.Instance.HpEnemy[2];
        _speed = GamaManager.Instance.SpeedEnemy[2];
        _damageLong = GamaManager.Instance.DamageEnemy[2];
        _damageClose = GamaManager.Instance.DamageEnemy[3];

    }
    private void Update()
    {
        _orientation = (_posCam.transform.position - this.transform.position);
        _currentDistance = _orientation.magnitude;
        _rb.velocity = Physics.gravity;
    }

    private void FixedUpdate()
    {
        transform.parent.LookAt(_player.transform.position);
        attackCloseDistance();
        attackLongDistance();
        goToPlayer();

    }

    //tang cong tam gan
    private void attackCloseDistance()
    {
        if (_currentDistance <= fixCloseDistance)
        {
            _animator.SetBool("attackClose", true);
        }
        else
        {
            _animator.SetBool("attackClose", false);
        }
    }

    private void attackLongDistance()
    {
        if(shooting && _currentDistance >= _closeDistance && _currentDistance <= _longDistance)
        {
            Debug.Log("dang dung trong tam ban xa"); 
            Instantiate(prefebBullet, _spawnBulletLeft.position, Quaternion.identity);
            Instantiate(prefebBullet, _spawnBulletRight.position, Quaternion.identity);
            shooting = false;
            StartCoroutine(fireBullet());

            Invoke("Move", 3);
        }
    }

    private void goToPlayer()
    {
        if((_currentDistance > fixCloseDistance && _currentDistance <= _closeDistance) || (_currentDistance >= _longDistance))
        {
            Invoke("Move", 3);
        }
    }

    public void Move()
    {
        _rb.velocity += _orientation.normalized * _speed;
    }

    public IEnumerator fireBullet()
    {   yield return new WaitForSeconds(1);
        shooting = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            _hp -= _player.DamageBullet;
            if(_hp <= 0)
            {
                _player._isWin = true;
                GamaManager.Instance.IsEndGame = true;
                Destroy(this.gameObject);
            }
        }
    }
}
