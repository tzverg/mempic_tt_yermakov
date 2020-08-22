using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private ConfigSO config;

    [SerializeField] private CamController camC;
    [SerializeField] private GameObject startGO;

    private Vector3 scaleChange;
    private GameObject currentMeshGO;
    private GameObject prevMeshGO;

    private List<GameObject> towerMeshes;

    private void OnEnable()
    {
        if (startGO != null)
        {
            config.towerPerfectMove = new List<bool>();
            towerMeshes = new List<GameObject>();

            AddCurrentMeshToGamePool(startGO);
            currentMeshGO = startGO;
            camC?.StartFocusCamera(currentMeshGO.transform);

            scaleChange = new Vector3(config.ScaleSpeed, 0F, config.ScaleSpeed);
        }
    }

    public void ClearMeshList()
    {
        for(int cnt = 1; cnt < towerMeshes.Count; cnt++)
        {
            Destroy(towerMeshes[cnt]);
        }
    }

    public int GetMeshCount()
    {
        return towerMeshes.Count;
    }

    public GameObject CurrentMeshGO
    {
        get { return currentMeshGO; }
    }

    public Vector3 GetCurrentMeshLocalScale()
    {
        return currentMeshGO.transform.localScale;
    }

    public Vector3 GetPrevMeshLocalScale()
    {
        return prevMeshGO.transform.localScale;
    }

    void Update()
    {
        if (isActiveAndEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateMesh();
            }
            if (Input.GetMouseButtonUp(0))
            {
                CheckPerfectMove();
            }
            if (Input.GetMouseButton(0))
            {
                ScaleMesh();
            }
        }
    }

    public void DefeatCameraFocus()
    {
        camC?.DefeatCameraFocus(startGO.transform);
    }

    private void CheckPerfectMove()
    {
        if (prevMeshGO != null)
        {
            Vector3 currentMeshLS = currentMeshGO.transform.localScale;
            Vector3 previosMeshLS = prevMeshGO.transform.localScale;
            bool result = currentMeshLS.x >= (previosMeshLS.x - config.PerfectMoveFaultValue);
            AddCurrentMeshToGamePool(result);
        }
    }

    private void AddCurrentMeshToGamePool(bool perfectMove)
    {
        config.towerPerfectMove.Add(perfectMove);
        towerMeshes.Add(currentMeshGO);
    }

    private void CreateMesh()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Vector3 meshPosOffset = config.MeshPosOffset * towerMeshes.Count * 2;
        cylinder.transform.position = startGO.transform.position + meshPosOffset;
        cylinder.transform.localScale = config.MeshStartScale;
        cylinder.GetComponent<Renderer>().material.color = config.DefaultMeshColor;
        cylinder.name = cylinder.name + "_" + towerMeshes.Count;
        prevMeshGO = currentMeshGO;
        currentMeshGO = cylinder;

        camC?.FocusCamera(currentMeshGO.transform);
    }

    public GameObject GetStartGO()
    {
        return startGO;
    }

    private void ScaleMesh()
    {
        currentMeshGO.transform.localScale += scaleChange;
    }
}
