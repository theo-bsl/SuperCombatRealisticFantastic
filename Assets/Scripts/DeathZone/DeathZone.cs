using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private List<GameObject> playerList = new List<GameObject>();

    private int deathZoneLimitStart = -20;

    private void Start()
    {
        playerList = GameManager.Instance.PlayerList;
    }

    private void Update()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].activeSelf)
            {
                if (playerList[i].transform.position.y < deathZoneLimitStart)
                {
                    playerList[i].GetComponent<PlayerManagement>().Death();
                    playerList[i].GetComponent<PlayerController>().Death();
                }
            }
        }
    }
}
