public interface IRPGAction
{
    void ActionStart();

    void ActionEnd();

    void OnTurnStart();

    void OnTurnEnd();
}

public interface IManager
{
    /// <summary>
    /// Always invoked first before Run()
    /// </summary>
    void Init();

    /// <summary>
    /// Will invoked after Init()
    /// </summary>
    void Run();

    void Exit();
}

