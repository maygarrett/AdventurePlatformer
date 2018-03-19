using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    private static SoundManager _instance = null;

    // music clips
    // [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;

    // audio sources
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _fxSource;
    private AudioSource _playerSource;

    // sound effect clips
    // character sounds
    [SerializeField] private AudioClip footstep1;
    public AudioClip g_jumpGrunt;
    public AudioClip g_throwWhoosh;
    public AudioClip g_zombieDeath;
    public AudioClip g_zombieChase;

    // item sounds
    [SerializeField] private AudioClip smash1;
    [SerializeField] private AudioClip smash2;

    // alternating sounds variables 
    private bool _breakTrigger;

    private void Awake()
    {
        // instance singleton stuff
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }



    // Use this for initialization
    void Start()
    {
        // assign variables
        _playerSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        // play music at start
        PlayMusic(_gameMusic);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static SoundManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public void PlayMusic(AudioClip pClip)
    {
        _musicSource.clip = pClip;
        _musicSource.Play();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void ResumeMusic()
    {
        _musicSource.UnPause();
    }

    public void PlaySoundEffect(AudioClip pClip)
    {
        _fxSource.clip = pClip;
        _fxSource.Play();
    }
    public void PlaySoundEffect(AudioSource pSource, AudioClip pClip)
    {
        pSource.clip = pClip;
        pSource.Play();
    }

    public void PlayPlayerSoundEffect(AudioClip pClip)
    {
        if (_playerSource)
        {
            _playerSource.clip = pClip;
            _playerSource.Play();
        }
    }

    public void PlayBreakSound()
    {
        if (_breakTrigger)
            _fxSource.clip = smash1;
        else _fxSource.clip = smash2;
        _fxSource.Play();
        _breakTrigger = !_breakTrigger;
    }

    public void PlayFootstepSound()
    {
        if (_playerSource)
        {   _playerSource.clip = footstep1;
            _playerSource.Play(); }
    }

}
