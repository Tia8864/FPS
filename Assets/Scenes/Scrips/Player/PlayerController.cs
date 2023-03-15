using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController s_instance;
    private float _hp;
    private float _speed;
    private EnemyTutorial _tutorial;
    private EnemyPhase1 _phase1;
    private Boss _boss;
    public float HP { get => _hp; set => _hp = value; }
    public float Speed { get => _speed; set => _speed = value; }
    private float _damageClose, _damageLong;

    [SerializeField] private CharacterController _player;
    public bool IsRun = true;
    private float _vertical;
    private float _horizontal;

    private RaycastHit hit;
    private Ray ray;
    private int _qualityBullet;
    private float _damageBullet;
    public int QualityBullet { get => _qualityBullet; set => _qualityBullet = value; }
    public float DamageBullet { get => _damageBullet; set => _damageBullet = value; }
    [SerializeField] private Transform _orientation;
    private Vector3 _diraction;

    public bool OverDoor1 = false;
    private bool _bite = false;
    public bool _isWin = false;



    private void Start()
    {
        _tutorial = GamaManager.Instance.Tutorial;
        _phase1 = GamaManager.Instance.Phase1;
        _boss = GamaManager.Instance.Boss;
        _hp = GamaManager.Instance.HpMax;
        _speed = GamaManager.Instance.Speed;
        _qualityBullet = GamaManager.Instance.QualityBullet;
        _damageBullet = GamaManager.Instance.Damage;
    }

    private void Update()
    {
        _Movement();
        _CheckPhase();
        _ShowPopup();
    }

    private void LateUpdate()
    {
    }
    private void _ShowPopup()
    {
        if(_hp <= 0)
        {
            GamaManager.Instance.IsEndGame = true;
        }
    }
    private void _CheckPhase()
    {
        if(!_tutorial._isClear && !_phase1.isClear)
        {
            _damageClose = _tutorial.DamageEnemy;

            _damageBullet = 15;
        }
        else if(_tutorial._isClear && !_phase1.isClear)
        {
            _damageLong = _phase1.DamageEnemy;

            _damageBullet = 25;
        }
        else if(_tutorial._isClear && _phase1.isClear)
        {
            _damageClose = _boss.DamageCloseEnemy;
            _damageLong = _boss.DamageLongEnemy;
            _damageBullet = 30;
        }
    }
    private void _Movement()
    {

        if (IsRun)
        {
            _vertical = Input.GetAxisRaw("Vertical");
            _horizontal = Input.GetAxisRaw("Horizontal");
        }
        this.transform.rotation = Quaternion.Lerp(
                                transform.rotation,
                                _orientation.rotation,
                                8f * Time.deltaTime);//_orientation.rotation;
        _diraction = _orientation.forward * _vertical + _orientation.right * _horizontal;

        _player.Move(_diraction.normalized * Time.deltaTime * _speed);
        _player.Move(Physics.gravity);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _player.Move(_diraction.normalized * Time.deltaTime * _speed * 5);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("door1"))
        {
            OverDoor1 = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))//collision.gameObject.CompareTag("bulletBoss"))
        {
            _hp -= _damageClose;
        }
        if (collision.gameObject.CompareTag("bulletBoss"))
        {
            _hp -= _damageLong;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy") && _bite)
        {
            _hp -= _damageClose;
            _bite = false;
            StartCoroutine(_SetBite());
        }
    }

    IEnumerator _SetBite()
    {
        yield return new WaitForSeconds(1);
        _bite = true;
    }


}
