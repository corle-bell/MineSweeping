using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    /// <summary>
    /// Image is a textured element in the UI hierarchy.
    /// </summary>

    [AddComponentMenu("UI/UIImageCustom", 110)]
    public class UIImageCustom : Image
    {
        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            switch (type)
            {
                case Type.Simple:
                case Type.Tiled:
                case Type.Filled:
                    base.OnPopulateMesh(toFill);
                    break;
                case Type.Sliced:
                    if (sprite == null)
                    {
                        base.OnPopulateMesh(toFill);
                        return;
                    }
                    break;
            }
        }
    }
}
