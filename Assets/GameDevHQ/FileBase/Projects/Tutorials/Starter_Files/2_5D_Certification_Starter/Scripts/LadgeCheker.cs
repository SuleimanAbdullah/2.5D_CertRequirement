using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadgeCheker : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private GameObject _handPos;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponentInChildren<Player>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LedgeGrabChecker")
        {
            if (_player != null)
            {
                _player.ActivateLedgeGrab(_handPos);
            }
           
        }
    }
}
