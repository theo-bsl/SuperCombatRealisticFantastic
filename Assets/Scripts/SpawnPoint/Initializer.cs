using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public List<Material> materials = new List<Material>();
    private Material _currentMaterial;
    public bool _isPlayer2Keyboard = false;
    // Start is called before the first frame update
    void Awake()
    {
        if(!_isPlayer2Keyboard)
            Initialized();
    }

    public void Initialized()
    {
        GameManager.Instance.AddPlayer(gameObject);
        gameObject.name = "Player " + GameManager.Instance.nbPlayer;
        _currentMaterial = materials[GameManager.Instance.nbPlayer - 1];
        HUDManager.Instance.GetHUD(gameObject);
        HUDManager.Instance.PlaceHUD();
        NameManager.Instance.GetName(gameObject);
        gameObject.GetComponent<Outline>().OutlineColor = _currentMaterial.color;
        transform.position = SpawnPointManager.Instance.GetSpawnPoint();
    }
    public Material GetCurrentMaterial { get => _currentMaterial; }
}
