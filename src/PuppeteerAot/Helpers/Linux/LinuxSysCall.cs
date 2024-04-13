using System.Runtime.InteropServices;

namespace PuppeteerAot.Helpers.Linux
{
    public static class LinuxSysCall
    {
        public const FileAccessPermissions ExecutableFilePermissions =
            FileAccessPermissions.UserRead | FileAccessPermissions.UserWrite | FileAccessPermissions.UserExecute |
            FileAccessPermissions.GroupRead |
            FileAccessPermissions.GroupExecute |
            FileAccessPermissions.OtherRead |
            FileAccessPermissions.OtherExecute;

        [DllImport("libc", SetLastError = true, EntryPoint = "chmod")]
        public static extern int Chmod(string path, FileAccessPermissions mode);
    }
}
