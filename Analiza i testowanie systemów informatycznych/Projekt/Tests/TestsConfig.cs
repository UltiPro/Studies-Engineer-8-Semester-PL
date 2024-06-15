namespace SPA.Tests
{
    public class TestsConfig
    {
        public string _basicSimpleCodeFile { get; }

        public string _bigSimpleCodeFile { get;}

        public string _basicTestsQueries { get; }

        public string _bigTestsQueries { get; }

        public TestsConfig(string basicSimpleCodeFile, string bigSimpleCodeFile, string basicTestsQueries, string bigTestsQueries)
        {
            _basicTestsQueries = basicTestsQueries;
            _bigTestsQueries = bigTestsQueries;
            _basicSimpleCodeFile = basicSimpleCodeFile;
            _bigSimpleCodeFile = bigSimpleCodeFile;

        }
    }
}
