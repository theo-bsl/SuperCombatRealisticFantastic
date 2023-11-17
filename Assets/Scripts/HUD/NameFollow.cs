using TMPro;
using UnityEngine;

public class NameFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = Vector3.up * 3;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = player.name;
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(offset + player.transform.position);
    }
}
