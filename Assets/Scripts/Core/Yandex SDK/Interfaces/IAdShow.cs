namespace Interfaces
{
    public interface IAdShow
    {
        void OnCloseCallback();

        void ContinueGame();

        void PauseGame();

        void OnOpenCallback();

        void OnErrorCallback(string errorMessage);
    }
}
