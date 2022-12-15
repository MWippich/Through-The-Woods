using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour, IGazeSensitve
{

    private List<GameObject> outside = new List<GameObject>();

    [SerializeField] private Transform respawnPos;

    private void Start()
    {
        foreach(RespawnCollider child in GetComponentsInChildren<RespawnCollider>())
        {
            child.onObjectEnter.AddListener(AddObj);
        }
    }

    private void AddObj(GameObject obj)
    {
        if (outside.Contains(obj))
        {
            return;
        }
        outside.Add(obj);
    }

    public void OnBlinkStart(float distToPlayer)
    {

        if (outside.Count > 0)
        {
            GameObject obj = outside[outside.Count - 1];
            outside.RemoveAt(outside.Count - 1);
            obj.transform.parent.position = respawnPos.position;
            if(obj.GetComponent<Rigidbody>() != null)
            {
                obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
