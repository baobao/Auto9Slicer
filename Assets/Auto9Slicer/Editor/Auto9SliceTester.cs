using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Auto9Slicer
{
    [CreateAssetMenu(menuName = "Auto 9Slice/Tester", fileName = nameof(Auto9SliceTester))]
    public class Auto9SliceTester : ScriptableObject
    {
        public SliceOptions Options => options;
        [SerializeField] private SliceOptions options = new SliceOptions();

        public bool CreateBackup => createBackup;
        [SerializeField] private bool createBackup = true;
        [SerializeField, Header("ファイル名の語尾キーワードでフィルタリング／無記載:全画像対象")] private string _allowKeyword;

        [SerializeField, Header("Slice済画像を対象にするフラグ")] private bool _isAgainSlice;

        public void Run()
        {
            var directoryPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
            if (directoryPath == null) throw new Exception($"directoryPath == null");

            var fullDirectoryPath = Path.Combine(Path.GetDirectoryName(Application.dataPath) ?? "", directoryPath);
            var targets = Directory.GetFiles(fullDirectoryPath)
                .Select(Path.GetFileName)
                .Where(x =>
                {
                    if (string.IsNullOrEmpty(_allowKeyword))
                    {
                        return x.EndsWith(".png") || x.EndsWith(".jpg") || x.EndsWith(".jpeg");
                    }

                    return x.EndsWith($"{_allowKeyword}.png") || x.EndsWith($"{_allowKeyword}.jpg") ||
                           x.EndsWith($"{_allowKeyword}.jpeg");
                })
                .Where(x => x.EndsWith(".png") || x.EndsWith(".jpg") || x.EndsWith(".jpeg"))
                .Where(x => !x.Contains(".original"))
                .Select(x => Path.Combine(directoryPath, x))
                .Select(x => (Path: x, Texture: AssetDatabase.LoadAssetAtPath<Texture2D>(x)))
                .Where(x => x.Item2 != null)
                .ToArray();

            foreach (var target in targets)
            {
                var importer = AssetImporter.GetAtPath(target.Path);
                if (importer is TextureImporter textureImporter)
                {
                    if (textureImporter.spriteBorder != Vector4.zero && _isAgainSlice == false) continue;
                    var fullPath = Path.Combine(Path.GetDirectoryName(Application.dataPath) ?? "", target.Path);
                    var bytes = File.ReadAllBytes(fullPath);

                    // バックアップ
                    if (CreateBackup)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(fullPath);
                        File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(fullPath) ?? "", fileName + ".original" + Path.GetExtension(fullPath)), bytes);
                    }

                    // importerのreadable設定に依らずに読み込むために直接読む
                    var targetTexture = new Texture2D(2, 2);
                    targetTexture.LoadImage(bytes);

                    var slicedTexture = Slicer.Slice(targetTexture, Options);
                    textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.spriteBorder = slicedTexture.Border.ToVector4();
                    if (fullPath.EndsWith(".png")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToPNG());
                    if (fullPath.EndsWith(".jpg")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToJPG());
                    if (fullPath.EndsWith(".jpeg")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToJPG());

                    Debug.Log($"Auto 9Slice {Path.GetFileName(target.Path)} = {textureImporter.spriteBorder}");
                }
            }

            AssetDatabase.Refresh();
        }
    }

    [CustomEditor(typeof(Auto9SliceTester))]
    public class Auto9SliceTesterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Run")) ((Auto9SliceTester) target).Run();
        }
    }
}