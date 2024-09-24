using System.Collections;
using UnityEngine;

public class DisappearingBox : InteractableEnter
{
    [SerializeField] private float _transparencyDelay = 1f;
    [SerializeField] private float _transparencyDuration = 1f;
    [SerializeField] private float _deactivateDelay = 1f;
    [SerializeField] private float _reactivateDelay = 3f;
    [SerializeField] private float _visibilityDuration = 1f;
    [SerializeField] private Renderer _objectRenderer;
    [SerializeField] private MeshCollider _meshCollider;

    private Color _originalColor;
    private Material _material;

    private void Start()
    {
        _material = _objectRenderer.material;
        _originalColor = _material.color;
        SetMaterialTransparent();
    }

    public override void InteractEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(_transparencyDelay);

        float elapsed = 0f;
        while (elapsed < _transparencyDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / _transparencyDuration);
            SetTransparency(alpha);
            yield return null;
        }

        SetTransparency(0f);

        yield return new WaitForSeconds(_deactivateDelay);

        _objectRenderer.enabled = false;
        _meshCollider.enabled = false;

        yield return new WaitForSeconds(_reactivateDelay);

        StartCoroutine(Reactivate());
    }

    private IEnumerator Reactivate()
    {
        _meshCollider.enabled = true;
        _objectRenderer.enabled = true;

        float elapsed = 0f;
        while (elapsed < _visibilityDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / _visibilityDuration);
            SetTransparency(alpha);
            yield return null;
        }

        SetTransparency(1f);
    }

    private void SetTransparency(float alpha)
    {
        if (_material != null)
        {
            Color newColor = _originalColor;
            newColor.a = alpha;
            _material.color = newColor;
        }
    }

    private void SetMaterialTransparent()
    {
        if (_material != null)
        {
            _material.SetFloat("_Mode", 2);
            _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _material.SetInt("_ZWrite", 0);
            _material.DisableKeyword("_ALPHATEST_ON");
            _material.EnableKeyword("_ALPHABLEND_ON");
            _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            _material.renderQueue = 3000;
        }
    }
}
