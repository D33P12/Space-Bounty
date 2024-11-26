public abstract class MovementBaseControl : IMovement
{
  public abstract float YawAmount { get; }
  public abstract float PitchAmount { get;}
  public abstract float RollAmount { get;  }
  public abstract float ThrustAmount { get; }
}
