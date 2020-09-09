public interface IRPGAction
{
    void ActionStart();

    void ActionEnd();

    void OnTurnStart();

    void OnTurnEnd();
}

public interface IManager
{
    void Init();

    void Exit();
}

