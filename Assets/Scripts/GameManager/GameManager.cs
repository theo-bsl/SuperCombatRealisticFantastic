using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsPaused = false;

    public int nbPlayer = 2;
    public List<GameObject> PlayerList;

    public GameObject VictoryMenu;
    public bool isGameOver = false;
    
    private void Awake()
    {
        if(Instance== null)
            Instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        if (CheckActifPlayer() == 1 && !isGameOver)
        {
            GameOver();
        }

        IsPaused = Time.timeScale == 1 ? false : true;
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
        isGameOver = true;
        VictoryMenu.SetActive(true);
    }
}
