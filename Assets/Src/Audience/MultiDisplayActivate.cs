using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplayActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE && !UNITY_EDITOR
        for(int i=1; i <= Display.displays.Length; i++)
        {
            Display.displays[i].Activate(1920, 1080, default);
        }

#endif
        //Application.targetFrameRate = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
