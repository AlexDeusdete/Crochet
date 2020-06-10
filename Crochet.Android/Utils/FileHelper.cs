﻿using System;
using System.IO;
using Crochet.Droid.Utils;
using Crochet.Utils;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Crochet.Droid.Utils
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}