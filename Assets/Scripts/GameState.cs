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
            towerMeshes = new List<GameObject>();
            towerMeshes.Add(startGO);
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
        if (Input.GetMouseButtonDown(0))
        {
            CreateMesh();
        }
        if (Input.GetMouseButton(0))
        {
            ScaleMesh();
        }
    }

    public void DefeatCameraFocus()
    {
        camC?.DefeatCameraFocus(startGO.transform);
    }

    private void CreateMesh()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = towerMeshes[towerMeshes.Count - 1].transform.position + config.MeshPosOffset * 2;
        cylinder.transform.localScale = config.MeshStartScale;
        cylinder.GetComponent<Renderer>().material.color = config.DefaultMeshColor;
        cylinder.name = cylinder.name + "_" + towerMeshes.Count;
        towerMeshes.Add(cylinder);
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
