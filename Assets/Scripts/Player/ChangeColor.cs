using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<SkinnedMeshRenderer>().material = gameObject.GetComponentInParent<Initializer>().GetCurrentMaterial;
    }
}
