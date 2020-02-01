using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMvt : MonoBehaviour
{

    [SerializeField] Transform character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character.position.x > this.transform.position.x && this.transform.position.x < 13f)
        {
            this.transform.position =new Vector3(character.position.x,this.transform.position.y, this.transform.position.z);
        }
    }
}
