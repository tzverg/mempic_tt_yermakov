using UnityEngine;

public class CamController : MonoBehaviour
{
    //public static bool defeat = false;

    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 cameraOffset;
    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraNextPos;

    public Vector3 CameraNextPos
    {
        set { cameraNextPos = value; }
    }

    public Transform Target
    {
        get { return target; }
        set { target = value; CalculateNewCameraPos(target.position); }
    }

    private void Start()
    {
        if (target != null)
        {
            CalculateNewCameraPos(target.position);
        }
    }

    public void CalculateNewCameraPos(Vector3 targetPos)
    {
        cameraNextPos = targetPos + cameraOffset;
    }



    void LateUpdate()
    {
        //if (defeat)
        //{
        //    cameraNextPos = new Vector3(0F, 1F, -5F);
        //    transform.position = Vector3.SmoothDamp(transform.position, cameraNextPos, ref velocity, smoothTime);
        //}
        //else
        //{
            transform.position = Vector3.SmoothDamp(transform.position, cameraNextPos, ref velocity, smoothTime);
        //}
    }
}
