using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameDataManager
{
    private static StorySavedData _currentStorySavedData = null;
    private static int _currentSlotIndex = -1;
    private const string FilePrefix = "storySavedData_slot";  // 파일 이름 접두사
    private const string FileExtension = ".json";           // 파일 확장자
    public const int MaxSlotCount = 5;  // 최대 저장 슬롯 개수

    public static StorySavedData CurrentStorySavedData { get => _currentStorySavedData; }
    public static int CurrentSlotIndex { get => _currentSlotIndex; }

    // 슬롯 번호에 따른 파일 경로를 반환
    public static string GetSaveFilePath(int slotNumber)
    {
        if (!IsValidSlot(slotNumber)) return null;
        return Path.Combine(Application.persistentDataPath, $"{FilePrefix}{slotNumber}{FileExtension}");
    }

    // 슬롯 번호 유효성 검사
    private static bool IsValidSlot(int slotNumber)
    {
        return slotNumber >= 0 && slotNumber < MaxSlotCount;
    }

    // 게임 데이터를 특정 슬롯에 저장하고 성공 여부 반환
    public static bool SaveGameData(StorySavedData storySavedData, int slotNumber)
    {
        // 유효한 슬롯 번호와 게임 데이터가 아닌 경우 실패 처리
        if (!IsValidSlot(slotNumber))
        {
            Debug.LogError($"잘못된 슬롯 번호: {slotNumber}. 유효한 범위는 0에서 {MaxSlotCount - 1}입니다.");
            return false;
        }

        if (storySavedData == null)
        {
            Debug.LogError("저장할 게임 데이터가 null입니다.");
            return false;
        }

        // 파일 경로 생성
        string path = GetSaveFilePath(slotNumber);
        Debug.Log($"슬롯 {slotNumber}에 저장할 파일 경로: {path}");

        // 게임 데이터를 JSON 형식으로 직렬화
        string jsonData = JsonUtility.ToJson(storySavedData, true);
        Debug.Log($"저장할 데이터 (슬롯 {slotNumber}): {jsonData.Substring(0, Mathf.Min(jsonData.Length, 100))}... (100자 미리보기)");

        // 파일 쓰기 시도
        try
        {
            File.WriteAllText(path, jsonData);
            Debug.Log($"슬롯 {slotNumber}에 게임 데이터 저장 성공.");
            
            
            return true;  // 성공적으로 저장된 경우 true 반환
        }
        catch (IOException e)
        {
            Debug.LogError($"슬롯 {slotNumber}에 파일 저장 중 오류 발생: {e.Message}\n경로: {path}");
            return false;  // 실패한 경우 false 반환
        }
    }


    // 특정 슬롯 번호에서 JSON 데이터를 읽어 StorySavedData로 변환
    public static StorySavedData GetStorySavedData(int slotNumber)
    {
        if(slotNumber == -1){
            return null;
        }
        string path = GetSaveFilePath(slotNumber);
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            try
            {
                StorySavedData loadedData = JsonUtility.FromJson<StorySavedData>(jsonData);
                return loadedData;  // 데이터를 반환
            }
            catch (System.Exception e)
            {
                Debug.LogError($"데이터 파싱 중 오류 발생: {e.Message}");
                return null;  // 파싱 오류 시 null 반환
            }
        }
        else
        {
            return null;  // 파일이 존재하지 않으면 null 반환
        }
    }

    // 특정 슬롯의 파일을 삭제하는 함수
    public static bool DeleteSaveDataFile(int slotNumber)
    {
        string filePath = GetSaveFilePath(slotNumber);

        if (filePath != null && File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Debug.Log($"슬롯 {slotNumber}의 파일 삭제 완료.");
                return true;  // 성공적으로 삭제되면 true 반환
            }
            catch (IOException e)
            {
                Debug.LogError($"파일 삭제 중 오류 발생: {e.Message}");
                return false;  // 삭제 실패 시 false 반환
            }
        }
        else
        {
            Debug.LogWarning($"슬롯 {slotNumber}에 파일이 존재하지 않거나 경로가 유효하지 않습니다.");
            return false;  // 파일이 존재하지 않으면 false 반환
        }
    }

    // 저장된 모든 게임 파일 슬롯 번호를 반환
    public static List<int> GetSavedGameSlots()
    {
        List<int> savedSlots = new List<int>();
        for (int i = 0; i < MaxSlotCount; i++)
        {
            if (File.Exists(GetSaveFilePath(i)))
            {
                savedSlots.Add(i);  // 저장된 파일이 있는 슬롯 번호를 추가
            }
        }
        return savedSlots;
    }

    // 특정 슬롯이 사용 중인지 확인
    public static bool IsSlotUsed(int slotNumber)
    {
        return File.Exists(GetSaveFilePath(slotNumber));
    }

    public static void LoadGameDataThenPlay(int slotIndex)
    {
        StorySavedData storySavedDataInSlot = GetStorySavedData(slotIndex);
        
        if(storySavedDataInSlot != null){
            _currentStorySavedData = storySavedDataInSlot;
            _currentSlotIndex = slotIndex;
            Debug.Log($"{_currentStorySavedData.StoryID} 스토리가 로드됩니다");
        }
        else{
            _currentStorySavedData = null;
            _currentSlotIndex = 0;
            Debug.Log("새로 시작하기 입니다");
        }

        // // 씬 로드 완료 시 실행할 이벤트 등록LoadGameData
        // SceneManager.sceneLoaded += OnSceneLoaded;
        // 게임 플레이 씬으로 전환
        SceneManager.LoadScene("InGame");
    }


}
