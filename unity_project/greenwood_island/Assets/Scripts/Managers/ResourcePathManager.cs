using System.IO;
using UnityEngine;

public enum ResourceType
{
    FX,
    SFX,
    Place,
    Story,
    Character,
    Imagination,
    StoryData,
}

public static class ResourcePathManager
{
    private const string SHARED_RESOURCE_PATH = "SharedResources"; // 공유 자원의 기본 경로
    private const string STORY_RESOURCE_BASE_PATH = "StoryResources"; // 스토리별 자원의 기본 경로
    private const string ASSETS_PATH = "Assets/Resources"; // Resources 폴더의 루트 경로

    // 스토리 기본 경로를 반환하는 메서드
    public static string GetStoryResourcesBasePath()
    {
        return Path.Combine(ASSETS_PATH, STORY_RESOURCE_BASE_PATH);
    }

    /// <summary>
    /// 리소스 타입과 스토리 ID를 바탕으로 적합한 경로를 반환합니다.
    /// </summary>
    /// <param name="resourceID">리소스 ID (파일명)</param>
    /// <param name="storyID">스토리 ID (폴더 이름)</param>
    /// <param name="resourceType">리소스 타입 (FX, SFX, Place 등)</param>
    /// <param name="isShared">공유 자원을 검색할지 여부</param>
    /// <returns>적합한 경로 문자열</returns>
    /// 

    public static StoryData GetStoryData(string storyID)
    {
        // ResourcePathManager에서 StoryData의 경로를 가져옴
        string resourcePath = GetCurrentStoryResourcePath(storyID, ResourceType.StoryData);

        // 해당 경로에서 StoryData를 로드
        StoryData storyData = Resources.Load<StoryData>(resourcePath);

        // StoryData가 존재하지 않을 경우 경고 메시지 출력
        if (storyData == null)
        {
            Debug.LogWarning($"StoryData not found for storyID: {storyID}");
        }

        return storyData;
    }

    public static string GetSharedResourcePath(string resourceID, ResourceType resourceType){
        return GetResourcePath(resourceID, "", resourceType, true);
    }
    public static string GetCurrentStoryResourcePath(string resourceID, ResourceType resourceType){
        return GetResourcePath(resourceID, StoryManager.Instance.CurrentStoryName, resourceType, false);
    }
    public static string GetStoryResourcePath(string resourceID, string storyID, ResourceType resourceType){
        return GetResourcePath(resourceID, storyID, resourceType, false);
    }
    private static string GetResourcePath(string resourceID, string storyID, ResourceType resourceType, bool isShared = false)
    {
        string resourcePath = string.Empty;

        // 기본 폴더 경로 설정
        string basicFolderPath = isShared ? SHARED_RESOURCE_PATH : $"{STORY_RESOURCE_BASE_PATH}/{storyID}";

        // 리소스 타입별로 특정한 폴더 구조를 반영
        switch (resourceType)
        {
            case ResourceType.FX:
                resourcePath = $"{basicFolderPath}/FXs/{resourceID}/{resourceID}";
                break;

            case ResourceType.SFX:
                resourcePath = $"{basicFolderPath}/SFXs/{resourceID}";
                break;

            case ResourceType.Place:
                resourcePath = $"{basicFolderPath}/Places/{resourceID}";
                break;

            case ResourceType.Story:
                resourcePath = $"{basicFolderPath}/Scripts/{resourceID}";
                break;

            case ResourceType.Character:
                resourcePath = $"{basicFolderPath}/Characters/{resourceID}";
                break;

            case ResourceType.Imagination:
                resourcePath = $"{basicFolderPath}/Imaginations/{resourceID}";
                break;

            case ResourceType.StoryData:
                resourcePath = $"{basicFolderPath}/StoryData/{resourceID}";
                break;
                
            default:
                Debug.LogWarning($"Unknown resource type: {resourceType}");
                break;
        }

        return resourcePath;
    }

     // 스토리 ID 리스트를 반환하는 메서드 (폴더 이름을 기반으로)
    public static string[] GetAvailableStoryIDs()
    {
        string storyPath = Path.Combine(ASSETS_PATH, STORY_RESOURCE_BASE_PATH);
        if (Directory.Exists(storyPath))
        {
            // 폴더 경로에서 폴더 이름만 반환
            string[] directories = Directory.GetDirectories(storyPath);
            for (int i = 0; i < directories.Length; i++)
            {
                directories[i] = Path.GetFileName(directories[i]); // 경로에서 폴더 이름만 추출
            }
            return directories;
        }

        return new string[0];
    }
}
