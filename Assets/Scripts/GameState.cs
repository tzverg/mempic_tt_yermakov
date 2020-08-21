using UnityEngine;

public class GameState : State
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("HOLD");
        }
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("FREE");
        }
    }
}
