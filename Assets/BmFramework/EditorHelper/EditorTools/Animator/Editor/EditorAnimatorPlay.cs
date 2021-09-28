#region 脚本说明
/*----------------------------------------------------------------
// 脚本作用：实现编辑器模式下播放预设的动画，便于特效对位
// 创建者：黑仔
// Modify : Badman
//----------------------------------------------------------------*/
#endregion

using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;
using System.Collections.Generic;

public class AnimatorData
{
	public bool isPreview;
	public Animator animator;
	public int id;
}

public class EditorAnimatorPlay : EditorWindow
{

	private List<AnimatorState> stateNames = new List<AnimatorState>();

	private int aniLieBiaoID = 0;

	private GameObject go;
	private Animator animator;
	private AnimatorData animatorData;
	private AnimatorController aniConS;
	private AnimatorState aniState;

	protected float clipTime = 0.0f;
	private string[] aniLieBiaoString;
	private bool playtime = false;
	protected bool lockSelection = false;
	private bool hongPei = false;
	private string aniZhuangTai = "";

	/// <summary>
	/// 时间增量
	/// </summary>
	private double delta;

	/// <summary>
	/// 当前运行时间
	/// </summary>
	private float m_RunningTime;

	/// <summary>
	/// 上一次系统时间
	/// </summary>
	private double m_PreviousTime;

	/// <summary>
	/// 最大时间长度	
	/// </summary>
	private float aniTime = 0.0f;

	private float RecordEndTime = 0;

	private bool isInspectorUpdate = false;

	private Dictionary<int, AnimatorData> animtorCache = new Dictionary<int, AnimatorData>();

	private void ChuShiHua()
	{
		stateNames.Clear();
		aniLieBiaoID = animatorData.id;
		clipTime = 0.0f;
		aniLieBiaoString = null;
		playtime = false;
		lockSelection = !animatorData.isPreview;
	}

	[MenuItem("Tools/AnimatorController/编辑模式动画播放")]
	public static void DoWindow()
	{
		GetWindow<EditorAnimatorPlay>("编辑模式动画播放");
	}

	public void OnSelectionChange()
	{
		{
			lockSelection = false;
			go = Selection.activeGameObject;

			if(go==null)
            {
				return;
            }

			animator = go.GetComponent<Animator>();

			if(animator==null)
            {
				Debug.Log("!!  没有Animaotr 无法预览");
				return;
            }

			if (animtorCache.ContainsKey(go.gameObject.GetInstanceID()))
            {
				animatorData = animtorCache[go.gameObject.GetInstanceID()];
			}
			else
            {
				
				animatorData = new AnimatorData();
				animatorData.isPreview = false;
				animatorData.animator = go.GetComponent<Animator>();
				animtorCache.Add(go.GetInstanceID(), animatorData);
			}
			
			if (animatorData!=null) 
			{
				ChuShiHua();
				animator = animatorData.animator;
				GetAllStateName();
				LieBiaoShuaXin();

				aniState = stateNames[aniLieBiaoID];
				AnimaterHongPei(aniState);

			}
			Repaint();
		}
	}

	private void GetAllStateName()
	{
		if (animator != null) 
		{
			var runAnimator = animator.runtimeAnimatorController;
			aniConS = runAnimator as AnimatorController;

			foreach (var layer in aniConS.layers)
			{
				GetAnimState(layer.stateMachine);
			}

		}
	}

	private void GetAnimState(AnimatorStateMachine aSM)
	{
		foreach (var s in aSM.states)
		{
			if (s.state.motion == null)
				continue;
			var clip = GetClip(s.state.motion.name);
			if (clip != null) 
			{
				stateNames.Add(s.state);
			}
		}

		foreach (var sms in aSM.stateMachines)
		{
			GetAnimState(sms.stateMachine);
		}
	}

	private AnimationClip GetClip(string name)
	{
		foreach (var clip in aniConS.animationClips)
		{
			if (clip.name.Equals(name))
				return clip;
		}

		return null;
	}

	public void OnGUI()
	{

		if (go == null || animator == null || animatorData==null)
		{
			EditorGUILayout.HelpBox("请选择一个带Animator的物体！", MessageType.Info);
			return;
		}


		var oldAniID = aniLieBiaoID;
		aniLieBiaoID = EditorGUILayout.Popup("动画列表", aniLieBiaoID, aniLieBiaoString);
		animatorData.id = aniLieBiaoID;
		aniState = stateNames[aniLieBiaoID];

		if (oldAniID != aniLieBiaoID) 
		{
			hongPei = false;
			AnimaterHongPei(aniState);
		}

		EditorGUILayout.BeginHorizontal();

		EditorGUI.BeginChangeCheck();
		if (!lockSelection) {
			aniZhuangTai = "开启动画";
		} else {
			aniZhuangTai = "关闭动画";
		}
		GUILayout.Toggle(AnimationMode.InAnimationMode(), aniZhuangTai, EditorStyles.toolbarButton);
		if (EditorGUI.EndChangeCheck())
		{
			lockSelection = !lockSelection;
			animatorData.isPreview = !lockSelection;
			if (lockSelection) 
			{
				GuanBi();
			}
			AnimaterHongPei(aniState);
		}
		GUILayout.FlexibleSpace();

		if (GUILayout.Button("Play"))
		{
			playtime = true;
		}
		if (GUILayout.Button("Stop"))
		{
			playtime = false;
		}
		if (GUILayout.Button("CopyName"))
		{
			Debug.Log(aniState.motion.name);

			TextEditor text = new TextEditor();
			text.text = aniState.motion.name;
			text.OnFocus();
			text.Copy();
		}
		EditorGUILayout.EndHorizontal();

		if (aniState != null)
		{
			if (!playtime) 
			{
				float startTime = 0.0f;
				float stopTime  = RecordEndTime;
				clipTime = EditorGUILayout.Slider(clipTime, startTime, stopTime);
				var zhen = clipTime * 30;
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("帧数：" + (int)zhen);
				EditorGUILayout.LabelField("进度：" + clipTime / stopTime);
				EditorGUILayout.EndHorizontal();
			}
		}
		Repaint();
	}

	private void LieBiaoShuaXin()
	{
		aniLieBiaoString = new string[stateNames.Count];
		for (int i = 0; i < stateNames.Count; i++) 
		{
			aniLieBiaoString[i] = stateNames[i].name;
		}
		
	}


	/// <summary>
	/// 预览播放状态下的更新
	/// </summary>
	void update()
	{
		if (Application.isPlaying || !lockSelection)
		{
			return;
		}

		if (go == null)
			return;

		if (aniState == null)
			return;

		if (animator != null && animator.runtimeAnimatorController == null)
			return;

		if (!hongPei) 
		{
			return;
		}

		if (playtime) 
		{
			if (m_RunningTime <= aniTime) 
			{
				animator.playbackTime = m_RunningTime;
				animator.Update(0);
			}
			if (m_RunningTime >= aniTime) 
			{
				m_RunningTime = 0.0f;
			}
		} else {
			m_RunningTime = clipTime;
			animator.playbackTime = m_RunningTime;
			animator.Update(0);
		}
	}

	void GuanBi()
	{
		if (animator != null) 
		{
			m_RunningTime = 0;
			animator.playbackTime = m_RunningTime;
			animator.Update(0);
		}
	}

	void AnimaterHongPei(AnimatorState state)
	{
		if (Application.isPlaying || state == null || !lockSelection)
		{
			return;
		}

		aniTime = 0.0f;

		aniTime = GetClip(state.motion.name).length;
		
		float frameRate = 30f;
		int frameCount = (int)((aniTime * frameRate) + 2);
		animator.StopPlayback();
		animator.Play(state.name);
		animator.recorderStartTime = 0;
		
		animator.StartRecording(frameCount);
		
		for (var j = 0; j < frameCount - 1; j++)
		{
			animator.Update(1.0f / frameRate);
		}
		
		animator.StopRecording(); 
		RecordEndTime = animator.recorderStopTime;
		animator.StartPlayback();
		hongPei = true;
	}

	void OnEnable()
	{
		m_PreviousTime = EditorApplication.timeSinceStartup;
		EditorApplication.update += inspectorUpdate;
		isInspectorUpdate = true;
	}

    private void OnDisable()
    {
		animtorCache.Clear();
	}

    void OnDestroy()
	{
		EditorApplication.update -= inspectorUpdate;
		isInspectorUpdate = false;
		GuanBi();
		animtorCache.Clear();
	}

	private void inspectorUpdate()
	{
		delta = EditorApplication.timeSinceStartup - m_PreviousTime;
		m_PreviousTime = EditorApplication.timeSinceStartup;

		if (!Application.isPlaying)
		{
			m_RunningTime = m_RunningTime + (float)delta;
			update();
		}
	}
}
