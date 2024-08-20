using System.Collections.Generic;
using UnityEngine;

public class SkinPlacement : MonoBehaviour
{
    private const string RENDER_LAYER = "SkinRender";

    [SerializeField] private Rotator _rotator;

    private GameObject _currentModel;
    private List<GameObject> _currentModels = new List<GameObject>();

    public void InstantiateModel(GameObject model, bool isMove)
    {
        Clear();

        _rotator.Reset();
        _rotator.enabled = isMove;

        if (model != null)
        {
            _currentModel = Instantiate(model, transform);
            Transform[] children = model.GetComponentsInChildren<Transform>();

            foreach (var child in children)
                child.gameObject.layer = LayerMask.NameToLayer(RENDER_LAYER);
        }
    }

    public void InstantiateModel(GameObject model)
    {
        if (model != null)
        {
            _rotator.Reset();

            var result = Instantiate(model, transform);
            _currentModels.Add(result);

            Transform[] children = model.GetComponentsInChildren<Transform>();

            foreach (var child in children)
                child.gameObject.layer = LayerMask.NameToLayer(RENDER_LAYER);
        }
    }

    public void Clear()
    {
        if (_currentModel != null)
            Destroy(_currentModel);

        if (_currentModels.Count > 0)
        {
            foreach (var currentModel in _currentModels)
                Destroy(currentModel);
            _currentModels.Clear();
        }

        _rotator.enabled = true;
    }
}