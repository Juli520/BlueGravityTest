public class GameManager : MonoBehaviourSingleton<GameManager>
{
    private int _currentMoney = 10;
    public int CurrentMoney => _currentMoney;
    
    public void AddMoney(int amount = 10)
    {
        _currentMoney += amount;
        EventManager.Instance.Trigger(NameEvent.OnMoneyUpdated);
    }
    
    public void RemoveMoney(int amount)
    {
        _currentMoney -= amount;
        EventManager.Instance.Trigger(NameEvent.OnMoneyUpdated);
    }
}
