using Comp1.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRatingsJSONRepository;

namespace MSUnitTestProject1
{
    [TestClass]
    public class MovieRatingsPerformanceTest
    {

        private static MovieRatingsRepository repo;

        [ClassInitialize]
        public static void SetUpTest(TestContext tc)
        {
            repo = new MovieRatingsRepository(@"C:\Users\Mads\Documents\GitHub\MovieRatingsCompulsory\ratings.json");
        }

        [TestMethod]
        [Timeout(4000)]
        public void GetNumberofReviewsFromReviewer()
        {
            MovieRatingsServiceLinq service = new MovieRatingsServiceLinq(repo);
            service.NumberOfReviewsFromReviewer(1);
        }
    }
}
