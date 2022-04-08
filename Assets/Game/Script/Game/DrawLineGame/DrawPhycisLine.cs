using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPhycisLine : MonoBehaviour
{
    [EnumName("线条绘制")]
    public LineRenderer line;
    [EnumName("刚体")]
    public Rigidbody rigidbody;

    private List<Vector3> drawPoints = new List<Vector3>();
    private Camera camera;
    private List<Collider> colliders = new List<Collider>();
    private List<Collider> colliderCache = new List<Collider>();

    [EnumName("分布密度")]
    public float ColliderLen = 0.15f;

    [EnumName("单个碰撞体大小")]
    public float ColliderRadius = 0.1f;

    [EnumName("单个碰撞体Z轴长度")]
    public float ColliderZLen = 0.3f;

    [EnumName("距离修正最大数量")]
    public int ColliderFixNum = 10;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    public void Clear()
    {
        ClearAllCollider();
        rigidbody.isKinematic = true;
        drawPoints.Clear();
        line.positionCount = drawPoints.Count;
    }

    // Update is called once per frame
    public void Logic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clear();
            drawPoints.Add(Vector3.zero);
            line.transform.position = FrameworkTools.ConvertMouseToWorld(line.transform, camera);

        }
        if (Input.GetMouseButton(0))
        {
            var pos = line.transform.InverseTransformPoint(FrameworkTools.ConvertMouseToWorld(line.transform, camera));
            drawPoints.Add(Vector3.Lerp(drawPoints[drawPoints.Count - 1], pos, 0.9f));
            line.positionCount = drawPoints.Count;
            line.SetPositions(drawPoints.ToArray());
            AddColliderByFix(pos);
        }
        if(Input.GetMouseButtonUp(0))
        {
            rigidbody.isKinematic = false;
        }
    }

    //增加碰撞体(修正) 如果速度滑动过快根据距离修正
    void AddColliderByFix(Vector3 pos)
    {
        CapsuleCollider last = null;
        CapsuleCollider add = null;
        Vector3 lastPos = Vector3.zero;
        if (colliders.Count > 0)
        {
            last = colliders[colliders.Count - 1] as CapsuleCollider;
            lastPos = last.transform.localPosition;
        }

        Vector3 forward = (pos - lastPos).normalized;
        float distance = Vector3.Distance(lastPos, pos);

        int len = (int)(distance / ColliderLen);
        len = len > ColliderFixNum ? ColliderFixNum : len;
        for (int i=0; i<len; i++)
        {
            var nextPos = lastPos + forward * ((float)(i+1)*ColliderLen);
            AddCollider(nextPos);
        }
    }

    //根据位置添加碰撞
    void AddCollider(Vector3 pos)
    {
        CapsuleCollider last = null;
        CapsuleCollider add = null;
        Vector3 lastPos=Vector3.zero;
        if (colliders.Count>0)
        {
            last = colliders[colliders.Count - 1] as CapsuleCollider;
            lastPos = last.transform.localPosition;
        }

        float a = Vector3.Distance(lastPos, pos);
        if (a < ColliderLen / 2)
        {
            return;
        }
        add = AddColliderObj();

        if (add!=null)
        {
            add.height = ColliderZLen;
            add.radius = ColliderRadius;
            add.direction = 2;
            add.center = Vector3.zero;
            add.transform.right = (lastPos - pos).normalized;
            add.transform.localPosition = pos;

            colliders.Add(add);
        }
    }


    //添加碰撞体
    CapsuleCollider AddColliderObj()
    {
        if(colliderCache.Count>1)
        {
            var ret = colliderCache[0] as CapsuleCollider;
            colliderCache.RemoveAt(0);
            ret.gameObject.SetActive(true);
            return ret;
        }
        GameObject tmp = new GameObject();
        tmp.transform.parent = line.transform;
        return tmp.AddComponent<CapsuleCollider>();
    }

    //清除所有碰撞体
    void ClearAllCollider()
    {
        foreach (var item in colliders)
        {
            item.gameObject.SetActive(false);
            colliderCache.Add(item);
        }
        colliders.Clear();
    }
}
