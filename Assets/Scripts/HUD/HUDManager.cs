using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public List<GameObject> hudList;
    public static HUDManager Instance;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }
    public void PlaceHUD()
    {
        int nbPlayer = GameManager.Instance.nbPlayer;

        float distance = Screen.width / (nbPlayer + 1);

        for (int i = 0; i < nbPlayer; i++)
        {
            hudList[i].SetActive(true);

            Transform HUDTransform = hudList[i].transform;

            HUDTransform.position = new Vector3(
                0 + (i + 1) * distance,
                HUDTransform.position.y,
                HUDTransform.position.z);
        }
    }

    public void GetHUD(GameObject newPlayer)
    {
        for(int i = 0;i < hudList.Count;i++) 
        {
            if (hudList[i].GetComponent<PlayerHUD>().player == null)
            {
                hudList[i].GetComponent<PlayerHUD>().player = newPlayer;
                hudList[i].SetActive(true) ;
                break;
            }
        }
    }
}
