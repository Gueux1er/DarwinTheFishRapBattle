using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public static MovementCharacter Instance;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject combatUI;
    [SerializeField] CombatScript cS;
    ChallengerStat challengerInfos;
    [HideInInspector] public bool inFight = false;
    [HideInInspector] public bool prepareFight = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFight == false && !prepareFight)
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
            prepareFight = true;

            print("Fight");

            challengerInfos = collision.transform.GetComponent<ChallengerStat>();
            CombatSetup();
        }
    }

    public void CombatSetup()
    {
        combatUI.SetActive(true);
        cS.StartFight(challengerInfos.name, challengerInfos.flavorText, challengerInfos.spriteToDisplay);
    }
}
