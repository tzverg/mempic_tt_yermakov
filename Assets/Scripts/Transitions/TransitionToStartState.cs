using UnityEngine;

public class TransitionToStartState : Transition
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameOverState gameOverState;

    void OnEnable()
    {
        NeedTransit = false;
    }

    void Update()
    {
        if (isActiveAndEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameState.ClearMeshList();
                gameOverState.HideLable();
                NeedTransit = true;
            }
        }
    }
}