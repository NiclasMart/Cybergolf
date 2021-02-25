public enum GameState 
{
    MENU,
    GENERATE_PHASE,
    TUTORIAL,
    IDLE_PHASE,
    HIT_PHASE,
    ROLL_PHASE,
    SPEC_PHASE,
    POINT_VIEW,
    PAUSE_MENU
    
}

public enum BallAbility
{
    STOP,
    JUMP,
    BREAK,
    REDIRECT,
    NONE
}

public enum BallBounciness
{
    SOFT,
    NORMAL,
    HARD
}

public enum WorldGravity
{
    LOW,
    NORMAL,
    HIGH
}

public enum Difficulty
{
    EASYMODE,
    NORMAL,
    HARDMODE
}

public enum WheelState
{
    PRE_SPINNING,
    SPINNING,
    STOPED
}

public enum InformType
{
    ROLL_END,
    TEST_CAM,
    TRIGGER_ABILITY,
    REACH_GOAL
}