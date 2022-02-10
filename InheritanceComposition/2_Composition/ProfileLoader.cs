namespace CleanCode.InheritanceComposition.Composition
{
    public class ProfileLoader : ILoader
    {
        private IProgressBar _progress;

        public ProfileLoader(IProgressBar progress)
        {
            _progress = progress;
        }

        public void Load()
        {
            _progress.Show();
            // load profile
            _progress.Hide();
        }
    }
}
