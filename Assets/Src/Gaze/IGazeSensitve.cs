using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGazeSensitve
{
    public virtual void UpdateGaze(float angleToGaze, float distToPlayer)
    {

    }

    public virtual void OnBlinkStart(float distToPlayer)
    {

    }

    public virtual void OnLongBlinkStart(float distToPlayer)
    {

    }

    public virtual void OnBlinkEnd(float distToPlayer, float time)
    {

    }

    public virtual void OnLongBlinkEnd(float distToPlayer, float time)
    {

    }

}
