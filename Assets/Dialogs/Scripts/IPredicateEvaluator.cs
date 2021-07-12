using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPredicateEvaluator
{
    // Start is called before the first frame update
    bool? Evaluate(string predicate, string[] parameters);
}
