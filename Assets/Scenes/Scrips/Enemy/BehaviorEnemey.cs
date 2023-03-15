using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorEnemey : MonoBehaviour
{
    private PlayerController _Player;
    [SerializeField] private Rigidbody _rb;
    public bool IsAlive;
    private bool isGround = false;
    private float _damageByPlayer;

    public float Hp { get; set; }
    public float Speed { get; set; }
    protected void Start()
    {
        _Player = GamaManager.Instance.Player;
        _damageByPlayer = _Player.DamageBullet;
    }
    private void Awake()
    {
        IsAlive = true;
    }
    protected void FixedUpdate()
    {
        _ForwardPlayer();
    }
    protected void _ForwardPlayer()
    {
        _rb.velocity = Physics.gravity;
        transform.LookAt(_Player.transform.position);
        if(isGround)
        _rb.velocity += transform.forward.normalized * Speed;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet")){
            Hp -= _damageByPlayer;
            if (Hp <= 0)
            {
                //destroy and animation enemydaeth
                IsAlive = false;
                GamaManager.Instance.CountEnemyKill++;
                _destroySelf();
            }
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            isGround = true;
        }
    }
    private void _destroySelf()
    {
        Destroy(this.gameObject);
    }
}
