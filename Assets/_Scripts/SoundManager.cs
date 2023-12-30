using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //SoundManager is a script tasked with the management and creation of audiosources and music.
    //There is 2 categories, 2D Sounds and Music.
     
    //To use it just have the sound clip on another object and use...
    //SoundManager.GetSoundManager().TryPlay2DSound(audioClip)
    //SoundManager.GetSoundManager().TryPlayMusic(audioClip)

    static SoundManager instance = null;

    private List<AudioSource> soundAudioSources = new List<AudioSource>();
    private AudioSource musicAudioSources;

    private GameObject sound;
    private GameObject music;

    static public SoundManager Get()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            instance.sound = new GameObject("Sounds");
            instance.sound.transform.SetParent(gameObject.transform);

            instance.music = new GameObject("Music");
            instance.music.transform.SetParent(gameObject.transform);

            GameObject.DontDestroyOnLoad(gameObject);
            DependencyInjector.AddDependency<SoundManager>(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void TryPlaySound(AudioClip audioClip, bool loop = false)
    {
        if (CanPlaySound(audioClip))
        {
            int slot = -1;
            if (SoundSourceAvailable(out slot) == true)
            {
                OverwriteSoundClip(slot, audioClip, loop);
                PlaySoundSource(slot);
            }
            else
            {
                AddNewSoundSource(loop);
                TryPlaySound(audioClip);
            }
        }
    }

    bool CanPlaySound(AudioClip soundClip) { return soundClip != null; }

    public void TryPlayMusic(AudioClip musicClip)
    {
        if (CanPlayMusic(musicClip))
        {
            if (MusicSourceAvailable() == false)
            {
                GenerateMusicSource();
                OverwriteMusicClip(musicClip);
            }
            else
            {
                PlayMusic(play: false);
                OverwriteMusicClip(musicClip);
            }

            if(musicAudioSources.clip != null ||
                musicAudioSources.clip != musicClip)
            { PlayMusic(play: true); }
        }
    }

    bool CanPlayMusic(AudioClip newMusicClip)
    {
        return newMusicClip != null;
    }

    void OverwriteMusicClip(AudioClip musicClip)
    {
        musicAudioSources.clip = musicClip;
    }

    void PlayMusic(bool play)
    {
        if (play == true) { musicAudioSources.Play(); }
        else { musicAudioSources.Stop(); }
    }

    void GenerateMusicSource()
    {
        GameObject tempObject = new GameObject();
        tempObject.name = "MusicSource";

        AudioSource source = tempObject.AddComponent<AudioSource>();
        source.loop = true;
        source.playOnAwake = true;
        tempObject.transform.SetParent(music.transform);

        musicAudioSources = source;
    }

    bool MusicSourceAvailable()
    {
        return music.transform.childCount != 0;
    }

    bool SoundSourceAvailable(out int audioSourceSlot)
    {
        List<AudioSource> targetSource = new List<AudioSource>();
        targetSource = soundAudioSources;

        for (int i = 0; i < targetSource.Count; i++)
        {
            if (targetSource[i].isPlaying == false)
            {
                audioSourceSlot = i;
                return true;
            }
        }

        audioSourceSlot = -1;
        return false;
    }

    void AddNewSoundSource(bool loop = false)
    {
        GameObject l_TempObject = new GameObject();
        l_TempObject.name = "SoundSource" + sound.transform.childCount.ToString();

        AudioSource audio = l_TempObject.AddComponent<AudioSource>();
        audio.loop = loop;
        audio.spatialBlend = 0.0f; 

        audio.playOnAwake = true;
        l_TempObject.transform.SetParent(sound.transform);
        soundAudioSources.Add(audio);
    }

    void OverwriteSoundClip(int slot, AudioClip audioClip, bool loop = false)
    {
        soundAudioSources[slot].clip = audioClip;
        soundAudioSources[slot].loop = loop;
    }

    void PlaySoundSource(int slot)
    {
        soundAudioSources[slot].Play();
    }
}
