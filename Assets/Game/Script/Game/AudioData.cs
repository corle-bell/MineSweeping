using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public class AudioData : MonoBehaviour
{
    public AudioClip bgm;
    public AudioClip boom;
    public AudioClip click;
    public AudioClip flag;
    public AudioClip win;
    public AudioClip fail;

    int bgmHanlde = 0;
    private void OnDestroy()
    {
        AudioManager.instance.StopAll();
    }

    public void StartBgm()
    {
        if(bgmHanlde==0)
        {
            bgmHanlde = AudioManager.instance.PlaySound2D(bgm, true);
        }
    }

    public void StopBgm()
    {
        AudioManager.instance.Stop(bgmHanlde, 0.3f);
        bgmHanlde = 0;
    }

    public void Boom()
    {
        AudioManager.instance.PlaySound2D(boom, true);
    }

    public void Click()
    {
        AudioManager.instance.PlaySound2D(click, true);
    }

    public void Win()
    {
        AudioManager.instance.PlaySound2D(win, true);
    }

    public void Fail()
    {
        AudioManager.instance.PlaySound2D(fail, true);
    }

    public void Flag()
    {
        AudioManager.instance.PlaySound2D(flag, true);
    }
}
