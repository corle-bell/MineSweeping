using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImage_AjustSprite : Image {

	public bool useConentSize=false;

	public void SetSprite(Sprite _sprite)
	{
		if(useConentSize)
        {
			this.sprite = _sprite;
			return;
		}
		Vector2 size = Vector2.zero;
		size.x = _sprite.rect.width;
		size.y = _sprite.rect.height;
		this.sprite = _sprite;
		rectTransform.sizeDelta = size;
	}
}

