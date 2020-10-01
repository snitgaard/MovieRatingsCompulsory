using MovieRatingsApplication.Core.Interfaces;
using MovieRatingsApplication.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieRatingsApplication.Core.Services
{
    public class MovieRatingsService
    {
        private readonly IMovieRatingsRepository RatingsRepository;

        public MovieRatingsService(IMovieRatingsRepository repo)
        {
            RatingsRepository = repo;
        }

        public int NumberOfMoviesWithGrade(int grade)
        {
            if (grade < 1 || grade > 5)
            {
                throw new ArgumentException("Grade must be 1 - 5");
            }

            HashSet<int> movies = new HashSet<int>();
            foreach (MovieRating rating in RatingsRepository.GetAllMovieRatings())
            {
                if (rating.Grade == grade)
                {
                    movies.Add(rating.Movie);
                }
            }
            return movies.Count;
        }

        //Opgave 1
        public int NumberOfReviewsFromReviewer(int review)
        {
            int count = 0;
            foreach (MovieRating m in RatingsRepository.GetAllMovieRatings())
            {
                if (m.Reviewer == review)
                {
                    count++;
                }
            }
            return count;
        }

        //Opgave 2
        public double AverageRateFromReviewer(int rating)
        {
            double ratingSum = RatingsRepository.GetAllMovieRatings()
                .Where(r => r.Reviewer == rating)
                .Average(r => r.Grade);
            return ratingSum;
        }

        //Opgave 3
        public int NumberOfRatesByReviewer(int reviewer, int rate)
        {
            int numberOfRates = RatingsRepository.GetAllMovieRatings()
                .Where(r => r.Reviewer == reviewer)
                .Where(r => r.Grade == rate)
                .Count();

            return numberOfRates;
        }

        //Opgave 4
        public int NumberOfReviews(int movie)
        {
            int count = 0;
            foreach (MovieRating m in RatingsRepository.GetAllMovieRatings())
            {
                if (m.Movie == movie)
                {
                    count++;
                }
            }
            return count;
        }

        //Opgave 5
        public double AverageRateOfMovie(int movie)
        {
            double ratingSum = RatingsRepository.GetAllMovieRatings()
                .Where(r => r.Movie == movie)
                .Average(r => r.Grade);
            return ratingSum;
        }

        //Opgave 6
        public int NumberOfRates(int movie, int rate)
        {
            int numberOfRates = RatingsRepository.GetAllMovieRatings()
                .Where(r => r.Movie == movie)
                .Where(r => r.Grade == rate)
                .Count();

            return numberOfRates;
        }

        //Opgave 7
        public List<int> MoviesWithHighestNumberOfTopRates()
        {
            var movie5 = RatingsRepository.GetAllMovieRatings()
                .Where(r => r.Grade == 5)
                .GroupBy(r => r.Movie)
                .Select(group => new {
                    Movie = group.Key,
                    MovieGrade5 = group.Count()
                });

            int max5 = movie5.Max(grp => grp.MovieGrade5);

            return movie5
                .Where(grp => grp.MovieGrade5 == max5)
                .Select(grp => grp.Movie)
                .ToList();
        }

        //Opgave 8
        public List<int> MostProductiveReviewers()
        {

            var maxReviews = RatingsRepository.GetAllMovieRatings()
                .GroupBy(r => r.Reviewer)
                .Select(group => new
            {
                Review = group.Key,
                MaxReviews = group.Count()
            });

            int max = maxReviews.Max(grp => grp.MaxReviews);
            return maxReviews
                .Where(grp => grp.MaxReviews == max)
                .Select(grp => grp.Review)
                .ToList();
        }

        //Opgave 9
        //public List<int> TopRatedMovies(int amount)
        //{
        //    var topAverageMovie = RatingsRepository.GetAllMovieRatings()
        //        .GroupBy(r => r.Grade)
        //        .Select(group => new
        //        {
        //            Rating = group.Key,
        //            TopRatedMovie = group.Count()
        //        });

        //    double averageAmount = topAverageMovie.Average(grp => grp.TopRatedMovie);
        //    return topAverageMovie
        //        .Where(grp => grp.TopRatedMovie == averageAmount)
        //        .Select(grp => grp.Rating)
        //        .ToList();
        //}

        //Opgave 10
        public List<int> TopMoviesByReviewer(int reviewer)
        {
            var topMovies = RatingsRepository.GetAllMovieRatings()
                .GroupBy(r => r.Reviewer)
                .Select(group => new
                {
                    Review = group.Key,
                    Movies = group.OrderBy(r => r.Grade),
                    Date = group.OrderBy(r => r.Date)
                });

            return topMovies
                .Where(grp => grp.Review == reviewer)
                .Select(grp => grp.Review)
                .ToList();
        }

    }
}
