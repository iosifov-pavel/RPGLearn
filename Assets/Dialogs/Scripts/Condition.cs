using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [SerializeField] Disjunction[] and;
    public bool Check(IEnumerable<IPredicateEvaluator> evaluators){
        foreach(Disjunction or in and){
            if(!or.Check(evaluators)) return false;
        }
        return true;
    }

    [System.Serializable]
    public class Disjunction{
        [SerializeField] Predicate[] or;
        public bool Check(IEnumerable<IPredicateEvaluator> evaluators){
            foreach(Predicate pred in or){
                if(pred.Check(evaluators)) return true;
            }
            return false;
        }
    }

    [System.Serializable]
    public class Predicate{
    [SerializeField]string predicate;
    [SerializeField]string[] parameters;

    public bool Check(IEnumerable<IPredicateEvaluator> evaluators){
        foreach(IPredicateEvaluator evaluator in evaluators){
            bool? result = evaluator.Evaluate(predicate,parameters);
            if(result==null){
                continue;
            }
            if(result==false){
                return false;
            }
        }
        return true;
    }
    }
}
