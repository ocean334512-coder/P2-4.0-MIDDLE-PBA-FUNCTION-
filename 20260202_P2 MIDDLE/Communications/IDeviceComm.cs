using System;

namespace _20260202_P2_MIDDLE.Communications
{
    public interface IDeviceComm
    {
        bool Connect();
        void Disconnect();
        bool IsConnected { get; }
    }
}
