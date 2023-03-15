using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlullet : MonoBehaviour
{
    [Header("Setup spawn")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform _posSpawn;
    [SerializeField] private float _coolDown;
    private PlayerController _player;
    private int _qualityBullet;
    public int QualityBullet { get => _qualityBullet; set => _qualityBullet = value; }
    public bool _fire = true;
    private RaycastHit hit;
    private Ray ray;
    public Vector3 dir;
    private void Start()
    {
        _player = GamaManager.Instance.Player;
        _qualityBullet = _player.QualityBullet;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_Reload() && _fire)
        {
            StartCoroutine(_SpawnBullet());
        }
        ray = new Ray(_posSpawn.position, _posSpawn.forward);


        if(Input.GetKeyDown(KeyCode.R))
        {
            //animator nap dan
            StartCoroutine(_Fire());
        }
    }

    IEnumerator _Fire()
    {
        _fire = false;
        yield return new WaitForSeconds(_coolDown);
        _qualityBullet = _player.QualityBullet;
        _fire = true;
    }

    IEnumerator _SpawnBullet()
    {
        if (Physics.Raycast(ray, out hit))
        {
        }
        dir = (hit.point - _posSpawn.position).normalized;

        Instantiate(Bullet, _posSpawn.position, Quaternion.identity);
        _qualityBullet--;
        yield return new WaitForSeconds(1f);
    }

    public bool _Reload()
    {
        if (_qualityBullet > 0)
        {
            return false;
        }
        else
        {
            _fire = false;
            return true;
        }
    }
}
