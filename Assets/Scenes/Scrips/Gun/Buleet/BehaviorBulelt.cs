using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBulelt : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [Range(1, 100)]
    [SerializeField] private float _speed;
    private Vector3 _dir;
    private void Start()
    {
        Invoke("selfDestroy", 10f);
        _dir = GamaManager.Instance.SpawnBlullet.dir;
        
    }
    private void FixedUpdate()
    {
        _rb.velocity = _dir * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall") || 
            collision.gameObject.CompareTag("ground")||
            collision.gameObject.CompareTag("enemy") )
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
