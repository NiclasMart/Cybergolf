using UnityEngine;

public struct Properties
{
    public BallAbility ballAbility;
    public BallBounciness ballBounciness;
    public WorldGravity gravity;

    public Properties(BallAbility ability, BallBounciness mat, WorldGravity grav)
    {
        ballAbility = ability;
        ballBounciness = mat;
        gravity = grav;
    }
    
}
