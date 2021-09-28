using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using UnityEditor.SceneManagement;

public class SceneNode{
	public string name;
	public string fullName;
	public string assetName;
	public string nickName;
	public bool isInBuild;
	public bool isShow = true;
}

public class SceneWindowEditor : EditorWindow
{
    [MenuItem("Tools/场景管理器/场景速查", false)]
    static void Open()
    {
		EditorWindow.GetWindow<SceneWindowEditor>(false, "场景速查", true).Show();
    }

	[MenuItem("Tools/场景管理器/快速启动  %F5", false)]
    static void StartMainScene()
    {
		if(launchScene == null)
        {
			return;
        }
        
    }

	[MenuItem("Tools/场景管理器/回到运行前场景 %LEFT", false)]
    static void BackToEditorScene()
    {
        if(SceneManager.GetActiveScene().name.Equals(lastScene.name)==false)
		{
			 EditorSceneManager.OpenScene(lastScene.assetName);
		}
    }


	public static SceneNode launchScene;
	public string scanPath = "Assets/3D";
	private static List<SceneNode> sceneNodes = new List<SceneNode>();
	private static bool isScan = false;
	private bool isOnlyShowBuild;
	
	public static SceneNode lastScene;
    Vector2 scrollPos;
	private void OnEnable()
	{
		scanPath = EditorPrefs.GetString(PlayerSettings.productName+"SceneWindowEditor_Path", scanPath);

		if (isScan==false)
		{
			Scan();
			isScan = true;
		}
		
	}

	private static SceneNode FindScene(string _name)
	{
		for(int i=0;i<sceneNodes.Count; i++)
		{
			if(sceneNodes[i].nickName==_name)
			{
				return sceneNodes[i];
			}
		}
		return null;
	}

	bool CheckIsInBuild(SceneNode node)
	{
		EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        for (int i = 0; i < scenes.Length; ++i)
        {
            if(scenes[i].path.IndexOf(node.assetName.Replace("\\", "/"))>=0)
			{
				return true;
			}
        }
		return false;
	}

	void Scan()
	{
		sceneNodes.Clear();

		string fullPath = scanPath;  //路径

		string current = EditorPrefs.GetString("SceneManager_Launch", "");
		string lastScenestr = EditorPrefs.GetString("SceneManager_Last", "");

		CheckFolder(fullPath, current, lastScenestr);

		sceneNodes.Sort((left, right) =>
            {
				if(launchScene!=null)
                {
					if (left.name == launchScene.name)
					{
						return -1;
					}
					if (right.name == launchScene.name)
					{
						return 1;
					}
				}
                return left.name.CompareTo(right.name);
            });

		UpTheLaunch();
	}

	void UpTheLaunch()
    {

	}

	void CheckFolder(string _path, string current, string lastScenestr)
    {
		//获取指定路径下面的所有资源文件

		string[] resFiles = AssetDatabase.FindAssets("t:Scene", new string[] { _path });
		if (resFiles!=null)
		{
			for (int i = 0; i < resFiles.Length; i++)
			{
				string assetName = AssetDatabase.GUIDToAssetPath(resFiles[i]); ;
				string[] sarr = assetName.Split(new char[] { '/' });
				SceneNode tt = new SceneNode();
				tt.name = sarr[sarr.Length-1];
				tt.assetName = assetName;
				tt.fullName = assetName;
				tt.nickName = tt.name.Substring(0, tt.name.IndexOf(".unity"));
				tt.isInBuild = CheckIsInBuild(tt);
				sceneNodes.Add(tt);

				if (current == tt.nickName && launchScene == null)
				{
					launchScene = tt;
				}

				if (lastScenestr == tt.nickName && lastScene == null)
				{
					lastScene = tt;
				}
			}
		}
		
	}
	
	void OpenScene(SceneNode node, bool isAdd=false)
	{
		if(isAdd==false)
		{
			if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(node.nickName))
			{
				EditorSceneManager.OpenScene(node.assetName);
			}
		}
		else
		{
			EditorSceneManager.OpenScene(node.assetName, OpenSceneMode.Additive);
		}
	}

	void PlayScene(SceneNode node)
	{
		OpenScene(node);
		EditorApplication.ExecuteMenuItem("Edit/Play");
	}

	void RemoveSceneToBuild(SceneNode node)
    {

    }

	void AddSceneToBuild(SceneNode node)
	{
		EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

		EditorBuildSettingsScene[] newScene = new EditorBuildSettingsScene[scenes.Length+1];
        for (int i = 0; i < scenes.Length; ++i)
        {
            newScene[i] = scenes[i];
        }
		newScene[scenes.Length] = new EditorBuildSettingsScene(node.assetName, true);
		EditorBuildSettings.scenes = newScene;
		node.isInBuild = true;
	}

    void OnGUI()
    {
		if (GUILayout.Button("刷新", GUILayout.MaxWidth(100)))
		{
			Scan();
		}

		EditorGUILayout.BeginHorizontal();
		scanPath = EditorGUILayout.TextField("路径:", scanPath);
		if(GUILayout.Button("保存"))
        {
			EditorPrefs.SetString(PlayerSettings.productName + "SceneWindowEditor_Path", scanPath);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.HelpBox("选项", MessageType.Info);
		GUILayout.Space(5);
		GUILayout.BeginHorizontal();
		
		EditorGUILayout.LabelField("是否只显示Build场景:", GUILayout.MaxWidth(120));
		isOnlyShowBuild = EditorGUILayout.Toggle(isOnlyShowBuild, GUILayout.MaxWidth(30));
		GUILayout.EndHorizontal();


		EditorGUILayout.HelpBox("场景列表---------------快速启动：Ctrl+F5   返回启动前的上一场景:Ctrl+Left", MessageType.Info);
		GUILayout.Space(5);		
		scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
      
		for(int i=0; i<sceneNodes.Count; i++)
		{
			var t = sceneNodes[i];
			if(t!=null && ((isOnlyShowBuild && t.isInBuild) || !isOnlyShowBuild) )
			{
				GUILayout.BeginHorizontal();

				GUILayout.Space(5);

				int styleId = (launchScene==null)?149:(launchScene.name==t.name?150:149);				
				GUILayout.Label(t.nickName, GUI.skin.customStyles[styleId], GUILayout.MaxWidth(120), GUILayout.MinHeight(30));

				GUILayout.Space(5);

				GUILayout.Label("", GUI.skin.customStyles[8], GUILayout.MaxWidth(30), GUILayout.MaxHeight(30));
				
				if (GUILayout.Button("复制名字", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
				{
					GUIUtility.systemCopyBuffer = t.nickName;
				}
				if(!t.isInBuild)
				{
					if (GUILayout.Button("添加到Build", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
					{
						AddSceneToBuild(t);
					}
				}
				else
				{
					if (GUILayout.Button("从Build中移除", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
					{
						RemoveSceneToBuild(t);
					}
				}
				
				if(launchScene==null || launchScene.name!=t.name)
                {
                    if (GUILayout.Button("设置为启动场景", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
                    {
                        launchScene = t;
                        EditorPrefs.SetString("SceneManager_Launch", launchScene.nickName);

						UpTheLaunch();

					}
                }
                else
                {
                    GUILayout.Label("启动场景", GUI.skin.customStyles[81], GUILayout.MaxWidth(100), GUILayout.MinHeight(30));
                }



				if (GUILayout.Button("打开", GUILayout.MaxWidth(50), GUILayout.MinHeight(30)))
				{
					OpenScene(t);
				}
				if (GUILayout.Button("叠加打开", GUILayout.MaxWidth(100), GUILayout.MinHeight(30)))
				{
					OpenScene(t, true);
				}
				if (GUILayout.Button("运行", GUILayout.MaxWidth(50), GUILayout.MinHeight(30)))
				{
					PlayScene(t);
				}

				
				GUILayout.EndHorizontal(); 
		

				GUILayout.Space(20);
			}
		}

		GUILayout.EndScrollView();
    }

	public static void FastRun()
	{
		if (!SceneManager.GetActiveScene().name.Equals(launchScene.name))
		{
			EditorPrefs.SetString("SceneManager_Last", SceneManager.GetActiveScene().name);
			EditorSceneManager.OpenScene(launchScene.assetName);
		}

		EditorApplication.ExecuteMenuItem("Edit/Play");
	}
}