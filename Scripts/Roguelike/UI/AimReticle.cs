using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimReticle : MonoBehaviour
{
    public Camera mainCamera;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.enabled = true;
        if (Application.isFocused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = 0;
        transform.position = mouseScreenPosition;
    }
}
