﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QWCTranslationEditor.JsonHelper;

namespace QWCTranslationEditor
{
    public class AppOptions
    {
        public SaveStyles SaveStyle { get; set; }
        public string DefaultPath { get; set; }
        public int PageSize { get; set; }
        public int TruncateResultsOver { get; set; }
        public int LoadingDepth { get; set; }

        static string path = System.IO.Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.MyDoc‌​uments), "QWCTranslationEditor");


        public static AppOptions FromDisk()
        {
            if (File.Exists(Path.Combine(path, "settings.json")))
            {
                var loaded = File.ReadAllText(Path.Combine(path, "settings.json"));
                var options = JsonConvert.DeserializeObject<AppOptions>(loaded);

                if (options.PageSize <= 0)
                    options.PageSize = 100;

                if (options.TruncateResultsOver <= 0)
                    options.TruncateResultsOver = 2000;

                if (options.LoadingDepth <= 0)
                    options.LoadingDepth = 1;

                return options;
            }

            return new AppOptions() { SaveStyle = SaveStyles.Json, PageSize = 100, TruncateResultsOver = 2000, LoadingDepth= 10 };

        }
        public void ToDisk()
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(Path.Combine(path, "settings.json"), json);

        }
    }
}
