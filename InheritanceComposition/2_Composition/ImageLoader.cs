namespace CleanCode.InheritanceComposition.Composition
{
    public class ImageLoader : ILoader
    {
        private IProgressBar _progress;

        public ImageLoader(IProgressBar progress)
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
