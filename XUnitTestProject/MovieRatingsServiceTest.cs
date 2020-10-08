using Comp1.Core.Interfaces;
using Comp1.Core.Model;
using Comp1.Core.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingsServiceTest
    {
        private List<MovieRating> ratings = null;
        private Mock<IMovieRatingsRepository> repoMock;

        public MovieRatingsServiceTest()
        {
            repoMock = new Mock<IMovieRatingsRepository>();
            repoMock.Setup(repo => repo.GetAllMovieRatings()).Returns(() => ratings);
        }


        // Opgave 1
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void GetNumberOfReviewsFromReviewer(int reviewer, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 3, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now)
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            // act

            int result = mrs.NumberOfReviewsFromReviewer(reviewer);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 2
        [Theory]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        public void GetAverageRateFromReviewer(int rating, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now)
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            // act

            double result = mrs.AverageRateFromReviewer(rating);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 3
        [Theory]
        [InlineData(2, 3, 1)]
        [InlineData(3, 4, 2)]
        [InlineData(4, 4, 1)]
        public void GetNumberOfRatesByReviewer(int reviewer, int rating, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now)
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            // act

            double result = mrs.NumberOfRatesByReviewer(reviewer, rating);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 4
        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void GetNumberOfReviews(int movie, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now),
                new MovieRating(4, 3, 4, DateTime.Now)

            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            // act

            int result = mrs.NumberOfReviews(movie);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 5
        [Theory]
        [InlineData(1, 4)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void GetAverageRateOfMovie(int movie, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 4, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 2, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now),
                new MovieRating(4, 3, 2, DateTime.Now),
                new MovieRating(4, 3, 4, DateTime.Now)

            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            // act

            double result = mrs.AverageRateOfMovie(movie);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 6
        [Theory]
        [InlineData(1, 3, 1)]
        [InlineData(1, 4, 2)]
        [InlineData(2, 4, 1)]
        public void GetNumberOfRates(int movie, int rating, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 4, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now)
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            // act

            double result = mrs.NumberOfRates(movie, rating);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }


        //Opgave 7
        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 5, DateTime.Now),

                new MovieRating(2, 1, 4, DateTime.Now),
                new MovieRating(2, 2, 5, DateTime.Now),

                new MovieRating(2, 3, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now),
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            List<int> expected = new List<int>() { 2, 3 };

            // act
            var result = mrs.MoviesWithHighestNumberOfTopRates();

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 8
        [Fact]
        public void GetMostProductiveReviewers()
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 5, DateTime.Now),

                new MovieRating(2, 1, 4, DateTime.Now),
                new MovieRating(2, 2, 5, DateTime.Now),

                new MovieRating(2, 3, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now),
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            List<int> expected = new List<int>() { 2 };

            // act
            var result = mrs.MostProductiveReviewers();

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 9
        [Theory]
        [InlineData(1, new int[] { 4 })]
        [InlineData(2, new int[] { 4, 1 })]
        [InlineData(4, new int[] { 4, 1, 2, 3 })]
        [InlineData(10, new int[] { 4, 1, 2, 3 })]
        public void GetTopRatedMovies(int n, int[] expected)
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 2, 3, DateTime.Now),     // movie 1 avg = 4                                                            
                new MovieRating(1, 3, 2, DateTime.Now),     // movie 2 avg = 3
                new MovieRating(2, 1, 4, DateTime.Now),     // movie 3 avg = 2.5
                new MovieRating(2, 3, 3, DateTime.Now),     // movie 4 avg = 4.5
                new MovieRating(2, 4, 4, DateTime.Now),
                new MovieRating(3, 4, 5, DateTime.Now)
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);

            var result = mrs.TopRatedMovies(n);

            Assert.Equal(new List<int>(expected), result);
        }

        //Opgave 10
        [Theory]
        [InlineData(2, new int[] { 1 })]
        [InlineData(3, new int[] { 2, 3, 1 })]
        [InlineData(4, new int[] { 1 })]
        public void GetTopMoviesByReviewer(int reviewer, int[] expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 2, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now.AddDays(-1)),
                new MovieRating(4, 1, 4, DateTime.Now)
            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);


            // act

            var result = mrs.TopMoviesByReviewer(reviewer);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }

        //Opgave 11
        [Theory]
        [InlineData(2, new int[] { 3 })]
        [InlineData(1, new int[] { 4, 3, 2 })]
        [InlineData(3, new int[] { 4 })]
        public void GetReviewersByMovie(int movie, int[] expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now.AddDays(-1)),
                new MovieRating(3, 2, 5, DateTime.Now),
                new MovieRating(4, 1, 4, DateTime.Now),
                new MovieRating(4, 3, 4, DateTime.Now)

            };

            MovieRatingsService mrs = new MovieRatingsService(repoMock.Object);


            // act

            var result = mrs.ReviewersByMovie(movie);

            // assert
            Assert.Equal(expected, result);
            repoMock.Verify(repo => repo.GetAllMovieRatings(), Times.Once);
        }
    }
}
