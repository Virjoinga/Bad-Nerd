using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	private static List<UIRoot> mRoots = new List<UIRoot>();

	public bool automatic = true;

	public int manualHeight = 800;

	private Transform mTrans;

	public static List<UIRoot> list
	{
		get
		{
			return mRoots;
		}
	}

	private void Awake()
	{
		mTrans = base.transform;
		mRoots.Add(this);
	}

	private void OnDestroy()
	{
		mRoots.Remove(this);
	}

	private void Start()
	{
		UIOrthoCamera componentInChildren = GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
	}

	private void Update()
	{
		manualHeight = Mathf.Max(2, (!automatic) ? manualHeight : Screen.height);
		float num = 2f / (float)manualHeight;
		Vector3 localScale = mTrans.localScale;
		if (!(Mathf.Abs(localScale.x - num) <= float.Epsilon) || !(Mathf.Abs(localScale.y - num) <= float.Epsilon) || !(Mathf.Abs(localScale.z - num) <= float.Epsilon))
		{
			mTrans.localScale = new Vector3(num, num, num);
		}
	}

	public static void Broadcast(string funcName)
	{
		int i = 0;
		for (int count = mRoots.Count; i < count; i++)
		{
			UIRoot uIRoot = mRoots[i];
			if (uIRoot != null)
			{
				uIRoot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
			return;
		}
		int i = 0;
		for (int count = mRoots.Count; i < count; i++)
		{
			UIRoot uIRoot = mRoots[i];
			if (uIRoot != null)
			{
				uIRoot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
