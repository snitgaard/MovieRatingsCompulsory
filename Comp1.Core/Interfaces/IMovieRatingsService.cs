using System;
using System.Collections.Generic;
using System.Text;

namespace Comp1.Core.Interfaces
{
    public interface IMovieRatingsService
    {
        int NumberOfReviewsFromReviewer(int reviewer);
        double AverageRateFromReviewer(int reviewer);
        int NumberOfRatesByReviewer(int reviewer, int rate);
        int NumberOfReviews(int movie);
        double AverageRateOfMovie(int movie);
        int NumberOfRates(int movie, int rate);
        List<int> MoviesWithHighestNumberOfTopRates();
        List<int> MostProductiveReviewers();
        List<int> TopRatedMovies(int amount);
        List<int> TopMoviesByReviewer(int reviewer);
        List<int> ReviewersByMovie(int movie);
    }
}
