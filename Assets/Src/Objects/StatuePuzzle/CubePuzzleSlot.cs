using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubePuzzleSlot : MonoBehaviour
{

    public bool cubePlaced = false;
    public UnityEvent<bool> onCubePlaced;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tag && !cubePlaced)
        {
            CubePlaced(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tag && cubePlaced)
        {
            CubePlaced(false);
        }
    }

    public void CubePlaced(bool val)
    {
        cubePlaced = val;
        onCubePlaced.Invoke(val);
    }

}
