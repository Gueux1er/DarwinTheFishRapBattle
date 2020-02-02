using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerManager : MonoBehaviour
{
    public static ChallengerManager Instance;

    public List<ChallengerStat> challengers;
    public ChallengerStat currentChallenger;

    public void Start()
    {
        Instance = this;

        for (int i = 0; i < challengers.Count; ++i)
        {
            challengers[i].gameObject.SetActive(false);
        }

        currentChallenger = challengers[LevelManager.Instance.level];
        currentChallenger.gameObject.SetActive(true);
    }
}
