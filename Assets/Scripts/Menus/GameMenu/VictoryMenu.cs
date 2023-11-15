using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    private List<GameObject> playerList = new List<GameObject>();

    public List<GameObject> playerFaceList;
    public TextMeshProUGUI winnerName;

    private void Awake()
    {
        playerList = GameManager.Instance.PlayerList;
    }

    private void OnEnable()
    {
        SetVictoryImage();
    }

    private void SetVictoryImage()
    {
        int index = GetActifPlayerIndex();

        playerFaceList[index].SetActive(true);
        winnerName.text = playerFaceList[index].name;
    }

    private int GetActifPlayerIndex()
    {
        int index = 0;

        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].activeSelf)
            {
                index = i; 
                break;
            }
        }
        return index;
    }
}
