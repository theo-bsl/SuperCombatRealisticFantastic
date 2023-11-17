using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    public List<GameObject> playerName;
    public static NameManager Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.nbPlayer; i++)
        {
            playerName[i].SetActive(true);
        }
    }

    public void GetName(GameObject newPlayer)
    {
        for (int i = 0; i < playerName.Count; i++)
        {
            if (playerName[i].GetComponent<NameFollow>().player == null)
            {
                playerName[i].GetComponent<NameFollow>().player = newPlayer;
                playerName[i].SetActive(true);
                break;
            }
        }
    }
}
