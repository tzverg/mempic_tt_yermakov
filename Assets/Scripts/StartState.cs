using UnityEngine;

public class StartState : State
{
    [SerializeField] private ConfigSO config;

    [SerializeField] private Transform spereTR;
    [SerializeField] private Transform cillTR;

    void OnEnable()
    {
        spereTR.GetComponent<Renderer>().material.color = config.DefaultSphereColor;
        cillTR.GetComponent<Renderer>().material.color = config.DefaultMeshColor;

        spereTR.position = new Vector3(0, -3.175F, 0);
        cillTR.position = new Vector3(0, 0, 0);
    }
}
