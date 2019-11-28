
public abstract class BaseBehaviour
{
    public abstract int Priority { get; }
    public  PlayerController PlayerController { get; protected set; }
    public PlayerState PlayerState { get; protected set; }

    public void Init(PlayerController playerController, PlayerState playerState)
    {
        PlayerController = playerController;
        PlayerState = playerState;
    }

    public virtual void Start() { }
    public virtual void CleanUp() { }

    public virtual void OnActivate() { }
    public virtual void OnDeactivate() { }
}
