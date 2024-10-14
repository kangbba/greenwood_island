using System;
using System.Collections;
using UnityEngine;

public class If : Element
{
    private readonly Func<bool> _condition;     // 조건 람다식
    private readonly SequentialElement _trueElement;   // 조건이 참일 때 실행할 SequentialElement
    private readonly SequentialElement _falseElement;  // 조건이 거짓일 때 실행할 SequentialElement

    // 생성자: 조건과 참/거짓일 때의 SequentialElement를 받음
    public If(Func<bool> condition, SequentialElement trueElement, SequentialElement falseElement)
    {
        _condition = condition;
        _trueElement = trueElement;
        _falseElement = falseElement;
    }

    // 조건에 따라 적절한 SequentialElement의 코루틴 실행
    public override IEnumerator ExecuteRoutine()
    {
        if (_condition())  // 조건이 참일 경우
        {
            yield return _trueElement.ExecuteRoutine();
        }
        else  // 조건이 거짓일 경우
        {
            yield return _falseElement.ExecuteRoutine();
        }
    }

    // 조건에 따라 SequentialElement를 즉시 실행 (비동기 처리 필요 없을 때)
    public override void ExecuteInstantly()
    {
        if (_condition())  // 조건이 참일 경우
        {
            _trueElement.ExecuteInstantly();
        }
        else  // 조건이 거짓일 경우
        {
            _falseElement.ExecuteInstantly();
        }
    }
}
