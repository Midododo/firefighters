using UnityEngine;

namespace Marvest.Sounds
{
    public class SoundEffect : MonoBehaviour
    {
        public AudioClip SeStoneHit;
        public AudioClip SeBulletHit;
        public AudioClip SeIceSlide;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayStoneHitSe()
        {
            audioSource.PlayOneShot(SeStoneHit);
        }

        public void PlayBulletHitSe()
        {
            audioSource.PlayOneShot(SeBulletHit);
        }

        public void IceSlideSe()
        {
            audioSource.PlayOneShot(SeIceSlide);
        }
    }
}