namespace RPiWindows.Clients
{
    interface IClient
    {
        void Send(string address, string port, string message);
    }
}
