using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{

    public float time;

    void Start()
    {
        Destroy(this.gameObject, time);
    }

}
