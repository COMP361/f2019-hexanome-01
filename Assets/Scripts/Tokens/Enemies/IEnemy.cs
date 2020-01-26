using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    int Will { get; set; }

    int Strength { get; set; }

    int Reward { get; set; }

    Token Token { get; set; }
}