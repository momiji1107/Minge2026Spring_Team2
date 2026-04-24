using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [Header("効果音関係")]
    [SerializeField] private AudioSource seAudioSource;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip clearClip;
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip confirmClip;
    
    [Header("BGM関係")]
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioClip[] bgmClip;
    [SerializeField] private AudioClip gameOverBGMClip;
    [SerializeField] private AudioClip clearBGMClip;
    [SerializeField] private AudioClip bossAppearClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGameBGM();
    }

    public IEnumerator GameOver()
    {
        bgmAudioSource.Stop(); //BGMを止める
        bgmAudioSource.loop = false;
        seAudioSource.PlayOneShot(gameOverClip); //ゲームオーバー効果音
        yield return new WaitForSecondsRealtime(0.5f);
        bgmAudioSource.clip = gameOverBGMClip; //ゲームオーバーBGM
        bgmAudioSource.Play();
    }

    public IEnumerator GameClear()
    {
        bgmAudioSource.Stop(); //BGMを止める
        bgmAudioSource.loop = false;
        seAudioSource.PlayOneShot(clearClip); //ゲームクリア効果音
        
        yield return new WaitForSecondsRealtime(0.5f);
        bgmAudioSource.clip = clearBGMClip; //ゲームクリアBGM
        bgmAudioSource.Play();
    }

    public void Select()
    {
        seAudioSource.PlayOneShot(selectClip);
    }

    public void Confirm()
    {
        seAudioSource.PlayOneShot(confirmClip);
    }

    public void Attack(AudioClip attackClip)
    {
        seAudioSource.PlayOneShot(attackClip);
    }

    public void BossAppear()
    {
        //ボス出現BGMを流す
        bgmAudioSource.Stop();
        bgmAudioSource.loop = false;
        bgmAudioSource.clip = bossAppearClip;
        bgmAudioSource.Play();
        StartCoroutine(Common.DelayCall(StartGameBGM, 10.0f));
    }

    private void StartGameBGM()
    {
        if (!bgmAudioSource.isPlaying)
        {
            bgmAudioSource.loop = true;
            int idx = Random.Range(0, bgmClip.Length);
            bgmAudioSource.clip = bgmClip[idx];
            bgmAudioSource.Play();
        }
    }
}
