using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace kursovayaConsole
{
    internal class Cinema
    {
        private string nameOfCinema;
        private Movies firstMovie;

        public Cinema(string nameOfCinema)
        {
            this.firstMovie = null;
            this.nameOfCinema = nameOfCinema;
        }

        public bool addFirst(string nameOfMovie, int movieDuration)
        {
            if (firstMovie == null)
            {
                Movies newMovie = new Movies(nameOfMovie, movieDuration);
                firstMovie = newMovie;
                newMovie.setNextMovie(firstMovie);
                return true;
            }
            return false;
        }

        public void addMovie(string nameOfMovie,int movieDuration, string searchFilm)
        {
            if (firstMovie == null)
            {
                addFirst(nameOfMovie,movieDuration);
            }
           else
            {
                Movies pTemp = findMovies(searchFilm);
                Movies newMovie = new Movies(nameOfMovie, movieDuration);
                newMovie.setNextMovie(pTemp.getNextMovie());
                pTemp.setNextMovie(newMovie);
            }
        }
        public void addMovie(Movies newMovie)
        {
            Movies temp = firstMovie;
            if (firstMovie == null)
            {
                addFirst(newMovie.getMovies(), newMovie.getMovieDuration());
            }
            else
            {
                while (temp.getNextMovie() != firstMovie)
                {
                    temp = temp.getNextMovie();
                }
                temp.setNextMovie(newMovie);
                newMovie.setNextMovie(firstMovie);
            }
        }
        public bool delMovie(Movies nameOfMovie)
        {
            Movies previousMovie;
            if(firstMovie ==null) return false;
            else
            {
                previousMovie = findPreviousMovie(nameOfMovie);
                Movies tempMovie = findMovies(nameOfMovie.getMovies());
                Movies movies = firstMovie;
                if (tempMovie == firstMovie)
                {
                    while (movies.getNextMovie() != firstMovie)
                    {
                        movies = movies.getNextMovie();
                    }
                    movies.setNextMovie(tempMovie.getNextMovie());
                    firstMovie = tempMovie.getNextMovie();
                }
                else
                {
                    previousMovie.setNextMovie(nameOfMovie.getNextMovie());
                    nameOfMovie = previousMovie;
                }
                return true;
            }
        }

        public Movies findMovies(string nameOfMovie)
        {
            Movies tempMovie = firstMovie;
            while (tempMovie != null)
            {
                if (tempMovie.getMovies() == nameOfMovie)
                {
                    return tempMovie;
                }
                else
                {
                    tempMovie = tempMovie.getNextMovie();
                }
            }
            return null;
        }

        public Movies findPreviousMovie(Movies nameOfMovie)
        {
            Movies tempMovie = firstMovie;
            while (tempMovie.getNextMovie() != null)
            {
                if (tempMovie.getNextMovie() == nameOfMovie)
                {
                    return tempMovie;
                }
                else
                {
                   tempMovie = tempMovie.getNextMovie();
                }
            }
            return null;
        }

        public int showNumberOfMovies()
        {
            Movies tempMovie = firstMovie;
            int numOfMovies = 0;
            if(tempMovie != null) { numOfMovies++; }
            else
            {
                return numOfMovies;
            }
            while (tempMovie.getNextMovie() != firstMovie)
            {
                numOfMovies++;
                tempMovie = tempMovie.getNextMovie();
            }
            return numOfMovies;
        }

        public string showInformationAboutCinema()
        {
            Movies tempMovie = firstMovie;
            string informationAboutCinema = "";
            if(firstMovie == null) {
                return "empty list";
            }
            while (tempMovie.getNextMovie() != firstMovie)
            {
                if(tempMovie.getNextMovie() == firstMovie)
                {
                    informationAboutCinema += (tempMovie.getMovieDuration() + " " + tempMovie.showInformationAboutFilm());
                }
                informationAboutCinema += (tempMovie.getMovieDuration() + " " + tempMovie.showInformationAboutFilm());
                tempMovie = tempMovie.getNextMovie();
            }
            if (tempMovie.getNextMovie() == firstMovie)
            {
                informationAboutCinema += (tempMovie.getMovieDuration() + " " + tempMovie.showInformationAboutFilm());
            }
            return nameOfCinema+ "\n" + informationAboutCinema + "\n";
            
        }
        public string saveStringFile()
        {
            string result="";
            string inf = "";
            string[] segment;
            Movies tempMovie = firstMovie;
            do
            {
                result += tempMovie.getMovieDuration().ToString() + '@';
                result += tempMovie.getMovies() + '@';
                inf = tempMovie.showInformationAboutFilm();
                segment = inf.Split('\n');
                if (segment[1] != "")
                {
                    segment[1].Replace(" ", "^");
                    result += segment[1].ToString();
                }
                result += "\n";
                tempMovie = tempMovie.getNextMovie();
            } while (tempMovie.getNextMovie() != firstMovie);

            result += tempMovie.getMovieDuration().ToString() + '@';
            result += tempMovie.getMovies() + '@';
            inf = tempMovie.showInformationAboutFilm();
            segment = inf.Split('\n');
            if (segment[1] != "")
            {
                string symb = segment[1].Replace(" ", "^");
                result += symb + "\n";
            }
            return result;
        }
    }
}
