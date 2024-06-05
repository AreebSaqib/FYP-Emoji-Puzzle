using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraadjustmen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform canvasRectTransform;
    private void Start()
    {
        AdjustCamera();
    }

    private void AdjustCamera()
    {
        float targetAspectRatio = canvasRectTransform.rect.width / canvasRectTransform.rect.height;
        float deviceAspectRatio = (float)Screen.width / Screen.height;
        float scaleFactor;

        if (deviceAspectRatio >= targetAspectRatio)
        {
          

            // Fit the width and adjust the height accordingly
            scaleFactor = Screen.width / canvasRectTransform.rect.width;
        }
        else
        {
           
            // Fit the height and adjust the width accordingly
            scaleFactor = Screen.height / canvasRectTransform.rect.height;
        }

        // Calculate the new size and position for the camera viewport rect
        float scaledWidth = canvasRectTransform.rect.width * scaleFactor;
        float scaledHeight = canvasRectTransform.rect.height * scaleFactor;
        float posX = (Screen.width - scaledWidth) / 2f;
        float posY = (Screen.height - scaledHeight) / 2f;

        // Apply the new viewport rect to the camera
        mainCamera.pixelRect = new Rect(posX, posY, scaledWidth, scaledHeight);
    }
}
