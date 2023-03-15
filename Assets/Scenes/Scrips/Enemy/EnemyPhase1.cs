using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase1 : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private BehaviorEnemey Enemy;
    [SerializeField] private List<AraeSpawn> _listSpawn;
    [SerializeField] private GameObject _PosSpawn;
    private int _numberSpawn;
    private PlayerController _player;
    private BehaviorEnemey _enemy;
    private EnemyTutorial _tutorial;
    public bool isClear;

    private int _countEnemyKill;
    private float _hp;
    private float _speed;
    private float _dame;
    public float DamageEnemy { get => _dame; set => _dame = value; }

    [Header("Key")] 
    public bool IsKey = false;
    private float _distanceKey;
    [SerializeField] private GameObject _keyPrefeb;
    [Header("Door")]
    [SerializeField] private Animator _door;
    [SerializeField] private GameObject Door;
    private float _distanceDoor;


    private void Start()
    {
        //_enemy = GamaManager.Instance.Enemy;
        _tutorial = GamaManager.Instance.Tutorial;
        _player = GamaManager.Instance.Player;
        isClear = false;
        _numberSpawn = GamaManager.Instance.ListTotalEnemy[1];
        //_keyPrefeb = Instantiate(_key, _posKey.position, Quaternion.identity);
        _keyPrefeb.SetActive(false);

        _ProcessSpawn();
        _PosSpawn.SetActive(false);


        _hp = GamaManager.Instance.HpEnemy[1];
        _speed = GamaManager.Instance.SpeedEnemy[1];
        _dame = GamaManager.Instance.DamageEnemy[1];
    }


    private void Update()
    {
        if(_tutorial._isClear && _player.OverDoor1)
        {
            _PosSpawn.SetActive(true) ;
        }

        _countEnemyKill = GamaManager.Instance.CountEnemyKill;
        if (_countEnemyKill >= GamaManager.Instance.ListTotalEnemy[0] +
                              GamaManager.Instance.ListTotalEnemy[1] * _listSpawn.Count && !IsKey)
        {
            _keyPrefeb.SetActive(true);
            isClear = true;
        }

        _distanceKey = (_player.transform.position - _keyPrefeb.transform.position).magnitude;
        if (_distanceKey <= 15 && Input.GetKeyDown(KeyCode.E))
        {
            _keyPrefeb.SetActive(false);
            IsKey = true;
        }

        _distanceDoor = (_player.transform.position - Door.transform.position).magnitude;
        if (IsKey && _distanceDoor <= 10 && Input.GetKeyDown(KeyCode.E))
        {
            _door.SetBool("openDoor", true);
            IsKey = false;
        }
        else
        {
            _door.SetBool("openDoor", false);
        }
    }

    public void _ProcessSpawn()
    {
        for (int i = 0; i < _listSpawn.Count; i++)
        {
            StartCoroutine(_Spawn(
               _listSpawn[i]._posSpawnBL,
               _listSpawn[i]._posSpawnUR));
        }
    }

    public IEnumerator _Spawn(Transform posBl, Transform posUR)
    {
        Vector3 pos;
        float minX, minZ;
        float ranDomx, ranDomz;

        for (int i = 0; i < _numberSpawn ; i++)
        {
            minX = Mathf.Min(posUR.position.x, posBl.position.x);
            minZ = Mathf.Min(posUR.position.z, posBl.position.z);

            ranDomx = Random.Range(minX, minX + Mathf.Abs(posUR.position.x - posBl.position.x));
            ranDomz = Random.Range(minZ, minZ + Mathf.Abs(posUR.position.z - posBl.position.z));

            pos = new Vector3(ranDomx, 0f, ranDomz);
            _enemy = Instantiate(Enemy, pos, Quaternion.identity, _PosSpawn.transform);
            _enemy.Hp = _hp;
            _enemy.Speed = _speed;
            yield return new WaitForSeconds(1);
        }
    }


    [System.Serializable]
    class AraeSpawn
    {
        public Transform _posSpawnBL;
        public Transform _posSpawnUR;
    }
}
