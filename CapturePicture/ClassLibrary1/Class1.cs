using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ClassLibrary1
{
    public class Class1
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DHDEV_SNAP_CFG{

            
        }
        [DllImport("dhnetsdk.dll")]
        public static extern bool CLIENT_GetDevConfig((LLONG lLoginID, DWORD dwCommand, LONG lChannel, LPVOID lpOutBuffer, DWORD dwOutBufferSize, LPDWORD lpBytesReturned,int waittime=500);

        
    }
}
