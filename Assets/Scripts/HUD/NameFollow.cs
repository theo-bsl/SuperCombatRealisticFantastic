using TMPro;
using UnityEngine;

public class NameFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = Vector3.up * 3;


    private void Update()
    {
        transform.position = offset + player.transform.position;
    }
}
