using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject combatUI;
    [SerializeField] CombatScript cS;
    ChallengerStat challengerInfos;
    bool inFight = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inFight == false)
            Move();
        else
        {
            cS.slider.fillAmount -= challengerInfos.powerFlow*0.001f;
        }


    }

    void Move()
    {
        this.transform.localPosition += Vector3.right*moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Challenger")
        {
            print("Fight");
            inFight = true;
            challengerInfos = collision.transform.GetComponent<ChallengerStat>();
            CombatSetup();
        }
    }

    void CombatSetup()
    {
        combatUI.SetActive(true);
    }
}
