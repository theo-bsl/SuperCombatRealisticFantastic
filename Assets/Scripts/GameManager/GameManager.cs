using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int nbPlayer = 2;
    public List<GameObject> PlayerList;

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
        if (PlayerList.Count == 1)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GameScene");
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

    public void RemovePlayer(GameObject player)
    { PlayerList.Remove(player); }

    private void GameOver()
    {

    }
}
