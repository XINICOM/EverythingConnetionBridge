using System.Runtime.InteropServices;
using System.Text;

namespace EverythingConnetionBridge
{
    public static class EVERYTHING
    {
        public enum RESULT : uint
        {
            OK = 0,
            ERROR_MEMORY = 1,
            ERROR_IPC = 2,
            ERROR_REGISTERCLASSEX = 3,
            ERROR_CREATEWINDOW = 4,
            ERROR_CREATETHREAD = 5,
            ERROR_INVALIDINDEX = 6,
            ERROR_INVALIDCALL = 7,
        }
        [Flags]
        public enum REQUEST : uint
        {
            FILE_NAME = 0x00000001,
            PATH = 0x00000002,
            FULL_PATH_AND_FILE_NAME = 0x00000004,
            EXTENSION = 0x00000008,
            SIZE = 0x00000010,
            DATE_CREATED = 0x00000020,
            DATE_MODIFIED = 0x00000040,
            DATE_ACCESSED = 0x00000080,
            ATTRIBUTES = 0x00000100,
            FILE_LIST_FILE_NAME = 0x00000200,
            RUN_COUNT = 0x00000400,
            DATE_RUN = 0x00000800,
            DATE_RECENTLY_CHANGED = 0x00001000,
            HIGHLIGHTED_FILE_NAME = 0x00002000,
            HIGHLIGHTED_PATH = 0x00004000,
            HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 0x00008000
        }
        public enum SORT : uint
        {
            NAME_ASCENDING = 1,
            NAME_DESCENDING = 2,
            PATH_ASCENDING = 3,
            PATH_DESCENDING = 4,
            SIZE_ASCENDING = 5,
            SIZE_DESCENDING = 6,
            EXTENSION_ASCENDING = 7,
            EXTENSION_DESCENDING = 8,
            TYPE_NAME_ASCENDING = 9,
            TYPE_NAME_DESCENDING = 10,
            DATE_CREATED_ASCENDING = 11,
            DATE_CREATED_DESCENDING = 12,
            DATE_MODIFIED_ASCENDING = 13,
            DATE_MODIFIED_DESCENDING = 14,
            ATTRIBUTES_ASCENDING = 15,
            ATTRIBUTES_DESCENDING = 16,
            FILE_LIST_FILENAME_ASCENDING = 17,
            FILE_LIST_FILENAME_DESCENDING = 18,
            RUN_COUNT_ASCENDING = 19,
            RUN_COUNT_DESCENDING = 20,
            DATE_RECENTLY_CHANGED_ASCENDING = 21,
            DATE_RECENTLY_CHANGED_DESCENDING = 22,
            DATE_ACCESSED_ASCENDING = 23,
            DATE_ACCESSED_DESCENDING = 24,
            DATE_RUN_ASCENDING = 25,
            DATE_RUN_DESCENDING = 26,
        }
        public enum TARGET_MACHINE : uint
        {
            X86 = 1,
            X64 = 2,
            ARM = 3,
        }
    }

    public partial class ECB
    {
        private const string DllName = "Everything64.dll";

        [LibraryImport(DllName, EntryPoint = "Everything_SetSearchW", StringMarshalling = StringMarshalling.Utf16)]
        public static partial uint SetSearch(string lpSearchString);

        [LibraryImport(DllName, EntryPoint = "Everything_SetMatchPath")]
        public static partial void SetMatchPath([MarshalAs(UnmanagedType.Bool)] bool bEnable);

        [LibraryImport(DllName, EntryPoint = "Everything_SetMatchCase")]
        public static partial void SetMatchCase([MarshalAs(UnmanagedType.Bool)] bool bEnable);

        [LibraryImport(DllName, EntryPoint = "Everything_SetMatchWholeWord")]
        public static partial void SetMatchWholeWord([MarshalAs(UnmanagedType.Bool)] bool bEnable);

        [LibraryImport(DllName, EntryPoint = "Everything_SetRegex")]
        public static partial void SetRegex([MarshalAs(UnmanagedType.Bool)] bool bEnable);

        [LibraryImport(DllName, EntryPoint = "Everything_SetMax")]
        public static partial void SetMax(uint dwMax);

        [LibraryImport(DllName, EntryPoint = "Everything_SetOffset")]
        public static partial void SetOffset(uint dwOffset);

        [LibraryImport(DllName, EntryPoint = "Everything_GetMatchPath")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetMatchPath();

        [LibraryImport(DllName, EntryPoint = "Everything_GetMatchCase")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetMatchCase();

        [LibraryImport(DllName, EntryPoint = "Everything_GetMatchWholeWord")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetMatchWholeWord();

        [LibraryImport(DllName, EntryPoint = "Everything_GetRegex")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetRegex();

        [LibraryImport(DllName, EntryPoint = "Everything_GetMax")]
        public static partial uint GetMax();

        [LibraryImport(DllName, EntryPoint = "Everything_GetOffset")]
        public static partial uint GetOffset();

        [LibraryImport(DllName, EntryPoint = "Everything_GetSearchW")]
        public static partial IntPtr GetSearch();

        [LibraryImport(DllName, EntryPoint = "Everything_GetLastError")]
        public static partial uint GetLastError();

        [LibraryImport(DllName, EntryPoint = "Everything_QueryW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool Query([MarshalAs(UnmanagedType.Bool)] bool bWait);

        [LibraryImport(DllName, EntryPoint = "Everything_SortResultsByPath")]
        public static partial void SortResultsByPath();

        [LibraryImport(DllName, EntryPoint = "Everything_GetNumFileResults")]
        public static partial uint GetNumFileResults();

        [LibraryImport(DllName, EntryPoint = "Everything_GetNumFolderResults")]
        public static partial uint GetNumFolderResults();

        [LibraryImport(DllName, EntryPoint = "Everything_GetNumResults")]
        public static partial uint GetNumResults();

        [LibraryImport(DllName, EntryPoint = "Everything_GetTotFileResults")]
        public static partial uint GetTotFileResults();

        [LibraryImport(DllName, EntryPoint = "Everything_GetTotFolderResults")]
        public static partial uint GetTotFolderResults();

        [LibraryImport(DllName, EntryPoint = "Everything_GetTotResults")]
        public static partial uint GetTotResults();

        [LibraryImport(DllName, EntryPoint = "Everything_IsVolumeResult")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool IsVolumeResult(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_IsFolderResult")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool IsFolderResult(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_IsFileResult")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool IsFileResult(uint nIndex);

        [DllImport(DllName, CharSet = CharSet.Unicode, EntryPoint = "Everything_GetResultFullPathName")]
        public static extern void GetResultFullPathName(uint nIndex, StringBuilder lpString, uint nMaxCount);

        [LibraryImport(DllName, EntryPoint = "Everything_Reset")]
        public static partial void Reset();

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultFileNameW")]
        public static partial IntPtr GetResultFileName(uint nIndex);

        // Everything 1.4
        [LibraryImport(DllName, EntryPoint = "Everything_SetSort")]
        public static partial void SetSort(uint dwSortType);

        [LibraryImport(DllName, EntryPoint = "Everything_GetSort")]
        public static partial uint GetSort();

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultListSort")]
        public static partial uint GetResultListSort();

        [LibraryImport(DllName, EntryPoint = "Everything_SetRequestFlags")]
        public static partial void SetRequestFlags(uint dwRequestFlags);

        [LibraryImport(DllName, EntryPoint = "Everything_GetRequestFlags")]
        public static partial uint GetRequestFlags();

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultListRequestFlags")]
        public static partial uint GetResultListRequestFlags();

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultExtension")]
        public static partial IntPtr GetResultExtension(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultSize")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetResultSize(uint nIndex, out long lpFileSize);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultDateCreated")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetResultDateCreated(uint nIndex, out long lpFileTime);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultDateModified")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetResultDateModified(uint nIndex, out long lpFileTime);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultDateAccessed")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetResultDateAccessed(uint nIndex, out long lpFileTime);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultAttributes")]
        public static partial uint GetResultAttributes(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultFileListFileName")]
        public static partial IntPtr GetResultFileListFileName(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultRunCount")]
        public static partial uint GetResultRunCount(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultDateRun")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetResultDateRun(uint nIndex, out long lpFileTime);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultDateRecentlyChanged")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetResultDateRecentlyChanged(uint nIndex, out long lpFileTime);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultHighlightedFileName")]
        public static partial IntPtr GetResultHighlightedFileName(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultHighlightedPath")]
        public static partial IntPtr GetResultHighlightedPath(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_GetResultHighlightedFullPathAndFileName")]
        public static partial IntPtr GetResultHighlightedFullPathAndFileName(uint nIndex);

        [LibraryImport(DllName, EntryPoint = "Everything_GetRunCountFromFileNameW", StringMarshalling = StringMarshalling.Utf16)]
        public static partial uint GetRunCountFromFileName(string lpFileName);

        [LibraryImport(DllName, EntryPoint = "Everything_SetRunCountFromFileNameW", StringMarshalling = StringMarshalling.Utf16)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool SetRunCountFromFileName(string lpFileName, uint dwRunCount);

        [LibraryImport(DllName, EntryPoint = "Everything_IncRunCountFromFileNameW", StringMarshalling = StringMarshalling.Utf16)]
        public static partial uint IncRunCountFromFileName(string lpFileName);
    }
}
