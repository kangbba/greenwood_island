using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : Element
{
    private float _duration;

    public Delay(float duration) // 페이드 아웃 시간을 인자로 받음
    {
        _duration = duration;
    }

    public override void ExecuteInstantly()
    {
    }

    public override IEnumerator ExecuteRoutine()
    {
        yield return new WaitForSeconds(_duration);
    }
}
