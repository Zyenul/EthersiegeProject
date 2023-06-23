using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public int speed;
    void Update()
    {
        this.transform.Rotate(Vector3.right * Time.deltaTime * speed);
    }
}
