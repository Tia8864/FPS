using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    private 
    // Start is called before the first frame update
    void Start()
    {
        Wall.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Wall.SetActive(false);
        }
    }

}
