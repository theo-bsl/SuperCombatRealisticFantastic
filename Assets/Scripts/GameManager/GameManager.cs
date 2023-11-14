using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int nbPlayer = 2;
    public List<GameObject> PlayerList;

    public GameObject VictoryMenu;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        if (CheckActifPlayer() == 1)
        {
            GameOver();
        }
    }

    private void SpawnPlayer()
    {
        for (int i = 0; i < nbPlayer; i++)
        {
            PlayerList[i].SetActive(true);
            PlayerList[i].transform.position = SpawnPointManager.Instance.GetSpawnPoint();
        }
    }

    private int CheckActifPlayer()
    {
        int nbActifPlayer = nbPlayer;

        for (int i = 0; i < nbPlayer; i++)
        {
            if (!PlayerList[i].activeSelf)
            {
                nbActifPlayer -= 1;
            }
        }

        return nbActifPlayer;
    }
    
    public void RemovePlayer(GameObject player)
    { PlayerList.Remove(player); }

    private void GameOver()
    {
        Time.timeScale = 0;
        VictoryMenu.SetActive(true);
    }
}
