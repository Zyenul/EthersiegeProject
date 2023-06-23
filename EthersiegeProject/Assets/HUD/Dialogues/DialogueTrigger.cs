using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Image uiImage;
    public float displayTime = 3f;

    private float timer;
    private bool displayActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiImage.gameObject.SetActive(true);
            displayActive = true;
            timer = displayTime;
        }
    }

    private void Update()
    {
        if (displayActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                uiImage.gameObject.SetActive(false);
                displayActive = false;
                gameObject.SetActive(false);
            }
        }
    }
}
