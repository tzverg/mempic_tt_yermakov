using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private CamController camC;
    [SerializeField] private GameObject startGO;
    [SerializeField] private float scaleSpeed;
    private Vector3 positionOffset = new Vector3(0F, 0.25F, 0F);
    private Vector3 cylinderStartScale = new Vector3(0F, 0.25F, 0F);
    private Vector3 cylinderMaxScale = new Vector3(1F, 0.25F, 1F);
    private Vector3 scaleChange;

    public List<GameObject> towerGOL;
    private GameObject currentMeshGO;
    private GameObject prevMeshGO;

    private void OnEnable()
    {
        if (startGO != null)
        {
            towerGOL = new List<GameObject>();
            towerGOL.Add(startGO);
            currentMeshGO = startGO;
            camC?.StartFocusCamera(currentMeshGO.transform);

            scaleChange = new Vector3(scaleSpeed, 0F, scaleSpeed);
        }
    }

    public void ClearMeshList()
    {
        for(int cnt = 1; cnt < towerGOL.Count; cnt++)
        {
            Destroy(towerGOL[cnt]);
        }
        //towerGOL.Clear();
    }

    public int GetMeshCount()
    {
        return towerGOL.Count;
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
        cylinder.transform.position = towerGOL[towerGOL.Count - 1].transform.position + positionOffset * 2;
        cylinder.transform.localScale = cylinderStartScale;
        cylinder.name = cylinder.name + "_" + towerGOL.Count;
        towerGOL.Add(cylinder);
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
