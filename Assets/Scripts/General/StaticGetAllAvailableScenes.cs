using UnityEngine;
using System.Collections;

public class StaticGetAllAvailableScenes
{
    public static string[] Levels = new string[] { "Prototype", "Character Customization", "DayNightCycle"};     // Holds all scenenames, for now hardcoded, later on automatically generated..
}

//using System;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using Debug = UnityEngine.Debug;

//namespace Assets.Scenes.Scripts
//{
//    /// <summary>
//    /// A context that is always available in the game. It provides global information about scenes
//    /// </summary>
//    public static class StaticGetAllAvailableScenes
//    {
//        #region Properties
//        /// <summary>
//        /// The folder the levels file is located when the game is hosted in the editor.
//        /// </summary>
//        private const string EDITOR_LEVELS_FILE_DIRECTORY = "Assets/Scenes/";
//        /// <summary>
//        /// The folder the levels file is located when the game has been deployed.
//        /// </summary>
//        private const string BUILD_LEVELS_FILE_DIRECTORY = "";
//        /// <summary>
//        /// The filename and extension of the file that contains the available levels.
//        /// </summary>
//        private const string LEVEL_FILE_NAME = "Levels.ini";

//        /// <summary>
//        /// The names of all levels in the game.
//        /// </summary>
//        private static string[] _levels;


//        /// <summary>
//        /// Gets the names of all levels in the game.
//        /// </summary>
//        public static string[] Levels
//        {
//            get
//            {
//                if (_levels == null)
//                {
//                    string directory;

//                    // The directory depends on the environment. In the editor, relative paths can be used.
//                    if (Application.isEditor)
//                    {
//                        directory = EDITOR_LEVELS_FILE_DIRECTORY;
//                    }
//                    else
//                    {
//                        string dataPath = Application.dataPath;

//                        Debug.Log(string.Format("Data path detected at '{0}'.", dataPath));

//                        directory = Path.Combine(dataPath ?? string.Empty, BUILD_LEVELS_FILE_DIRECTORY);
//                    }

//                    _levels = ReadLevelsFile(directory);

//                    Debug.Log(string.Format("Discovered level names: {0}.", string.Join(", ", _levels)));
//                }

//                return _levels;
//            }
//        }



//        #region Level Management
//#if UNITY_EDITOR
//        /// <summary>
//        /// This method is called when post-processing a build, which occurs after a build has been made. This method updates the levels file in the build directory.
//        /// </summary>
//        [UnityEditor.Callbacks.PostProcessBuild]
//        public static void PostProcessBuild(UnityEditor.BuildTarget target, string pathToBuiltProject)
//        {
//            const string DATA_FOLDER = "{0}_Data";

//            Debug.Log(string.Format("Post-processing build '{0}' at '{1}'.", target, pathToBuiltProject));

//            // The file name is integrated in some folder/file names of the built game. It may be needed to create references to these dynamic folders/files.
//            string fileName = Path.GetFileNameWithoutExtension(pathToBuiltProject);
//            // The build directory is the build path, without file name and extension and appended with the custom path.
//            string buildDirectory = Path.Combine(Path.Combine(Path.GetDirectoryName(pathToBuiltProject) ?? string.Empty,
//                                                                string.Format(DATA_FOLDER, fileName)),
//                                                  BUILD_LEVELS_FILE_DIRECTORY);

//            Debug.Log(string.Format("Detected levels file directory '{0}'.", buildDirectory));

//            WriteLevelsFile(buildDirectory);

//            Debug.Log("Post-processed build.");
//        }

//        /// <summary>
//        /// This method is called when post-processing a scene, which occurs in either the editor when running a scene or at build time when building a scene. This
//        /// method updates the levels file, if applicable.
//        /// </summary>
//        [UnityEditor.Callbacks.PostProcessScene]
//        public static void PostProcessScene()
//        {
//            Debug.Log("Post-processing scene.");

//            if (!_hasUpdatedLevelsFile)
//            {
//                // Only write a levels file if we're in the editor. If not, the PostProcessBuild method will do this, because the PostProcessScene is called for all scenes.
//                if (Application.isEditor)
//                {
//                    Debug.Log(string.Format("Detected editor, writing levels file to '{0}'.", EDITOR_LEVELS_FILE_DIRECTORY));

//                    WriteLevelsFile(EDITOR_LEVELS_FILE_DIRECTORY);
//                    _hasUpdatedLevelsFile = true;
//                }
//            }
//            else
//            {
//                Debug.Log("Already updated levels file.");
//            }

//            Debug.Log("Post-processed scene.");
//        }

//        /// <summary>
//        /// Writes or creates the levels file by collecting all levels configured in the build and (re-)writing the levels file at the provided directory.
//        /// </summary>
//        /// <param name="directory">The directory to write the levels file to.</param>
//        private static void WriteLevelsFile(string directory)
//        {
//            List<string> levelNames = new List<string>();

//            // Collect the names of all levels in the build settings.
//            foreach (UnityEditor.EditorBuildSettingsScene buildSettingsScene in UnityEditor.EditorBuildSettings.scenes)
//            {
//                if (buildSettingsScene.enabled)
//                {
//                    string name = buildSettingsScene.path.Substring(buildSettingsScene.path.LastIndexOf(Path.AltDirectorySeparatorChar) + 1);
//                    name = name.Substring(0, name.Length - 6);

//                    levelNames.Add(name);

//                    Debug.Log(string.Format("Detected level at '{0}' with name '{1}'.", buildSettingsScene.path, name));
//                }
//            }

//            string path = Path.Combine(directory, LEVEL_FILE_NAME);

//            Debug.Log(string.Format("Writing levels file to '{0}'.", path));

//            // Write the names of all levels to a file, so that it can be retrieved when running.
//            using (FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write))
//            {
//                using (StreamWriter writer = new StreamWriter(stream))
//                {
//                    foreach (string levelName in levelNames)
//                    {
//                        writer.WriteLine(levelName);
//                    }
//                }
//            }
//        }
//#endif

//        /// <summary>
//        /// Reads the levels file from the provided directory.
//        /// </summary>
//        /// <param name="directory">The directory that contains the levels file.</param>
//        /// <returns>The discovered levels.</returns>
//        private static string[] ReadLevelsFile(string directory)
//        {
//            string path = Path.Combine(directory, LEVEL_FILE_NAME);

//            Debug.Log(string.Format("Reading levels file from '{0}'.", path));

//            List<string> levelNames = new List<string>();

//            if (File.Exists(path))
//            {
//                // Read the names of all levels from the levels file.
//                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
//                {
//                    using (StreamReader reader = new StreamReader(stream))
//                    {
//                        // Possibly use ReadToEnd and string.Split(fileContent, Environment.NewLine).
//                        while (!reader.EndOfStream)
//                        {
//                            levelNames.Add(reader.ReadLine());
//                        }
//                    }
//                }
//            }
//            else
//            {
//                Debug.LogWarning("Levels file does not exist, no level names are available at runtime.");
//            }

//            return levelNames.ToArray();
//        }
//        #endregion
//    }
//}