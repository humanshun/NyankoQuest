using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM : MonoBehaviour
{
    private AudioSource audioSource;
    private static AudioM instance;
    public static AudioM Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioM>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioM");
                    instance = obj.AddComponent<AudioM>();
                }
            }
            return instance;
        }
    }

    void Awake()
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
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
