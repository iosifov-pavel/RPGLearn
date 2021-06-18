using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifierProvider{
    IEnumerable<float> GetAditiveModifier(Stat stat);
    IEnumerable<float> GetPercentModifier(Stat stat);
}
