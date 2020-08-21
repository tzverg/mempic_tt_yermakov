using UnityEngine;

public class StateMachine : MonoBehaviour
{
    /// Задается в Inspector'е.
    [SerializeField]
    private State startingState;

    private State currentState;

    public State CurrentState
    {
        get { return currentState; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    /// Переводит стейт машину в начальное состояние.
    public void Reset()
    {
        Transit(startingState);
    }

    /// На каждом кадре проверяет, не нужно ли совершить
    /// переход. Если нужно - совершает. (TODO)
    void Update()
    {
        if (currentState == null)
            return;

        var next = currentState.GetNext();
        if (next != null)
            Transit(next);
    }

    /// Собственно, переход.
    void Transit(State next)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = next;
        if (currentState != null)
            currentState.Enter();
    }
}