namespace RPiWindows.LANClients
{
    interface IClient
    {
        void Send(string address, string port, string message);
    }
}
