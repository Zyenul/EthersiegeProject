using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{
    public Image uiImage;
    public float displayDuration = 3f;


    public void DisplayImage()
    {
        StartCoroutine(DisplayUIImage());
    }

    private IEnumerator DisplayUIImage()
    {
        uiImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        uiImage.gameObject.SetActive(false);
    }
}
