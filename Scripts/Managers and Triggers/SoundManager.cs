using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("Sound Clips")]
    [Tooltip("This character's on-melee-attack sounds. Change the size to the number of sounds you wish to add.")]
    [SerializeField] AudioClip[] meleeAttackSounds;
    [Tooltip("This character's on-ranged-attack sounds. Change the size to the number of sounds you wish to add.")]
    [SerializeField] AudioClip[] rangedAttackSounds;
    [Tooltip("This character's on-hurt sounds. Change the size to the number of sounds you wish to add.")]
    [SerializeField] AudioClip[] hurtSounds;
    [Tooltip("This character's walking sounds. Change the size to the number of sounds you wish to add.")]
    [SerializeField] AudioClip[] walkSounds;
    [Tooltip("This character's running sounds. Change the size to the number of sounds you wish to add.")]
    [SerializeField] AudioClip[] runSounds;
    [Tooltip("This character's jumping sounds. Change the size to the number of sounds you wish to add.")]
    [SerializeField] AudioClip[] jumpSounds;
    [Tooltip("This character's on-death sounds. Change the size to the number of sounds you wish to add.")]
    [SerializeField] AudioClip[] deathSounds;

    AudioClip[] defaultMeleeAttackSounds;
    AudioClip[] defaultRangedAttackSounds;
    AudioClip[] defaultHurtSounds;
    AudioClip[] defaultWalkSounds;
    AudioClip[] defaultRunSounds;
    AudioClip[] defaultJumpSounds;
    AudioClip[] defaultDeathSounds;

    AudioSource m_audio;

    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();

        defaultMeleeAttackSounds = meleeAttackSounds;
        defaultRangedAttackSounds = rangedAttackSounds;
        defaultHurtSounds = hurtSounds;
        defaultWalkSounds = walkSounds;
        defaultRunSounds = runSounds;
        defaultJumpSounds = jumpSounds;
        defaultDeathSounds = deathSounds;
    }


    public virtual void LoadMeleeAttackSounds(AudioClip[] newAttackSounds)
    {
        meleeAttackSounds = newAttackSounds;
    }

    public virtual void LoadRangedAttackSounds(AudioClip[] newAttackSounds)
    {
        rangedAttackSounds = newAttackSounds;
    }

    public virtual void LoadHurtSounds(AudioClip[] newHurtSounds)
    {
        hurtSounds = newHurtSounds;
    }

    public virtual void LoadWalkSounds(AudioClip[] newWalkSounds)
    {
        walkSounds = newWalkSounds;
    }

    public virtual void LoadRunSounds(AudioClip[] newRunSounds)
    {
        runSounds = newRunSounds;
    }

    public virtual void LoadJumpSounds(AudioClip[] newJumpSounds)
    {
        jumpSounds = newJumpSounds;
    }

    public virtual void LoadDeathSounds(AudioClip[] newDeathSounds)
    {
        deathSounds = newDeathSounds;
    }


    public virtual void PlayMeleeAttackSound()
    {
        if (meleeAttackSounds.Length == 0)
        {
            return;
        }

        m_audio.PlayOneShot(meleeAttackSounds[Random.Range(0, meleeAttackSounds.Length)]);
    }

    public virtual void PlayRangedAttackSound()
    {
        if (rangedAttackSounds.Length == 0)
        {
            return;
        }

        m_audio.PlayOneShot(rangedAttackSounds[Random.Range(0, rangedAttackSounds.Length)]);
    }

    public virtual void PlayHurtSound(float volume)
    {
        if (hurtSounds.Length == 0)
        {
            return;
        }

        m_audio.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)], volume);
    }

    public virtual void PlayWalkSound()
    {
        if (walkSounds.Length == 0)
        {
            return;
        }

        m_audio.PlayOneShot(walkSounds[Random.Range(0, walkSounds.Length)], 1);
    }

    public virtual void PlayRunSound()
    {
        if (runSounds.Length == 0)
        {
            return;
        }

        m_audio.PlayOneShot(runSounds[Random.Range(0, runSounds.Length)]);
    }

    public virtual void PlayJumpSound()
    {
        if (jumpSounds.Length == 0)
        {
            return;
        }

        m_audio.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
    }

    public virtual void PlayDeathSound()
    {
        if (deathSounds.Length == 0)
        {
            return;
        }

        m_audio.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length)], 1);
    }
}
