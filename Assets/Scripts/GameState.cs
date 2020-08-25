using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState { PREPARE, ANIMATION_INC, ANIMATION_DEC, ANIM_RESTORE, DISABLED }

public class GameState : State
{
    [SerializeField] private ConfigSO config;

    [SerializeField] private CamController camC;
    [SerializeField] private GameObject startGO;
    [SerializeField] private GameObject meshPrefab;

    private Vector3 scaleChange;
    private GameObject currentMeshGO;
    private GameObject prevMeshGO;

    private List<GameObject> towerMeshes;

    public bool towerAnimating;

    private void OnEnable()
    {
        if (startGO != null)
        {
            towerAnimating = false;
            config.towerPerfectMove = new List<bool>();
            towerMeshes = new List<GameObject>();

            currentMeshGO = startGO;
            camC?.StartFocusCamera(currentMeshGO.transform);

            scaleChange = new Vector3(config.ScaleSpeed, 0F, config.ScaleSpeed);
        }
    }

    IEnumerator TowerWave()
    {
        //Debug.LogWarning("TowerWave roll");
        towerAnimating = true;

        if (towerAnimating)
        {
            for (int cnt = towerMeshes.Count - 2; cnt >= 0; cnt--) //TODO
            {
                MeshAnim currentMeshA = towerMeshes[cnt].GetComponent<MeshAnim>();
                if (currentMeshA != null)
                {
                    //Debug.Log(towerMeshes[cnt].name + " prepareToAnim");
                    currentMeshA.prepareToAnim = true;
                    if (cnt == 0)
                    {
                        towerAnimating = false;
                    }
                    else
                    {
                        yield return new WaitForSeconds(config.WaveTimer);
                    }
                }
            }
        }
    }

    public void ClearMeshList()
    {
        foreach (GameObject target in towerMeshes)
        {
            //Destroy(target);
            target.GetComponent<PoolObject>().ReturnToPool();
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
        if (prevMeshGO != null)
        {
            return prevMeshGO.transform.localScale;
        }
        return Vector3.zero;
    }

    void Update()
    {
        if (isActiveAndEnabled && !towerAnimating)
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
                if (GetMeshCount() > 0)
                {
                    ScaleMesh();
                }
            }
        }
    }

    public void GameOver()
    {
        camC?.DefeatCameraFocus(prevMeshGO.transform, startGO.transform);
    }

    private void CheckPerfectMove()
    {
        if (prevMeshGO != null)
        {
            Vector3 currentMeshLS = currentMeshGO.transform.localScale;
            Vector3 previosMeshLS = prevMeshGO.transform.localScale;
            bool result = currentMeshLS.x >= (previosMeshLS.x - config.PerfectMoveFaultValue);
            SetCurrentMeshPM(result);
        }
    }

    private void SetCurrentMeshPM(bool perfectMove)
    {
        config.towerPerfectMove.Add(perfectMove);

        MeshAnim currentMeshAnim = currentMeshGO.GetComponent<MeshAnim>();
        if (currentMeshAnim != null)
        {
            currentMeshAnim.perfectMove = perfectMove;
        }
    }

    public void AnimateTower()
    {
        MeshAnim currentMeshAnim = currentMeshGO.GetComponent<MeshAnim>();
        if (currentMeshAnim != null)
        {
            if (currentMeshAnim.perfectMove)
            {
                StartCoroutine("TowerWave");
            }
        }
    }

    private void CreateMesh()
    {
        //GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //GameObject cylinder = Instantiate(meshPrefab);
        Vector3 meshPosOffset = config.MeshPosOffset * (towerMeshes.Count + 1) * 2;
        Vector3 newHexPos = startGO.transform.position + meshPosOffset;
        GameObject cylinder = PoolManager.GetObject("MeshPrefab", newHexPos, config.MeshStartScale);
        //cylinder.transform.position = startGO.transform.position + meshPosOffset;
        //cylinder.transform.localScale = config.MeshStartScale;
        cylinder.GetComponent<Renderer>().material.color = config.DefaultMeshColor;
        //cylinder.name = cylinder.name + "_" + towerMeshes.Count;
        SetCurrentMesh(cylinder);

        camC?.FocusCamera(currentMeshGO.transform);
    }

    private void SetCurrentMesh(GameObject target)
    {
        prevMeshGO = currentMeshGO;
        currentMeshGO = target;

        towerMeshes.Add(currentMeshGO);

        MeshAnim prevMeshAnim = prevMeshGO.GetComponent<MeshAnim>();
        MeshAnim currentMeshAnim = currentMeshGO.GetComponent<MeshAnim>();
        if (prevMeshAnim != null)
        {
            prevMeshAnim.currentMesh = false;
        }
        if (currentMeshAnim != null)
        {
            currentMeshAnim.currentMesh = true;
        }
    }

    public GameObject GetStartGO()
    {
        return startGO;
    }

    private void ScaleMesh()
    {
        currentMeshGO.transform.localScale += scaleChange;
    }

    void OnDisable()
    {
        towerAnimating = false;
        StopCoroutine("TowerWave");
    }
}
