using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealZone : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public Image uiImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayParticleEffect();
            uiImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiImage.gameObject.SetActive(false);
        }
    }

    private void PlayParticleEffect()
    {
        if (particleEffect != null)
        {
            // Play the particle effect at the desired position
            ParticleSystem spawnedEffect = Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(spawnedEffect.gameObject, particleEffect.main.duration);
        }
    }
}
