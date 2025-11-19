using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PaddleInput : MonoBehaviour
{
    public UnityEvent<float> OnTargetUpdated = new();

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            OnTargetUpdated.Invoke(mouseWorldPos.x);
        }
    }
}