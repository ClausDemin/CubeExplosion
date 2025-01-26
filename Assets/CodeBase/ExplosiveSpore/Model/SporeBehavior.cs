using Assets.CodeBase.ExplosiveSpore.Interfaces;

public class SporeBehavior : ISporeBehavior
{
    public SporeBehavior(int generation) 
    { 
        Generation = generation;
    }

    public int Generation { get; }
}
