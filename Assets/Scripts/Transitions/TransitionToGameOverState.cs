using UnityEngine;

public class TransitionToGameOverState : Transition
{
    [SerializeField] private ConfigSO config;

    [SerializeField] private GameState gameState;
    [SerializeField] private GameOverState gameOverState;
    private Vector3 currentMeshLS;
    private Vector3 prevMeshLS;

    void OnEnable()
    {
        NeedTransit = false;
    }

    void Update()
    {
        if (isActiveAndEnabled)
        {
            if (Input.GetMouseButton(0))
            {
                if (gameState.GetMeshCount() > 0)
                {
                    NeedTransit = CheckRealtimeCondition();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (gameState.GetMeshCount() > 0)
                {
                    NeedTransit = CheckCondition();
                }
            }
        }
    }

    private bool CheckCondition()
    {
        currentMeshLS = gameState.GetCurrentMeshLocalScale();
        prevMeshLS = gameState.GetPrevMeshLocalScale();

        if (currentMeshLS != null && prevMeshLS != null)
        {
            if (currentMeshLS.x > prevMeshLS.x || currentMeshLS.z > prevMeshLS.z)
            {
                gameOverState.wrongMesh = gameState.CurrentMeshGO;
                gameState.GameOver();
                //Debug.LogError("CheckCondition GAME_OVER");
                return true;
            }
            else
            {
                gameState.AnimateTower();
            }
        }
        return false;
    }

    private bool CheckRealtimeCondition()
    {
        currentMeshLS = gameState.GetCurrentMeshLocalScale();
        prevMeshLS = gameState.GetPrevMeshLocalScale();

        if (currentMeshLS != null && prevMeshLS != null)
        {
            if (currentMeshLS.x > prevMeshLS.x * config.DefeatCondMaxScale.x ||
                currentMeshLS.z > prevMeshLS.z * config.DefeatCondMaxScale.z)
            {
                gameOverState.wrongMesh = gameState.CurrentMeshGO;
                gameState.GameOver();
                //Debug.LogError("CheckRealtimeCondition GAME_OVER");
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