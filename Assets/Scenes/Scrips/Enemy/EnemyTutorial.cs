using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private BehaviorEnemey Enemy;
    private int _numberSpawn;
    [SerializeField] private Transform _posSpawnBL; //Back and left
    [SerializeField] private Transform _posSpawnUR; //up and right
    private PlayerController _player;
    private DropAndPickup _scripPickUpGun;
    private BehaviorEnemey _enemy;
    private int _countEnemyKill=0, _currKill=0;
    private bool _isSpawn = false;
    public bool _isClear = false;

    private float _hp;
    private float _speed;
    private float _dame;
    public float DamageEnemy { get => _dame; set => _dame = value; }

    private void Start()
    {
        //_enemy = GamaManager.Instance.Enemy;
        _scripPickUpGun = GamaManager.Instance.ScripPickUpGun;
        _player = GamaManager.Instance.Player;
        _numberSpawn = GamaManager.Instance.ListTotalEnemy[0];
        _hp = GamaManager.Instance.HpEnemy[0];
        _speed = GamaManager.Instance.SpeedEnemy[0];
        _dame = GamaManager.Instance.DamageEnemy[0];
    }

    private void Update()
    {
        if (_numberSpawn > 0 && _isSpawn)
        {
            _isSpawn = false;
            _player.IsRun = false;
            StartCoroutine(_Spawn(_posSpawnBL, _posSpawnUR));
        }

        if(_numberSpawn <=0 && _countEnemyKill == GamaManager.Instance.ListTotalEnemy[0])
        {
            _player.IsRun = true;
            _isClear = true;
        }
        
    }

    private void FixedUpdate()
    {
        _countEnemyKill = GamaManager.Instance.CountEnemyKill;
        if (_countEnemyKill > _currKill)
        {
            _isSpawn = true;
            _currKill = _countEnemyKill;
        }
    }

    public IEnumerator _Spawn(Transform posBl, Transform posUR)
    {
        yield return new WaitForSeconds(1);
        _numberSpawn--;
        Vector3 pos;
        float minX, minZ;
        float ranDomx, ranDomz;

        minX = Mathf.Min(posUR.position.x, posBl.position.x);
        minZ = Mathf.Min(posUR.position.z, posBl.position.z);

        ranDomx = Random.Range(minX, minX + Mathf.Abs(posUR.position.x - posBl.position.x));
        ranDomz = Random.Range(minZ, minZ + Mathf.Abs(posUR.position.z - posBl.position.z));

        pos = new Vector3(ranDomx, 40f, ranDomz);
        _enemy = Instantiate(Enemy, pos, Quaternion.identity);
        _enemy.Hp = _hp;
        _enemy.Speed = _speed;
    }

    public void SetIsSpawn(bool on)
    {
        _isSpawn = on;
    }
}
