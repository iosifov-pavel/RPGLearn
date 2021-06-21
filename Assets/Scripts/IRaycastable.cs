using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

public interface IRaycastable {
    bool HandleRaycast(PlayerController controler);
    Cursors GetCursorType();
}
