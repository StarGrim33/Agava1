using UnityEngine;

public class SpiderBall : PlayerBall
{
    public void ChangeBuyStatus()
    {
        if (_isBuyed == false)
            _isBuyed = true;

        return;
    }
}
