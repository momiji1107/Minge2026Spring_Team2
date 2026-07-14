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
    [SerializeField] private AudioClip bossBGMClip;
    
    void Start()
    {
        StartGameBGM();
    }

    //ゲームオーバー時に実行
    public IEnumerator GameOver()
    {
        bgmAudioSource.Stop(); //BGMを止める
        bgmAudioSource.loop = false;
        seAudioSource.PlayOneShot(gameOverClip); //ゲームオーバー効果音
        yield return new WaitForSecondsRealtime(0.5f);
        bgmAudioSource.clip = gameOverBGMClip; //ゲームオーバーBGM
        bgmAudioSource.Play();
    }

    //ゲームクリア時に実行
    public IEnumerator GameClear()
    {
        bgmAudioSource.Stop(); //BGMを止める
        bgmAudioSource.loop = false;
        seAudioSource.PlayOneShot(clearClip); //ゲームクリア効果音
        
        yield return new WaitForSecondsRealtime(0.5f);
        bgmAudioSource.clip = clearBGMClip; //ゲームクリアBGM
        bgmAudioSource.Play();
    }

    //選択音を鳴らす
    public void Select()
    {
        seAudioSource.PlayOneShot(selectClip);
    }

    //決定音を鳴らす
    public void Confirm()
    {
        seAudioSource.PlayOneShot(confirmClip);
    }

    //攻撃音を鳴らす
    public void Attack(AudioClip attackClip)
    {
        seAudioSource.PlayOneShot(attackClip);
    }

    //ボス出現BGMを流す
    public void BossAppear()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.loop = false;
        bgmAudioSource.clip = bossAppearClip;
        bgmAudioSource.Play();
        StartCoroutine(Common.DelayCall(StartBossBGM, 9.0f));
    }

    //呼び出す側にAudio Clipを登録し、一度音を鳴らしたいときに使用する
    public void OneShot(AudioClip clip)
    {
        seAudioSource.PlayOneShot(clip);
    }
    
    //BGMをランダムで流し始める
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
    
    //ボスBGMを流す
    private void StartBossBGM()
    {
        if (!bgmAudioSource.isPlaying)
        {
            bgmAudioSource.loop = true;
            bgmAudioSource.clip = bossBGMClip;
            bgmAudioSource.Play();
        }
    }
    
}
