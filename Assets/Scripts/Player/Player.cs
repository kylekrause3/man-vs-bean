using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool toggleCursorLock = false;
    internal void AddBuff(Item item)
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            toggleCursorLock = !toggleCursorLock;
            if (toggleCursorLock) {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
