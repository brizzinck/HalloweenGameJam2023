﻿using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Scene
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}