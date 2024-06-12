using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;

    [SerializeField] private AudioClip bg;

    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        music.clip = bg;
        music.Play();
    }
}
