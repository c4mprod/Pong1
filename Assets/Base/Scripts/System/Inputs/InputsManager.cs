using UnityEngine;
using System.Collections.Generic;

public class InputsManager : IUpdateBehaviour
{
    private InputsBindingDatas m_InputsDatas;

    public InputsManager()
    {
        this.m_InputsDatas = GlobalDatas.Instance.m_InputsBinding;
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
    }
}
