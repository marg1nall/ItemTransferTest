namespace SaveLoad.Models
{
    public class SaveLoadStateGuard
    {
        private bool _isBusy;

        public bool TryBegin()
        {
            if (_isBusy)
            {
                return false;
            }

            _isBusy = true;
            return true;
        }

        public void End()
        {
            _isBusy = false;
        }
    }
}
