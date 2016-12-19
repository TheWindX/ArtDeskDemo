using ns_artDesk.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using ns_artDesk.core.Util;
using Shell32;

namespace ns_artDesk.module
{
    class AppStat
    {
        public String Name { get; set; }
        public String SystemShortcutPath { get; set; }
        public bool IsInstalled { get; set; }

        public String DirPath
        {
            get
            { 
                return Path.Combine(Config.Instance.work_space_dir.Replace('/', '\\'), "apps", Name);
            }
        }

        public String AppShortcutPath
        {
            get
            {
                var fileName = Path.GetFileName(SystemShortcutPath);
                var path = Path.Combine(new String[] {DirPath, "data", fileName });
                if (0 == path.IndexOf('\\'))
                {
                    path = path.Substring(1);
                }
                return path;
            }
        }

        public String LinkTargetPath
        {
            get
            {
                return FileSystemUtil.GetLinkTargetPath(AppShortcutPath);
            }
        }
    }

    [ModuleInstance(4)]
    class AppStatModule : CModule
    {
        const String LNK_DIR_PATH = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs";
        const String LNK_FILTER = "*.lnk";

        Dictionary<String, AppStat> mAppStatDict;
        HashSet<String> mLnkPaths;
        FileSystemWatcher lnkDirWatcher;

        public void onExit()
        {

        }

        public void onInit()
        {
            mLnkPaths = GetAllLnkPaths();

            lnkDirWatcher = new FileSystemWatcher() { Path = LNK_DIR_PATH,
                IncludeSubdirectories =true, EnableRaisingEvents=true,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName
            };
            lnkDirWatcher.Changed += FileSystemWatcherEventHandler;
            lnkDirWatcher.Created += FileSystemWatcherEventHandler;
            lnkDirWatcher.Deleted += FileSystemWatcherEventHandler;
            lnkDirWatcher.Renamed += FileSystemWatcherEventHandler;

            mAppStatDict = new Dictionary<string, AppStat>();
            foreach (var appConfig in CArtAppList.Instance.apps)
            {
                AppStat appStat = new AppStat()
                {
                    Name = appConfig.meta.name,
                    SystemShortcutPath = appConfig.meta.link_path.Replace('/', '\\'),
                    IsInstalled = false
                };
                
                CheckAppShutcutValid(appStat);
                mAppStatDict.Add(appStat.Name, appStat);
            }
        }

        public void onUpdate()
        {
            
        }

        private void FileSystemWatcherEventHandler(object source, FileSystemEventArgs e)
        {
            HashSet<String> oldLnkPath = mLnkPaths;
            mLnkPaths = GetAllLnkPaths();

            HashSet<String> addedLnkPaths = new HashSet<string>(mLnkPaths);
            addedLnkPaths.ExceptWith(oldLnkPath); // remain added paths

            oldLnkPath.ExceptWith(mLnkPaths); // remain removed paths
            HashSet<String> removedLnkPaths = oldLnkPath;

            foreach(AppStat appStat in mAppStatDict.Values)
            {
                if (removedLnkPaths.Contains(appStat.SystemShortcutPath))
                {
                    CheckAppShutcutValid(appStat);
                }
                if (addedLnkPaths.Contains(appStat.SystemShortcutPath))
                {
                    CheckAppShutcutValid(appStat);
                }
            }
        }

        private HashSet<String> GetAllLnkPaths()
        {
            HashSet<String> hsLnkPaths = new HashSet<String>();
            String[] lnkPaths = FileSystemUtil.GetFiles(LNK_DIR_PATH, LNK_FILTER, SearchOption.AllDirectories);
            foreach (String lnkPath in lnkPaths)
            {
                hsLnkPaths.Add(lnkPath);
            }

            return hsLnkPaths;
        }

        private void CheckAppShutcutValid(AppStat appStat)
        {
            if (appStat == null)
                return;

            appStat.IsInstalled = false;

            if (FileSystemUtil.ExistFile(appStat.AppShortcutPath))
            {
                String linkTargetPath = appStat.LinkTargetPath;
                if (linkTargetPath != null && FileSystemUtil.ExistFile(linkTargetPath))
                {
                    appStat.IsInstalled = true;
                }
                else
                {
                    FileSystemUtil.DeleteFile(appStat.AppShortcutPath);
                }
            }
            else
            { 
                if (FileSystemUtil.ExistFile(appStat.SystemShortcutPath))
                {
                    String linkTargetPath = FileSystemUtil.GetLinkTargetPath(appStat.SystemShortcutPath);
                    if (linkTargetPath != null && FileSystemUtil.ExistFile(linkTargetPath))
                    {
                        bool dirReady = true;
                        {
                            String appShortcutDir = Path.GetDirectoryName(appStat.AppShortcutPath);
                            if (!FileSystemUtil.ExistDir(appShortcutDir))
                            {
                                if (FileSystemUtil.CreateDir(appShortcutDir) == null)
                                {
                                    dirReady = false;
                                }
                            }
                        }
                        if (dirReady)
                        {
                            FileSystemUtil.CopyTo(appStat.SystemShortcutPath, appStat.AppShortcutPath, true);
                            appStat.IsInstalled = true;
                        }
                    }
                }
            }
        }

        public AppStat GetAppStat(String appName)
        {
            if (appName != null && mAppStatDict.ContainsKey(appName))
            {
                return mAppStatDict[appName];
            }

            return null;
        }
        public bool StartApp(String appName)
        {
            AppStat appStat = GetAppStat(appName);
            if (appStat == null)
                return false;

            if (!appStat.IsInstalled)
                return false;

            if (Process.Start(appStat.AppShortcutPath) == null)
                return false;

            return true;
        }
    }
}
