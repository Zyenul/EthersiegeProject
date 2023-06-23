using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackBoxCollected : MonoBehaviour
{

    public GameObject loreText;
    public GameObject loreTitle;
    public RawImage loreImage;
    public RawImage bgImage;
    public Button button;

    private Text tmp; // Reference to the Text component

    private bool hasCollided = false;
    private float previousTimeScale;

    [SerializeField]
    GameObject objectToDestroy;

    private void Start()
    {
        tmp = loreText.GetComponent<Text>(); // Get the Text component at runtime
        loreText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f; // Pause the game

            loreText.gameObject.SetActive(true);
            loreTitle.gameObject.SetActive(true);
            loreImage.gameObject.SetActive(true);
            bgImage.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
            button.onClick.AddListener(CollectLore);
            BBCounter.loreCollect += 1;
        }
    }

    private void DestroyGameObject()
    {
       
            Destroy(objectToDestroy);
    }

    public void CollectLore()
    {
        // Handle button click event
        Time.timeScale = previousTimeScale; // Resume the game
        loreText.gameObject.SetActive(false);
        loreImage.gameObject.SetActive(false);
        bgImage.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        loreTitle.gameObject.SetActive(false);
        Destroy(objectToDestroy);
        

    }

}
