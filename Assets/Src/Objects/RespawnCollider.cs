using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnCollider : MonoBehaviour
{
    public UnityEvent<GameObject> onObjectEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7) // is interactable 
        {
            onObjectEnter.Invoke(other.gameObject);
        }
    }

}
