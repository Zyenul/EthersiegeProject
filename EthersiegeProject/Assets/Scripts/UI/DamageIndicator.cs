using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public GameObject damageOverlay; // Reference to the damage overlay object
    public string projectileTag = "ProjectileEnemy"; // Tag of the projectiles that can cause damage

    private Animator damageOverlayAnimator; // Reference to the animator component of the damage overlay

    private void Start()
    {
        damageOverlayAnimator = damageOverlay.GetComponent<Animator>(); // Get the Animator component
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(projectileTag))
        {
            // Play the damage overlay animation
            PlayDamageOverlayAnimation();
        }

        if (collision.gameObject.CompareTag("Barrier"))
        {
            // Play the damage overlay animation
            PlayDamageOverlayAnimation();
        }
    }

    private void PlayDamageOverlayAnimation()
    {
        if (damageOverlayAnimator != null)
        {
            damageOverlayAnimator.SetTrigger("PlayDamage"); // Trigger the 'PlayDamage' animation state
        }
    }
}