using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase3 : MonoBehaviour
{

    [SerializeField] private GameObject _boss;
    private EnemyPhase1 phase1;
    // Start is called before the first frame update
    void Start()
    {
        phase1 = GamaManager.Instance.Phase1;

        _boss.SetActive(false);
    }

    private void Update()
    {
        if (phase1.isClear && phase1.IsKey)
        {
            if (_boss == null) return;
            _boss.SetActive(true);
        }
    }
}
