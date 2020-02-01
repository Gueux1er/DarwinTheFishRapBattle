using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerManager : MonoBehaviour
{
    public List<ChallengerStat> challengers;

    public void Start()
    {
        for (int i = 0; i < challengers.Count; ++i)
        {
            challengers[i].gameObject.SetActive(false);
        }

        challengers[LevelManager.Instance.level].gameObject.SetActive(true);
    }
}
