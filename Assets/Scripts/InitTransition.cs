﻿using UnityEngine;

public class InitTransition : Transition
{
    [SerializeField] private Transform spereTR;
    [SerializeField] private Transform cillTR;

    [SerializeField] private Vector3 defaultSpherePos;
    [SerializeField] private Vector3 defaultCillPos;

    /// Событие "включения".
    void OnEnable()
    {
        NeedTransit = CheckCondition() ? true : false;
    }

    // Проверка условия смены состояния
    private bool CheckCondition()
    {
        if (spereTR.position.Equals(defaultSpherePos) &&
            cillTR.position.Equals(defaultCillPos))
            return true;
        else
            return false;
    }
}