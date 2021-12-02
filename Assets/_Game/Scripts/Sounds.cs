using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds current;
    public AudioClip paintSplatter;
    public AudioClip footstep;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip music;

    private void Awake()
    {
        current = this;
    }
}
