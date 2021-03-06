﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct WaveComponentData : IComponentData
{
    public float amplitude;

    public float xOffset;

    public float yOffset;
}