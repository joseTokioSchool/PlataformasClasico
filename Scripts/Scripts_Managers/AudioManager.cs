using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*--------------------------------------------------- SINGLETONS --------------------------------------------------- */
    public static AudioManager AudioInstance { get; private set; }

    private void Awake()
    {
        if (AudioInstance != null && AudioInstance != this)
        {
            Destroy(this);
        }
        else
        {
            AudioInstance = this;
        }
    }
    /*------------------------------------------------------------------------------------------------------------------ */

    [SerializeField] AudioSource audioSource;

    [Header("Canvas")]
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip pauseClip;
    [SerializeField] AudioClip extraLifeClip;

    [Header("Player")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip shrinkClip;
    [SerializeField] AudioClip kickClip;
    [SerializeField] AudioClip dieClip;

    [Header("Items")]
    [SerializeField] AudioClip coinClip;
    [SerializeField] AudioClip flagPoleClip;
    [SerializeField] AudioClip itemClip;
    [SerializeField] AudioClip powerUpClip;

    public void GameOverClip()
    {
        audioSource.PlayOneShot(gameOverClip);
    }
    public void PauseClip()
    {
        audioSource.PlayOneShot(pauseClip);
    }
    public void ExtraLifeClip()
    {
        audioSource.PlayOneShot(extraLifeClip);
    }
    public void JumpClip()
    {
        audioSource.PlayOneShot(jumpClip);
    }
    public void ShrinkClip()
    {
        audioSource.PlayOneShot(shrinkClip);
    }
    public void KickClip()
    {
        audioSource.PlayOneShot(kickClip);
    }
    public void DieClip()
    {
        audioSource.PlayOneShot(dieClip);
    }
    public void CoinClip()
    {
        audioSource.PlayOneShot(coinClip);
    }
    public void FlagClip()
    {
        audioSource.PlayOneShot(flagPoleClip);
    }
    public void ItemClip()
    {
        audioSource.PlayOneShot(itemClip);
    }
    public void PowerUpClip()
    {
        audioSource.PlayOneShot(powerUpClip);
    }
}
