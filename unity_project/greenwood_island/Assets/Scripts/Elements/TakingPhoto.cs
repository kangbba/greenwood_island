using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TakingPhoto : Element
{
    private string _imaginationID;
    public TakingPhoto(string imaginationID)
    {
        _imaginationID = imaginationID;
    }

    public override IEnumerator ExecuteRoutine()
    {
        // PhotoManager 초기화
        PhotoManager.Instance.Init(ImaginationManager.Instance.GetActiveImageByID(_imaginationID));

        Debug.Log("사진 촬영 모드에 들어갑니다.");

        // 사진 촬영이 완료되거나 취소될 때까지 기다리는 루프
        while (!PhotoManager.Instance.IsPhotoTaken() && !PhotoManager.Instance.IsPhotoCancelled())
        {
            // PhotoManager의 Update 메서드를 호출하여 사용자 입력을 처리

            yield return null;  // 매 프레임마다 대기
        }

        if (PhotoManager.Instance.IsPhotoTaken())
        {
            Debug.Log("사진 촬영이 완료되었습니다.");
        }
        else if (PhotoManager.Instance.IsPhotoCancelled())
        {
            Debug.Log("사진 촬영이 취소되었습니다.");
        }
    }
}
