using System.Collections;

[System.Serializable]
public class CustomElement : Element
{
    private System.Action _action;
    private float _duration;

    public CustomElement(System.Action action, float duration)
    {
        _action = action;
        _duration = duration;
    }

    public override IEnumerator Execute()
    {
        _action?.Invoke();
        yield return CoroutineRunner.Instance.WaitForSeconds(_duration);
    }
}
