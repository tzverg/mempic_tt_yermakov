using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private ConfigSO config;

    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraNextPos;
    private Quaternion cameraNextRot;

    private void Start()
    {
        config.camDefaultRot = Camera.main.transform.rotation;
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
        cameraNextRot = config.camDefaultRot;
    }

    public void DefeatCameraFocus(Transform targetPos, Transform targetLookAt)
    {
        cameraNextPos = targetPos.position + config.DefeatCameraOffset;
        CalculateNewCameraRot(targetLookAt);
    }

    private void CalculateNewCameraPos(Transform target)
    {
        cameraNextPos = target.position + config.CameraOffset;
    }

    private void CalculateNewCameraRot(Transform targetTR)
    {
        Vector3 relativePos = (targetTR.position - transform.position);
        cameraNextRot = Quaternion.LookRotation(relativePos);
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraNextPos, ref velocity, config.CamSmoothPosTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, cameraNextRot, config.CamSmoothRotTime * Time.deltaTime);
    }
}
