using UnityEngine;
using UnityEngine.UI;

public class CrosshairControllerUI : MonoBehaviour {
    public Image crosshairImage;

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update() {
        // Move UI element to mouse screen position
        crosshairImage.rectTransform.position = Input.mousePosition;
    }
}