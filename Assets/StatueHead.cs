using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHead : MonoBehaviour, IGazeSensitve
{
    [SerializeField] private Transform player;
    private float maxAngle = 40f;

    public void OnBlinkStart(float dist)
    {
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPos);

        ClampRotation();
    }

    private void ClampRotation()
    {
        float y = transform.eulerAngles.y;

        if (y <= maxAngle || y >= 360 - maxAngle)
            return;

        if (y >= 180)
            y = 360 - maxAngle;
        else if (y < 180)
            y = maxAngle;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }
}
