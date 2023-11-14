using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public List<GameObject> hudList;

    private void Start()
    {
        int nbPlayer = GameManager.Instance.nbPlayer;

        float distance = Screen.width / (nbPlayer + 1);

        for (int i = 0; i < nbPlayer; i++)
        {
            hudList[i].SetActive(true);

            Transform HUDTransform = hudList[i].transform;

            HUDTransform.position = new Vector3(
                HUDTransform.position.x + (i+1) * distance,
                HUDTransform.position.y, 
                HUDTransform.position.z);
        }
    }
}
