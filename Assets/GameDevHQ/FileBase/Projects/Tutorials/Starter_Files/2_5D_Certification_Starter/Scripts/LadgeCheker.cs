using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadgeCheker : MonoBehaviour
{
    private Player _player;
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
                _player.ActivateLedgeGrab();
            }
           
        }
    }
}
