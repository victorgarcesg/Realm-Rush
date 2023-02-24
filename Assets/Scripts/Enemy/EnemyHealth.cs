using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoint when enemy dies.")]
    [SerializeField] int difficultyRamp = 2;
    [SerializeField] float speedRamp = 0.25f;

    [Tooltip("Numbers of deaths required to apply speed ramp")]
    [SerializeField] int deaths = 5;
    int currentDeaths = 0;

    int currentHitPoints = 0;
    Enemy enemy;
    EnemyMover mover;

    private void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        mover = GetComponent<EnemyMover>();
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
            currentDeaths++;
            if (currentDeaths == deaths && mover != null)
            {
                mover.IncreaseSpeed(speedRamp);
            }
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
