using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health Attributes")]
    [Tooltip("This character's default health.")]
    [SerializeField] int defaultHealth;
    [Tooltip("This character's default defense stat.")]
    [SerializeField] int defaultArmor;
    [Tooltip("This character's default barrier amount.")]
    [SerializeField] int defaultBarrier;

    [Space]
    [Header("Damage Attributes")]
    [Tooltip("This character's default damage.")]
    [SerializeField] int defaultDamage;
    [Tooltip("This character's default attack speed.")]
    [SerializeField] int defaultAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
