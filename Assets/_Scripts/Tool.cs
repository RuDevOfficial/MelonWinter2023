using System;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public ToolsManager.ToolType type;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryDoPressAction();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            TryDoReleaseAction();
        }
    }

    #region PressAction

    protected void TryDoPressAction()
    {
        if (CanDoPressAction())
        {
            DoPressAction();
        }
    }

    protected virtual void DoPressAction()
    {
    }

    protected virtual bool CanDoPressAction()
    {
        return true;
    }
#endregion
    #region ReleaseAction

    private void TryDoReleaseAction()
    {
        if (CanDoReleaseAction())
        {
            DoReleaseAction();
        }
    }

    protected virtual bool CanDoReleaseAction()
    {
        return true;
    }

    protected virtual void DoReleaseAction()
    {
    }
    #endregion
}
