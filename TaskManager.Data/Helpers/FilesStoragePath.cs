using System.IO;

namespace TaskManager.Data.Helpers
{
    public static class FilesStorageHelper
    {
        public static string StoragePath = (Directory.GetParent((Directory.GetCurrentDirectory()))).FullName + "/UserFiles";
    }
}