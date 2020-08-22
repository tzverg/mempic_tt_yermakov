//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ConfigSO", order = 51)]
public sealed class ConfigSO : ScriptableObject
{
    [SerializeField] private Vector3 meshMinScale;
    [SerializeField] private Vector3 meshMaxScale;
    [SerializeField] private Vector3 meshStartScale;
    [SerializeField] private Vector3 meshPosOffset;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 defeatCameraOffset;

    [SerializeField] private float scaleSpeed;
    [SerializeField] private float camSmoothPosTime;
    [SerializeField] private float camSmoothRotTime;
    [SerializeField] private float perfectMoveFaultValue;

    [SerializeField] private Color defaultSphereColor;
    [SerializeField] private Color defaultMeshColor;
    [SerializeField] private Color wrongMeshColor;

    public Quaternion camDefaultRot;

    public Vector3 MeshMinScale { get { return meshMinScale; } }
    public Vector3 MeshMaxScale { get { return meshMaxScale; } }
    public Vector3 MeshStartScale { get { return meshStartScale; } }
    public Vector3 MeshPosOffset { get { return meshPosOffset; } }
    public Vector3 CameraOffset { get { return cameraOffset; } }
    public Vector3 DefeatCameraOffset { get { return defeatCameraOffset; } }

    public float ScaleSpeed { get { return scaleSpeed; } }
    public float CamSmoothPosTime { get { return camSmoothPosTime; } }
    public float CamSmoothRotTime { get { return camSmoothRotTime; } }
    public float PerfectMoveFaultValue { get { return perfectMoveFaultValue; } }

    public Color DefaultSphereColor { get { return defaultSphereColor; } }
    public Color DefaultMeshColor { get { return defaultMeshColor; } }
    public Color WrongMeshColor { get { return wrongMeshColor; } }

    //public List<bool> towerPerfectMove;
}