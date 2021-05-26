using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovment : MonoBehaviour
{
    public bool shouldMove;
    public float moveSpeed;

    void Update()
    {
        if (shouldMove == true)
        {
            transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;                      // Makes the target moving on x axis
        }
    }
}
