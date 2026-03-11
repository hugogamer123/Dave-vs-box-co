using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] AudioClip BangSfx;
    [SerializeField] AudioSource SfxSource;

    public void PlayClip()
    {
        if(SfxSource.isPlaying)
        {
            SfxSource.Stop();
            SfxSource.Play();
        }
        else
        {
            SfxSource.Play();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
