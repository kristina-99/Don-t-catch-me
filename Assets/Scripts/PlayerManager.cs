using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private bool hasKnife = false;
    private bool hasFlameThrower = false; 

    public bool HasFlameThrower
    {
        get
        {
            return hasFlameThrower;
        }
        set
        {
            hasFlameThrower = value;
        }
    }

    public bool HasKnife
    {
        get
        {
            return hasKnife;
        }
        set
        {
            hasKnife = value;
        }
    }

}
