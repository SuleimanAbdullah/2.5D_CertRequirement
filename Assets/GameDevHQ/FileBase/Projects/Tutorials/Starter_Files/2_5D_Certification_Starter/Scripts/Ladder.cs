using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player _player;

    private bool _canNotClimbLadder;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LadderGrabChecker")
        {
            if (Input.GetKey(KeyCode.K))
            {
                if (_canNotClimbLadder==false)
                {
                    _player.AllowToClimbLadder();
                    if (_player != null)
                    {

                        _player.ClimbLader();
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.K))
            {
                _player.ExitLader();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.ExitLader();
            _canNotClimbLadder = true;
        }
    }
}
