﻿using UnityEngine;
using System.Collections;

public interface IDamageable {
    void TakeDamage(int damage, GameObject instigator);
}
