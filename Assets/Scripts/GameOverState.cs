using UnityEngine;

public class GameOverState : State
{
    [SerializeField] private GameObject textLable;
    [SerializeField, Tooltip("Time for Destroy WrongMesh in sec.")]
    private float timeForDestroy;
    public GameObject wrongMesh;
    private Material wrongMeshMat;

    void OnEnable()
    {
        textLable.SetActive(true);

        if (wrongMesh != null)
        {
            wrongMesh.GetComponent<Renderer>().material.color = Color.red;
            Destroy(wrongMesh, timeForDestroy);
        }
    }

    void OnDisable()
    {
        textLable?.SetActive(false);
    }
}