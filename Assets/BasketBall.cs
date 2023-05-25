using UnityEngine;

public class BasketBall : PlayerBall
{
    public void ChangeBuyStatus()
    {
        if(_isBuyed == false)
            _isBuyed = true;

        return;
    }
}
