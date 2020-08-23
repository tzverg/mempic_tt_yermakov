using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ConfigSO", order = 51)]
public sealed class ConfigSO : ScriptableObject
{
    [SerializeField] private Vector3 sphereStartPos;
    [SerializeField] private Vector3 meshMinScale;
    [SerializeField] private Vector3 meshMaxScale;
    [SerializeField] private Vector3 meshStartScale;
    [SerializeField] private Vector3 meshPosOffset;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 defeatCameraOffset;
    [SerializeField] private Vector3 defeatCondMaxScale;
    [SerializeField] private Vector3 waveFirstStepInc;
    [SerializeField] private Vector3 waveFirstStepDec;
    [SerializeField] private Vector3 waveNextStepInc;
    [SerializeField] private Vector3 waveNextStepDecM;

    [SerializeField] private float scaleSpeed;
    [SerializeField] private float waveSmoothScaleTime;
    [SerializeField] private float camSmoothPosTime;
    [SerializeField] private float camSmoothRotTime;
    [SerializeField] private float perfectMoveFaultValue;
    [SerializeField, Tooltip("Time for waiting between twening meshes in sec.")]
    private float waveTimer;
    [SerializeField, Tooltip("Time for Destroy WrongMesh in sec.")]
    private float destroyTimer;

    [SerializeField] private Color defaultSphereColor;
    [SerializeField] private Color defaultMeshColor;
    [SerializeField] private Color wrongMeshColor;

    public Quaternion camDefaultRot;

    public Vector3 SphereStartPos { get { return sphereStartPos; } }
    public Vector3 MeshMinScale { get { return meshMinScale; } }
    public Vector3 MeshMaxScale { get { return meshMaxScale; } }
    public Vector3 MeshStartScale { get { return meshStartScale; } }
    public Vector3 MeshPosOffset { get { return meshPosOffset; } }
    public Vector3 CameraOffset { get { return cameraOffset; } }
    public Vector3 DefeatCameraOffset { get { return defeatCameraOffset; } }
    public Vector3 DefeatCondMaxScale { get { return defeatCondMaxScale; } }
    public Vector3 WaveFirstStepInc { get { return waveFirstStepInc; } }
    public Vector3 WaveFirstStepDec { get { return waveFirstStepDec; } }
    public Vector3 WaveNextStepInc { get { return waveNextStepInc; } }
    public Vector3 WaveNextStepDecM { get { return waveNextStepDecM; } }

    public float ScaleSpeed { get { return scaleSpeed; } }
    public float WaveSmoothScaleTime { get { return waveSmoothScaleTime; } }
    public float CamSmoothPosTime { get { return camSmoothPosTime; } }
    public float CamSmoothRotTime { get { return camSmoothRotTime; } }
    public float PerfectMoveFaultValue { get { return perfectMoveFaultValue; } }
    public float WaveTimer { get { return waveTimer; } }
    public float DestroyTimer { get { return destroyTimer; } }

    public Color DefaultSphereColor { get { return defaultSphereColor; } }
    public Color DefaultMeshColor { get { return defaultMeshColor; } }
    public Color WrongMeshColor { get { return wrongMeshColor; } }

    public List<bool> towerPerfectMove;
}