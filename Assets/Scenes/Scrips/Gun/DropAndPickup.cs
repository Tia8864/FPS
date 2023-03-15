using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAndPickup : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    private SpawnBlullet scipsGun;
    [SerializeField] private PlayerController _player;
    [Range(1, 10)]
    [SerializeField] private float rangPickup = 6f;

    [SerializeField] private Transform _Hand;
    [SerializeField] private Transform _fpsCam;


    private EnemyTutorial _tutorial;
    public bool isEpuip = false;
    private void Start()
    {
        _player = GamaManager.Instance.Player;
        _tutorial = GamaManager.Instance.Tutorial;
        scipsGun = Gun.GetComponent<SpawnBlullet>();
        scipsGun.enabled = false; 
    }

    private void Update()
    {
        Vector3 distance = _player.transform.position - Gun.transform.position;
        if(distance.magnitude <= rangPickup && Input.GetKeyDown(KeyCode.E))
        {
            _PickUpGun();
            isEpuip = true;
            _tutorial.SetIsSpawn(true);
        }
    }

    private void _PickUpGun()
    {
        Gun.transform.SetParent(_Hand);
        Gun.transform.localPosition = Vector3.zero;
        Gun.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Gun.transform.localScale = Vector3.one * 0.5f;

        scipsGun.enabled = true;
    }

}
