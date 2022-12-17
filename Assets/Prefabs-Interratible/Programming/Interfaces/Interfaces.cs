using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDrawInterface
{
    public event Action<string> OnDrawText;
    public event Action<bool> OnDrawCanvas;
}