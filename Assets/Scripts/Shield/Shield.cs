using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerManagement _playerManagement;

    public GameObject _shield;
    private Transform _shieldTransform;
    private Material _shieldMaterial;
    private Vector3 _originalShieldScale;
    private Color _originalShieldColor;
    private float _originalShieldColorAlpha;

    public bool _isDefending = false;

    private void Awake()
    {
        _shieldTransform = _shield.GetComponent<Transform>();
        _shieldMaterial = _shield.GetComponent<Renderer>().material;

        _originalShieldScale = _shieldTransform.localScale;
        _originalShieldColor = _shieldMaterial.color;
        _originalShieldColorAlpha = _originalShieldColor.a;
    }

    private void Start()
    {
        _playerManagement = GetComponent<PlayerManagement>();
    }

    private void Update()
    {
        float stamina = _playerManagement.GetStamina();

        _shieldTransform.localScale = _originalShieldScale * stamina / 100f;

        Color newColor = _originalShieldColor + Color.red * (100 - stamina) / 100f;
        newColor.a = _originalShieldColorAlpha;
        _shieldMaterial.color = newColor;
    }

    public void SetDefending(bool defending)
    { 
        _isDefending = defending;
        _shield.SetActive(defending);
    }
}
