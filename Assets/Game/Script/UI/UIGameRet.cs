using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public partial class UIGameRet : UIAnimationRoot
{
    Game game;
	public override void OnOpen(object _data=null)
	{
	    base.OnOpen(_data);
        InitChildCommpent();
        //do open animtion here

        game = _data as Game;
        title.text = game.isWin ? "~Ê¤Àû~" : "!Ê§°Ü!";
        title.color = game.isWin ? Color.green : Color.red;
    }

	public override void OnOpenAnimation()
    {
        //on open animtion start
    }

    public override void OnOpenAnimationEnd()
    {
        //on open animtion end

        if(game.isWin)
        {
            game.audioData.Win();
        }
        else
        {
            game.audioData.Fail();
        }
    }

    public override void OnCloseAnimation()
    {
        //on close animtion start
    }

    public override void OnCloseAnimationEnd()
    {
        base.OnCloseAnimationEnd();

        //on close animtion end

        game.GameStart();
    }

	public override void OnClose()
	{
        base.OnClose();
	    //do close animtion here
	}
}
