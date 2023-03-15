using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;
    [Header("player")]
    [SerializeField] private PlayerController _player;
    [Range(0, 1000)] public float HpMax;
    [Range(0, 40)] public float Speed;
    [Range(0, 50)] public int Damage;
    [Range(0, 100)] public int QualityBullet;


    [Header("enemy")]
    [SerializeField] private BehaviorEnemey _enemy;
    public List<int> ListTotalEnemy;
    [Range(0, 1000)] public float[] HpEnemy;
    [Range(0, 20)] public float[] SpeedEnemy;
    [Range(0, 100)] public float[] DamageEnemy;

    [SerializeField] private DropAndPickup _scripPickUpGun;
    [SerializeField] private SpawnBlullet _spawnBlullet;
    [SerializeField] private EnemyTutorial _tutorial;
    [SerializeField] private EnemyPhase1 _phase1;
    [SerializeField] private Boss _boss;
    [SerializeField] private Transform _posCam;
    public GameObject imgKey, imgbullet;
    public int CountEnemyKill;

    public bool IsEndGame = false;

    public Transform PosCam { get => _posCam; set => _posCam = value; }
    public SpawnBlullet SpawnBlullet { get => _spawnBlullet; set => _spawnBlullet = value; }
    public Boss Boss { get => _boss; set => _boss = value; }
    public EnemyPhase1 Phase1 { get => _phase1; set => _phase1 = value; }

    public EnemyTutorial Tutorial
    {
        get => _tutorial;set => _tutorial = value;
    }
    public BehaviorEnemey Enemy
    {
        get => _enemy; set => _enemy = value;
    }
    public PlayerController Player
    {
        get => _player; set => _player = value;
    }
    public DropAndPickup ScripPickUpGun
    {
        get => _scripPickUpGun; set => _scripPickUpGun = value;
    }
     
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }
    private void Start()
    {
        
    }

    public void SetActiveImgBullet(bool isBullet)
    {
        imgbullet.SetActive(isBullet);
    }

    public void SetActiveImgKey(bool iskey)
    {
        imgKey.SetActive(iskey);
    }


}
