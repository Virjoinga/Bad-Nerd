using UnityEngine;

[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : IgnoreTimeScale
{
	public Vector3 target = Vector3.zero;

	public float strength = 10f;

	private UIPanel mPanel;

	private Transform mTrans;

	private float mThreshold;

	private UIDraggablePanel mDrag;

	private void Start()
	{
		mPanel = GetComponent<UIPanel>();
		mDrag = GetComponent<UIDraggablePanel>();
		mTrans = base.transform;
	}

	private void Update()
	{
		float deltaTime = UpdateRealTimeDelta();
		if (mThreshold == 0f)
		{
			mThreshold = (target - mTrans.localPosition).magnitude * 0.005f;
		}
		Vector3 localPosition = mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(mTrans.localPosition, target, strength, deltaTime);
		if (mThreshold >= Vector3.Magnitude(vector - target))
		{
			vector = target;
			base.enabled = false;
		}
		mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector4 clipRange = mPanel.clipRange;
		clipRange.x -= vector2.x;
		clipRange.y -= vector2.y;
		mPanel.clipRange = clipRange;
		if (mDrag != null)
		{
			mDrag.UpdateScrollbars(false);
		}
	}

	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		if (!springPanel.enabled)
		{
			springPanel.mThreshold = 0f;
			springPanel.enabled = true;
		}
		return springPanel;
	}
}
