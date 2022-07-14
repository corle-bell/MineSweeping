using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bm.EditorTool;


namespace Bm.EditorTool.Scene
{
	public enum FlagType
	{
		None,
		CommonlyUse,
		BuildUsing,
		Unused,
	}


	[AssetCreator]
	public class SceneToolsSetting : ScriptableObject
	{
		public FlagType flagFilter;
		public List<string> scanPath = new List<string>();
		public List<SceneNode> sceneNodes = new List<SceneNode>();
		public bool isScan = false;
		public bool isOnlyShowBuild;
	}

	[System.Serializable]
	public class SceneNode
	{
		public string name;
		public string fullName;
		public string assetName;
		public string nickName;		
		public bool isInBuild;
		public FlagType flag;
		public bool isShow = true;
	}
}

