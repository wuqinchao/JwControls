namespace Jw.Winform.Forms
{
    public interface IProgressService
    {
        event ProgressChangeEvent OnProgressChanged;
        event ProgressCompleteEvent OnComplete;
        bool CanCancel { get; }
        void Start();
        void Cancel();
    }
}
