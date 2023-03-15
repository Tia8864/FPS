using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBullet4Boss : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [Range(0, 50)]
    [SerializeField] private float _speed;


    private Vector3 _dir;
    private PlayerController _player;
    private Boss _boss;

    private void Start()
    {
        _player = GamaManager.Instance.Player;
        _boss = GamaManager.Instance.Boss;
        Invoke("selfDestroy", 10f);
    }

    private void Update()
    {
        _dir = _boss._orientation.normalized;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _dir.normalized * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") ||
            collision.gameObject.CompareTag("ground") ||
            collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
            Destroy(this.gameObject);
    }

    private void selfDestroy()
    {
        Destroy(this.gameObject);
    }
}
