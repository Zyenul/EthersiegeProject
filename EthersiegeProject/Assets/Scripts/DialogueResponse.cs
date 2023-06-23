using UnityEngine;

public class DialogueResponse : MonoBehaviour
{
    public GameObject objectToDestroy;
    public ImageDisplay imageDisplay;

    private void OnDestroy()
    {
        imageDisplay.DisplayImage();
    }
}
