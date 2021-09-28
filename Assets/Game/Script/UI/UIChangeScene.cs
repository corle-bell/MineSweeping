using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BmFramework.Core;

public partial class UIChangeScene : UIAnimationRoot
{
    string sceneName;
	public override void OnOpen(object _data=null)
	{
	    base.OnOpen(_data);

        //do open animtion here
        sceneName = _data as string;

    }

	public override void OnOpenAnimation()
    {
        //on open animtion start
    }

    public override void OnOpenAnimationEnd()
    {
        //on open animtion end
        if(sceneName==null)
        {
            GameHelper.LoadLevelAsync((AsyncOperation ao) => {
                this.Close();
            });
        }
        else if (sceneName == "Reload")
        {
            GameHelper.RealodLevel();
            this.Close();
        }
        else
        {
            SceneHelper.LoadSceneAsync(sceneName, (AsyncOperation ao) => {
                this.Close();
            });
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
    }

	public override void OnClose()
	{
        base.OnClose();
	    //do close animtion here
	}
}
