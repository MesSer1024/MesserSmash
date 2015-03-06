using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using MesserSmash.Modules;
using System.IO;

namespace MesserSmash.Arenas
{
    class LevelBuilder
    {
        private static string EXTERNAL_LEVELS = System.IO.Path.Combine(Environment.CurrentDirectory, "./levels/");
        private static string EXTERNAL_LEVELS_DEFAULT = System.IO.Path.Combine(Environment.CurrentDirectory, "../../SmashTV/SmashTV/external/default_levels/");
        public static List<jsLevel> LevelsData { get; private set; }

        internal static void init()
        {
            string levelData = loadLevelsByPath(EXTERNAL_LEVELS_DEFAULT);
            LevelsData = generateLevel(levelData);
        }

        private static string loadLevelsByPath(string path)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                throw new Exception();
            var files = dir.GetFiles("*.txt");
            if (files.Length == 0)
                throw new Exception();
            var sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < files.Length; ++i)
            {
                sb.Append(File.ReadAllText(files[i].FullName));
                if (i < files.Length - 1)
                    sb.Append(',');
                else
                    sb.Append(']');
            }
            return sb.ToString();
        }

        public static void setDebugLevelData(string rawData) {
            LevelsData = JsonConvert.DeserializeObject<List<jsLevel>>(rawData);
        }

        public static List<jsLevel> generateLevel(string data)
        {
            return JsonConvert.DeserializeObject<List<jsLevel>>(data);
        }

        public class jsLevel
        {
            public int Level = 0;
            public int Time = 0;
            public List<WaveSpawner> Waves = null;
            public bool NeedCode = false;

            private bool IsDirty = false;

            public void init()
            {
                if (IsDirty)
                {
                    foreach (var wave in Waves)
                    {
                        wave.enabled(true);
                    }
                }
                IsDirty = true;
            }
        }

        private static string getFilePath(int level)
        {
            var dir = new DirectoryInfo(EXTERNAL_LEVELS);
            var filename = String.Format("level_{0}.lvl", level);
            return Path.Combine(dir.FullName, filename);
        }

        public static jsLevel GetLevelData(int level)
        {
            var path = getFilePath(level);
            if (File.Exists(path))
                return JsonConvert.DeserializeObject<jsLevel>(File.ReadAllText(path));
            return null;
        }


        public static void SaveLevelData(jsLevel level)
        {
            var dir = new DirectoryInfo(EXTERNAL_LEVELS);
            if (!dir.Exists)
                dir.Create();

            var path = getFilePath(level.Level);
            var data = JsonConvert.SerializeObject(level);
            File.WriteAllText(path, data);
        }

        public static void SaveLevelData(int level, string data)
        {
            throw new NotImplementedException();
        }


        internal static Arena buildLevel(int levelNumber)
        {
            foreach (var levelData in LevelsData)
            {
                if (levelData.Level == levelNumber)
                {
                    levelData.init();
                    if (!levelData.NeedCode)
                    {
                        return new GeneratedLevel(levelData);
                    }
                    else
                    {
                        return buildCustomLevel(levelData);
                    }
                }
            }
            throw new Exception();
        }

        private static Arena buildCustomLevel(jsLevel levelData)
        {
            switch (levelData.Level)
            {
                case 10:
                    return new Level10();

            }
            throw new Exception();
        }
    }
}
