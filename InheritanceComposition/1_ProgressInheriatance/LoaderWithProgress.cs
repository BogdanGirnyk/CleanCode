using System;

namespace CleanCode.InheritanceComposition.ProgressInheriatance
{
    public abstract class LoaderWithProgress: ILoader
    {
        public void Load()
        {
            ShowProgress();
            LoadAction();
            HideProgress();
        }

        protected abstract void LoadAction();

        private void ShowProgress()
        {
            // Show progress
        }

        private void HideProgress()
        {
            // Hide progress
        }
    }
}
