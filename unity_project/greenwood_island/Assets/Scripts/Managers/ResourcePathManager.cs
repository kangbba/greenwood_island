using System.IO;
using UnityEngine;

public enum ResourceType
{
    FX,
    SFX,
    Place,
    Story,
    CharacterData,
    Imagination,
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
    /// 리소스 타입과 스토리 이름을 바탕으로 적합한 경로를 반환합니다.
    /// </summary>
    /// <param name="resourceID">리소스 ID (파일명)</param>
    /// <param name="storyName">스토리 이름</param>
    /// <param name="resourceType">리소스 타입 (FX, SFX, Place 등)</param>
    /// <param name="isShared">공유 자원을 검색할지 여부</param>
    /// <returns>적합한 경로 문자열</returns>
    public static string GetResourcePath(string resourceID, string storyName, ResourceType resourceType, bool isShared = false)
    {
        string resourcePath = string.Empty;

        // 기본 폴더 경로 설정
        string basicFolderPath = isShared ? SHARED_RESOURCE_PATH : $"{STORY_RESOURCE_BASE_PATH}/{storyName}";

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

            case ResourceType.CharacterData:
                resourcePath = $"{basicFolderPath}/CharacterDatas/{resourceID}";
                break;

            case ResourceType.Imagination:
                resourcePath = $"{basicFolderPath}/Imaginations/{resourceID}";
                break;
                
            default:
                Debug.LogWarning($"Unknown resource type: {resourceType}");
                break;
        }

        return resourcePath;
    }
}
