using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static MovingObjectInfomation[] PlayerInfomations = new MovingObjectInfomation[3];
    //避難用の入れ物　DecideButtonとGameManagerを橋渡しする

    public enum BGM_Type
    {
        Title,
        Stage,
        MonsterHouse,
        GameOver,
        GameClear
    }

    public enum SE_Type
    {
        click,
        gameStart,
        nameError,
        selectMove,
        enemyDie1,
        enemyDie2,
        enemyDie3,
        playerDie1,
        playerDie2,
        playerDie3,
        levelUp1,
        levelUp2,
        levelUp3,
        normalAttack1,
        normalAttack2,
        fire1,
        fire2,
        water1,
        water2,
        wind1,
        wind2,
        stone1,
        stone2,
        cannotAttack,
        chant,
        missChant,
        walk,
        fireSE,
        waterSE,
        windSE,
        stoneSE,
        exit,
        exit2,
        exitSE,
        cannotClick,
        alreadyMaxPoint,
        runOutOfPoint,
        cannotAssignPoint,
        gameOverVoice,
        agilityUp1,
        agilityUp2,
        areaHeal1,
        areaHeal2,
        attackUp1,
        attackUp2,
        brust1,
        brust2,
        burn1,
        burn2,
        comet1,
        comet2,
        defenceUp1,
        defenceUp2,
        freeze1,
        freeze2,
        heal1,
        heal2,
        phoenix1,
        phoenix2,
        speedUp1,
        speedUp2,
        sprash1,
        sprash2,
        aura,
        ice,
        healEffect,
        quake1,
        quake2,
        excessMp,
        itemGet1,
        itemGet2,
        itemGet3,
        bigBurn1,
        bigBurn2,
        explosion1,
        explosion2,
        healveil1,
        healveil2,
        inferno1,
        inferno2,
        meteo1,
        meteo2,
        resurrection1,
        resurrection2,
        stome1,
        stome2,
        superNova1,
        superNova2,
        thundar1,
        thudar2,
        volcano1,
        volcano2,
        craft1,
        warp1,
        craft,
        thundarSE,
        warp,
        monsterHouseVoice,
        secondSightVoice,
        cannotPickUpItem,
        portionVoie,
        ettherVoice
    }

    public float BGM_Volume = 0.1f;
    public float SE_Vloume = 0.2f;
    public bool Mute = false;

    public AudioClip[] IntroBGM_Clips;
    public AudioClip[] LoopBGM_Clips;
    public AudioClip[] SE_Clips;
    public AudioClip[] ManualSE_Clips;

    public const float CROSS_FADE_TIME = 1.0f;

    private AudioSource IntroBGM_Source = new AudioSource();
    private AudioSource LoopBGM_Source=new AudioSource();
    private AudioSource[] SE_Sources = new AudioSource[16];
    private AudioSource ManualSE_Sources = new AudioSource();

    private int CurrentBGMIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        IntroBGM_Source= gameObject.AddComponent<AudioSource>();
        LoopBGM_Source = gameObject.AddComponent<AudioSource>();
        IntroBGM_Source.loop = false;
        LoopBGM_Source.loop = true;
    

        for (int i=0;i< SE_Sources.Length; i++)
        {
            SE_Sources[i] = gameObject.AddComponent<AudioSource>();
        }
        ManualSE_Sources = gameObject.AddComponent<AudioSource>();
    }

    public void Update()
    {
        if (IntroBGM_Source.clip != null)
        {
            if (!IntroBGM_Source.isPlaying)
            {
                LoopBGM_Source.Play();
                IntroBGM_Source.clip = null;
            }
        }
    }

    public void PlayBGM(BGM_Type bgmType)
    {
        int index = (int)bgmType;
        CurrentBGMIndex = index;
        if (index<0 || index >= IntroBGM_Clips.Length)
        {
            return;
        }
        if (IntroBGM_Source.clip != null)
        {
            if (IntroBGM_Source.clip.Equals(IntroBGM_Clips[index]))
            {
                return;
            }
        }
        if (LoopBGM_Source.clip != null)
        {
            if (LoopBGM_Source.clip.Equals(LoopBGM_Clips[index]))
            {
                return;
            }
        }

        if (index == 4)
        {
            BGM_Volume = 0.35f;
        }
        else
        {
            BGM_Volume = 0.1f;
        }

        IntroBGM_Source.clip = IntroBGM_Clips[index];
        LoopBGM_Source.clip = LoopBGM_Clips[index];
        IntroBGM_Source.Play();
     //   LoopBGM_Source.PlayScheduled(AudioSettings.dspTime + IntroBGM_Clips[index].length);

        IntroBGM_Source.volume = BGM_Volume;
        LoopBGM_Source.volume = BGM_Volume;
    }

    public void StopBGM()
    {
        IntroBGM_Source.Stop();
        IntroBGM_Source.clip = null;
        LoopBGM_Source.Stop();
        LoopBGM_Source.clip = null;
    }

    public void PlaySE(SE_Type seType)
    {
        int index = (int)seType;
        if (index < 0 || index >= SE_Clips.Length)
        {
            return;
        }
        foreach(AudioSource source in SE_Sources)
        {
            if (!source.isPlaying)
            {
                source.clip = SE_Clips[index];
                source.Play();
                source.volume = SE_Vloume;
                return;
            }
        }
    }

    public void StopSE()
    {
        foreach (AudioSource source in SE_Sources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    public void PlayManualSE(int index)
    {
        Debug.Log(index);
        if (index < 0 || index >= ManualSE_Clips.Length)
        {
            return;
        }
        ManualSE_Sources.Stop();
        ManualSE_Sources.clip = ManualSE_Clips[index];
        ManualSE_Sources.Play();
        ManualSE_Sources.volume = SE_Vloume;
    }

}
