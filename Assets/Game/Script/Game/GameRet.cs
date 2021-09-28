using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRet : MonoBehaviour
{
    Game game;
    public Text text;
    public void Init(Game _game)
    {
        game = _game;
    }

    public void Show(bool isWin)
    {
        text.text = isWin ? "You Win" : "You Lose";
        this.SetActive(true);
    }

    public void Restart()
    {
        game.GameStart();
        this.SetActive(false);
    }
}
