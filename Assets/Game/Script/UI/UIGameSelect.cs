using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public partial class UIGameSelect : UIAnimationRoot
{
    public Game game;
	public override void OnOpen(object _data=null)
	{
	    base.OnOpen(_data);
        InitChildCommpent();
        //do open animtion here
        game = _data as Game;

    }

	public override void OnOpenAnimation()
    {
        //on open animtion start
    }

    public override void OnOpenAnimationEnd()
    {
        //on open animtion end
    }

    public override void OnCloseAnimation()
    {
        //on close animtion start
    }

    public override void OnCloseAnimationEnd()
    {
        base.OnCloseAnimationEnd();

        //on close animtion end


        game.Width = int.Parse(this.Width.text);
        game.Height = int.Parse(this.Height.text);
        game.LandmineNumber = int.Parse(this.MineNum.text);


        game.GameInit();
    }

    public void OnClick()
    {
        int t_Width = int.Parse(this.Width.text);
        int t_Height = int.Parse(this.Height.text);
        int t_LandmineNumber = int.Parse(this.MineNum.text);

        if (!MathTools.Between(t_Width, 5, _Width))
        {
            UITostaManager.instance.ShowToast("列数不符标准");
            return;
        }
        if (!MathTools.Between(t_Height, 5, _Height))
        {
            UITostaManager.instance.ShowToast("行数不符标准");
            return;
        }
        if (!MathTools.Between(t_LandmineNumber, 0, t_Width*t_Height))
        {
            UITostaManager.instance.ShowToast("雷的数量不符合标准");
            return;
        }

        this.Close();
    }

	public override void OnClose()
	{
        base.OnClose();
	    //do close animtion here
	}
}
