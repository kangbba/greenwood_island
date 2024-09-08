using UnityEngine;

public enum ResourceType
{
    FX,
    SFX,
    Place,
    // 다른 리소스 타입을 여기에 추가 가능
}

public static class ResourcePathManager
{
    private const string SHARED_RESOURCE_PATH = "SharedResources"; // 공유 자원의 기본 경로
    private const string STORY_RESOURCE_BASE_PATH = "StoryResources"; // 스토리별 자원의 기본 경로

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

        // 리소스 타입에 따른 폴더 이름 결정
        string typeFolder = GetTypeFolderName(resourceType);

        if (isShared)
        {
            // 공유 자원을 검색할 경우
            resourcePath = $"{SHARED_RESOURCE_PATH}/{typeFolder}/{resourceID}";
        }
        else
        {
            // 스토리 자원을 검색할 경우
            resourcePath = $"{STORY_RESOURCE_BASE_PATH}/{storyName}/{typeFolder}/{resourceID}";
        }

        return resourcePath;
    }

    /// <summary>
    /// 리소스 타입에 따른 폴더 이름을 반환합니다.
    /// </summary>
    /// <param name="resourceType">리소스 타입</param>
    /// <returns>폴더 이름 문자열</returns>
    private static string GetTypeFolderName(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.FX:
                return "FXs";
            case ResourceType.SFX:
                return "SFXs";
            case ResourceType.Place:
                return "Places";
            // 다른 리소스 타입에 대한 폴더 명을 추가할 수 있음
            default:
                Debug.LogWarning($"Unknown resource type: {resourceType}");
                return string.Empty;
        }
    }
}
