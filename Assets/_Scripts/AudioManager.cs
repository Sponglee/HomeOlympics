
using System.Collections.Generic;
using UnityEngine;

//audio manager object class on scene load
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    private AudioSource source;
    [Range(0f,1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    
   
    //get all the clips to the 'pool'
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play(bool sheduled = false)
    {
        if(source != null)
        {
            source.volume = volume;
            source.pitch = pitch * (1 + Random.Range(-randomPitch / 2, randomPitch / 2));
            if (sheduled)
                source.PlayScheduled(0.1f);
            else
                source.Play();
        }
       
    }

    public void Stop()
    {
        if(source != null)
            source.Stop();
    }

    public void CreateSoundObject(string name)
    {
       

        GameObject _go = new GameObject(name);
        _go.transform.SetParent(AudioManager.Instance.transform);
        this.volume = PlayerPrefs.GetFloat("PlayerVolume", 1f);
        //set the source
        this.SetSource(_go.AddComponent<AudioSource>());
        
    }
}




public class AudioManager : Singleton<AudioManager>
{

    [SerializeField]
    public List<Sound> sounds = new List<Sound>();



    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].CreateSoundObject(sounds[i].name);
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        //no sounds with that name
        Debug.Log("AudioManager: no sounds like that " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();

                return;
            }
        }
        //no sounds with that name
        Debug.Log("AudioManager: no sounds like that " + _name);
    }


    public void VolumeChange (float value)
    {
        PlayerPrefs.SetFloat("PlayerVolume", value);
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = value;
        }
    }
}
