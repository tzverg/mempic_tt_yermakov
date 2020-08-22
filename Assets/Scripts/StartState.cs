using UnityEngine;

public class StartState : State
{
    [SerializeField] private Transform spereTR;
    [SerializeField] private Transform cillTR;

    void OnEnable()
    {
        spereTR.position = new Vector3(0, -3.175F, 0);
        cillTR.position = new Vector3(0, 0, 0);
    }
}
