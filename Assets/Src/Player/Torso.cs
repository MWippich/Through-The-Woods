using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torso : MonoBehaviour
{
    [SerializeField] Transform Head;
    [SerializeField] Transform HandL;
    [SerializeField] Transform HandR;

    void Update()
    {
        Vector3 pos = Vector3.zero;

        pos.x = Head.position.x;
        pos.y = (HandL.position.y + HandR.position.y) / 2;

        float maxY = Head.position.y - 0.5f; //TODO: Make automatic and not hard coded
        if (pos.y > maxY)
            pos.y = maxY;

        pos.z = Head.position.z;

        transform.position = pos;
    }
}
