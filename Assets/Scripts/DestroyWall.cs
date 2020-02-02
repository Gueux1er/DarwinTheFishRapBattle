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
            wallsRb[i].AddForce(Vector3.back * 50 + Vector3.right * Random.Range(-20f, 20f) + Vector3.up * Random.Range(10f, 20f));
        }
    }
}
