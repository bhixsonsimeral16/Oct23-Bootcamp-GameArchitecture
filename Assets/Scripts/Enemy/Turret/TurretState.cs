public class TurretState
{
    protected TurretController turret;

    public TurretState(TurretController turret)
    {
        this.turret = turret;
    }

    public virtual void OnStateEnter() { }
    public virtual void OnStateUpdate() { }
    public virtual void OnStateExit() { }
}
