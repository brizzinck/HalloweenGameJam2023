﻿using System;
using UnityEngine;

namespace CodeBase.Logic.Id
{
  public class UniqueId : MonoBehaviour
  {
    public string Id;

    public void GenerateId() => 
      Id = $"{gameObject.scene.name}_{Guid.NewGuid().ToString()}";
  }
}