using System.Collections;
using UnityEngine;

public class GameOverState : State
{
    [SerializeField] private ConfigSO config;

    [SerializeField] private GameObject textLable;
    public GameObject wrongMesh;
    private Material wrongMeshMat;

    void OnEnable()
    {
        textLable.SetActive(true);

        if (wrongMesh != null)
        {
            wrongMesh.GetComponent<Renderer>().material.color = config.WrongMeshColor;
            //Destroy(wrongMesh, timeForDestroy);
            // crunch (problem with destroying oblect after changing game state) p.s. Destroy delay useless
            StartCoroutine("DestroyWithDelay");
        }
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(config.DestroyTimer);
        wrongMesh?.GetComponent<PoolObject>().ReturnToPool();
        //Destroy(wrongMesh);
    }

    public void HideLable()
    {
        textLable.SetActive(false);
    }

    void OnDisable()
    {
        StopCoroutine("DestroyWithDelay");
        wrongMesh?.GetComponent<PoolObject>().ReturnToPool();
        //Destroy(wrongMesh);
    }
}