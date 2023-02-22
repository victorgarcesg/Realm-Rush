using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;

    private void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        if (currentHitPoints > 0)
        {
            currentHitPoints--;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
