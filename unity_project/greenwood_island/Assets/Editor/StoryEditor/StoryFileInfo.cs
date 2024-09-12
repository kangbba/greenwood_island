using UnityEditor;
using UnityEngine;
using System.IO;

public class StoryFileInfo
{
    public string FileName { get; }
    public Texture2D Thumbnail { get; }
    public bool IsSprite { get; }
    public string RelativePath { get; }
    public string FullPath => Path.Combine(Application.dataPath, RelativePath.Substring("Assets".Length)).Replace("\\", "/");

    public StoryFileInfo(string fileName, string relativePath)
    {
        FileName = fileName;
        RelativePath = EnsureAssetsPath(relativePath);

        // AssetDatabase를 통해 썸네일을 가져옴
        Thumbnail = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath<Texture2D>(RelativePath));

        // TextureImporter를 사용해 파일이 Sprite 타입인지 확인
        var textureImporter = AssetImporter.GetAtPath(RelativePath) as TextureImporter;
        IsSprite = textureImporter != null && textureImporter.textureType == TextureImporterType.Sprite;
    }

    // relativePath가 항상 "Assets"로 시작하도록 보장하는 메서드
    private string EnsureAssetsPath(string path)
    {
        if (!path.StartsWith("Assets"))
        {
            path = "Assets" + path.Substring(Application.dataPath.Length).Replace("\\", "/");
        }
        return path;
    }
}
