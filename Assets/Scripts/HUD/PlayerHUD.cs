using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public GameObject player;
    private PlayerManagement playerManagement;

    public TextMeshProUGUI playerName;
    public List<GameObject> playerNbLife;
    public TextMeshProUGUI playerHP;

    private void Awake()
    {
        playerManagement = player.GetComponent<PlayerManagement>();
    }

    private void Start()
    {
        playerName.text = player.name;
    }

    private void Update()
    {
        playerHP.text = (playerManagement.GetLife() / playerManagement.GetMaxLife() * 100).ToString() + "%";

        if (playerManagement.NbLife < playerManagement.GetMaxNbLife())
        {
            playerNbLife[playerManagement.NbLife].SetActive(false);
        }
    }
}
