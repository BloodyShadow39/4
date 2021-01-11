using UnityEngine;

public class ButtonRaycast : MonoBehaviour
{
    private RectTransform _transform;

    private BoxCollider _boxCollider;

    private void Awake() {
        if (!gameObject.TryGetComponent(out _transform))
            Debug.LogError("RectTransform button not found");
        if (!gameObject.TryGetComponent(out _boxCollider))
            Debug.LogError("BoxCollider button not found");

        _boxCollider.size = new Vector3(_transform.rect.width, _transform.rect.height, _boxCollider.size.z);
    }
}
