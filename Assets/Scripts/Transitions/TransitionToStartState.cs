﻿using UnityEngine;

public class TransitionToStartState : Transition
{
    [SerializeField] private GameState gameState;

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
                NeedTransit = true;
            }
        }
    }
}