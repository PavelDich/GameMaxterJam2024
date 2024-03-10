using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : GetterObject
{
    // Геттер - реагирует на любые RigidBody.

    private void OnTriggerEnter(Collider other)
    {
        ChangeState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeState(false);
    }
}

