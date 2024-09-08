using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    private const string STORY_FOLDER_PATH = "Stories"; // 스토리가 위치한 Resources 폴더 경로

    private Story _currentStory; // 현재 실행 중인 스토리
    private Story _previousStory; // 이전에 실행된 스토리

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayStory("Story1");
    }

    // Resources 폴더 내에서 스토리 폴더의 이름을 가져오는 메서드
    private IEnumerable<string> GetStoryFolderNames()
    {
        // Resources/Stories 경로를 통해 스토리 폴더들을 탐색
        string resourcesPath = Path.Combine(Application.dataPath, "Resources", STORY_FOLDER_PATH);
        DirectoryInfo directoryInfo = new DirectoryInfo(resourcesPath);

        // Resources/Stories 폴더 내의 하위 디렉토리 이름들을 가져옴
        DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();

        // 각 디렉토리 이름을 반환
        return subDirectories.Select(dir => dir.Name);
    }

    // 스토리를 실행하는 메서드
    public void PlayStory(string storyName)
    {
        // 현재 스토리를 이전 스토리로 설정
        _previousStory = _currentStory;

        // 스토리를 동적으로 로드
        string storyPath = $"{STORY_FOLDER_PATH}/{storyName}/{storyName}"; // 경로 예: Stories/Story1/Story1
        Story loadedStory = Resources.Load<Story>(storyPath);

        if (loadedStory != null)
        {
            _currentStory = loadedStory;
            StartCoroutine(StoryStartRoutine());
        }
        else
        {
            Debug.LogError($"Story '{storyName}' could not be loaded from path '{storyPath}'.");
        }
    }

    private IEnumerator StoryStartRoutine()
    {
        // 모든 클리어 작업 수행
        float clearDuration = 1f;
        yield return new WaitForSeconds(clearDuration);

        // 현재 스토리의 StartRoutine과 UpdateRoutine을 실행
        if (_currentStory != null)
        {
            yield return _currentStory.StartRoutine();
            yield return _currentStory.UpdateRoutine();
            yield return _currentStory.ExitRoutine();
            RestoreAll(clearDuration);
        }
    }

    private void RestoreAll(float duration)
    {
        Debug.Log("StoryManager :: RestoreAll Started");
        new FXsClear(duration).Execute();
        new SFXsClear(duration).Execute();
        new PlaceFilmClear(duration).Execute();
        new CameraMove2DClear(duration).Execute();
        new CameraZoomClear(duration).Execute();
        new AllCharactersClear(duration).Execute();
        new CutInClear(duration).Execute();
        new PlaceOverlayFilmClear(duration).Execute();
        new ScreenOverlayFilmClear(duration).Execute();
        Debug.Log("StoryManager :: RestoreAll Completed");
    }
}
