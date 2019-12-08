using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TriggerEvent : UnityEvent<Collider2D> {}

public class PhysicsTriggerEvents : MonoBehaviour
{
    public TriggerEvent TriggerEnter;
    public TriggerEvent TriggerExit;

    private void OnTriggerEnter2D(Collider2D other) {
        TriggerEnter.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        TriggerExit.Invoke(other);
    }
}
