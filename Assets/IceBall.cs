using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : PlayerBall
{
    public void ChangeBuyStatus()
    {
        if (_isBuyed == false)
            _isBuyed = true;

        return;
    }
}
