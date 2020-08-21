using UnityEngine;
using System.Collections;

public class GameOverState : State
{
    [SerializeField] private GameObject textLable;
    public GameObject wrongMesh;
    private Material wrongMeshMat;

    void OnEnable()
    {
        textLable.SetActive(true);
        wrongMeshMat = wrongMesh.GetComponent<Renderer>().material;
        wrongMeshMat.color = Color.red;

        if (wrongMesh != null)
        {
            wrongMesh.GetComponent<Renderer>().material = wrongMeshMat;
            Destroy(wrongMesh, 5F);
        }
    }

    void OnDisable()
    {
        textLable.active = false;
    }
}