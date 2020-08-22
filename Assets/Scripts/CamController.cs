using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private float smoothPosTime;
    [SerializeField] private float smoothRotTime;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 defeatCameraOffset;
    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraNextPos;
    private Quaternion cameraNextRot;
    private Quaternion cameraDefaultRot;

    private void Start()
    {
        cameraDefaultRot = Camera.main.transform.rotation;
    }

    public Vector3 CameraNextPos
    {
        set { cameraNextPos = value; }
    }

    public void FocusCamera(Transform target)
    {
        CalculateNewCameraPos(target);
        CalculateNewCameraRot(target);
    }

    public void StartFocusCamera(Transform target)
    {
        CalculateNewCameraPos(target);
        cameraNextRot = cameraDefaultRot;
    }

    public void DefeatCameraFocus(Transform target)
    {
        cameraNextPos = target.position + defeatCameraOffset;
        CalculateNewCameraRot(target);
    }

    private void CalculateNewCameraPos(Transform target)
    {
        cameraNextPos = target.position + cameraOffset;
    }

    private void CalculateNewCameraRot(Transform targetTR)
    {
        Vector3 relativePos = (targetTR.position - transform.position);
        cameraNextRot = Quaternion.LookRotation(relativePos);
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraNextPos, ref velocity, smoothPosTime);        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraNextRot, smoothRotTime * Time.deltaTime);
    }
}
