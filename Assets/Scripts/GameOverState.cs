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
            StartCoroutine("ReloadTheGame");
        }
    }

    IEnumerator ReloadTheGame()
    {
        yield return new WaitForSeconds(config.TimeForDestroy);
        Destroy(wrongMesh);
    }

    void OnDisable()
    {
        textLable?.SetActive(false);
        StopCoroutine("ReloadTheGame");
        Destroy(wrongMesh);
    }
}