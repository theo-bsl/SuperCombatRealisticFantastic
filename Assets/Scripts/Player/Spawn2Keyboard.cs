using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn2Keyboard : MonoBehaviour
{
    private bool _IsInitialized = false;

    public void OnInit()
    {
        if(!_IsInitialized) 
        {
            _IsInitialized = true;
            gameObject.GetComponent<PlayerController>().enabled = true;
            gameObject.GetComponent<Initializer>().Initialized(); 
        }
    }
}
