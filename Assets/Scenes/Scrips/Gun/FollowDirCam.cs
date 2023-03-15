using UnityEngine;

public class FollowDirCam : MonoBehaviour
{
    [SerializeField] private Transform _camPos;


    void Update()
    {
        this.transform.rotation = _camPos.rotation;
    }
}
