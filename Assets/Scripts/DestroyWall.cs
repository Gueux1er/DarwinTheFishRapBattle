using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public static DestroyWall Instance;

    public List<Rigidbody> wallsRb;

    private void Awake()
    {
        Instance = this;
    }

    public void Destroy()
    {
        for (int i = 0; i < wallsRb.Count; ++i)
        {
            wallsRb[i].isKinematic = false;
            //wallsRb[i].AddForce(Vector3.back * 30 + Vector3.back * 30 + Vector3.back * 30);
        }
    }
}
