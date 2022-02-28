using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class FloatValue : ScriptableObject,ISerializationCallbackReceiver   //cannot be attached to any scene
{
  public float initialValue;
  [HideInInspector]
  public float RuntimeValue;
   public void OnAfterDeserialize()
  {
    RuntimeValue = initialValue;
  }
  public void OnBeforeSerialize()
  {

  }
}
