using UnityEngine;

public class Sound : MonoBehaviour
{
    public enum AudioTypes { sfx, music}
    public AudioTypes audioType;

    [HideInInspector] public AudioSource source;
    public string clipName;
    public AudioClip audioClip;
    public bool isLoop;
    public bool playOnAwake;

    [Range(0, 1)]
    public float volume = 0.5f;
}
