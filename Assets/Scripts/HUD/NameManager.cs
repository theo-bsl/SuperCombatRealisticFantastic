using System.Collections.Generic;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    public List<GameObject> playerName;


    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.nbPlayer; i++)
        {
            playerName[i].SetActive(true);
        }
    }
}
