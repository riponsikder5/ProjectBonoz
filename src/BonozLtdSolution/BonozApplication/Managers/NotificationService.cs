//namespace BonozApplication.Managers
//{
//    public class NotificationService
//    {
//        public string Message { get; set; }
//        public bool IsSuccess { get; set; }

//        public event Action OnChange;

//        public void SetMessage(string message, bool isSuccess)
//        {
//            Message = message;
//            IsSuccess = isSuccess;
//            NotifyStateChanged();
//        }

//        private void NotifyStateChanged() => OnChange?.Invoke();
//    }
//}
