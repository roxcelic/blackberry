using UnityEngine;

public class audio : MenuItems {

    [Header("components")]
    public AudioSource aS;
    
    [Header("sounds")]
    public AudioClip[] sounds;   
    public int CurrentClip = 0;

    protected override void Start() {
        base.Start();

        aS = GetComponent<AudioSource>();
    }


    protected override void Action() {
        aS.clip = sounds[CurrentClip];
        aS.Play();

        CurrentClip++;

        if (CurrentClip > sounds.Length - 1)
            CurrentClip = 0;
    }

}