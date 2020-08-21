using UnityEngine;

public class TransitionToGameOverState : Transition
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameOverState gameOverState;
    private Vector3 currentMeshLocalScale;
    private Vector3 prevMeshLocalScale;

    void OnEnable()
    {
        NeedTransit = false;
    }

    void Update()
    {
        if (isActiveAndEnabled)
        {
            if (gameState.GetMeshCount() > 2)
            {
                NeedTransit = CheckCondition();
            }
        }
    }

    private bool CheckCondition()
    {
        currentMeshLocalScale = gameState.GetCurrentMeshLocalScale();
        prevMeshLocalScale = gameState.GetPrevMeshLocalScale();

        if (currentMeshLocalScale != null && prevMeshLocalScale != null)
        {
            if (currentMeshLocalScale.x >= prevMeshLocalScale.x ||
                currentMeshLocalScale.z >= prevMeshLocalScale.z)
            {
                gameOverState.wrongMesh = gameState.CurrentMeshGO;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}