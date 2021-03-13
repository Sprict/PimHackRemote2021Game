using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBehaviour : Bolt.EntityBehaviour<ICenterState>
{
    public override void Attached()
    {
        state.SetTransforms(state.CenterTransform, transform);
    }
}
