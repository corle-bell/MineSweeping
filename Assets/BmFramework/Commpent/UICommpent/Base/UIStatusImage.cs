using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusImage : UIImage_AjustSprite
{
    public Sprite[] status;
    public bool isAjustSprite=false;

    public void SetStatus(int _status)
    {
        if(FrameworkTools.IsInArray(status, _status) && status[_status]!=null)
        {
            if(isAjustSprite)
            {
                SetSprite(status[_status]);
            }
            else
            {
                this.sprite = status[_status];
            }
            //this.canvasRenderer.cull = false;
            color = ColorTools.SetAlpha(color, 1);
        }
        else
        {
            //this.canvasRenderer.cull = true;
            color = ColorTools.SetAlpha(color, 0);
        }
        
    }
}
