using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementCharacter : MonoBehaviour
{
    public static MovementCharacter Instance;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject combatUI;
    [SerializeField] CombatScript cS;
    ChallengerStat challengerInfos;
    [HideInInspector] public bool inFight = false;
    [HideInInspector] public bool prepareFight = false;
    [HideInInspector] public bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFight == false && !prepareFight)
        {
            if (canMove == true)
                Move();
        }
        else
        {
            cS.slider.fillAmount -= challengerInfos.powerFlow * 0.001f;
            cS.handleSlider.value -= challengerInfos.powerFlow * 0.001f;
        }
        if (this.transform.position.x > 30f)
        {
            canMove = false;
            this.transform.position = new Vector3(-25, this.transform.position.y, this.transform.position.z);
            LevelManager.Instance.level += 1;
            LevelManager.Instance.EndFade();
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
            CombatSetup(true);
        }
    }

    public void CombatSetup(bool changePosition)
    {
        combatUI.SetActive(true);
        cS.StartFight(challengerInfos.name, challengerInfos.flavorText, challengerInfos.spriteToDisplay,changePosition);
    }
}
