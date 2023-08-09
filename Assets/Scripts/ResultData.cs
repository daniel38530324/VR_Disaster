using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Result Data")]
public class ResultData : ScriptableObject
{
    [TextArea(10, 20)]
    public List<string> result;
}
