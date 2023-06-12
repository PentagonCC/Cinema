using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace kursovayaConsole
{
    internal class Movies
    {
        private Movies nextMovie;
        private Session header;
        private string nameOfMovie;
        private int movieDuration;

        public Movies(string nameOfMovie, int movieDuration)
        {
            nextMovie = null;
            this.nameOfMovie = nameOfMovie;
            this.header = new Session(1, 1, 1, 1, 1, 1);
            header.setNextSession(null);
            header.setPreviousSession(null);
            this.movieDuration = movieDuration;
        }

        public string getMovies()
        {
            return nameOfMovie;
        }

        public void setMovies(string nameOfMovie)
        {
            this.nameOfMovie = nameOfMovie;
        }

        public void setNextMovie(Movies nextMovie)
        {
            this.nextMovie = nextMovie;
        }

        public Movies getNextMovie()
        {
            return nextMovie;
        }

        public void setMovieDuration(int movieDuration)
        {
            this.movieDuration = movieDuration;
        }

        public int getMovieDuration()
        {
            return movieDuration;
        }

        public void addFirstSession(int years, int months, int days, int hours, int minutes, int seconds = 0)
        {
            Session session = new Session(years, months, days, hours, minutes, seconds);
            if (header.getNextSession() == null)
            {
                header.setNextSession(session);
                session.setNextSession(null);
                session.setPreviousSession(header);
            }
        }

        public void addSession(int years, int months, int days, int hours, int minutes, int seconds = 0)
        {
            DateTime data = new DateTime(years, months, days, hours, minutes, seconds);
            Session session = new Session(years, months, days, hours, minutes, seconds);
            Session temp = header;

            if (header.getNextSession() == null)
            {
                addFirstSession(years, months, days, hours, minutes, seconds);
            }
            else
                while (temp != null)
                {
                    if (data <= temp.getDate() && data > temp.getPreviousSession().getDate())
                    {
                        session.setNextSession(temp);
                        session.setPreviousSession(temp.getPreviousSession());
                        Session prevT = temp.getPreviousSession();
                        prevT.setNextSession(session);
                        temp.setPreviousSession(session);
                        break;
                    }
                    else if (data > temp.getDate() && temp.getNextSession() == null)
                    {
                        temp.setNextSession(session);
                        session.setPreviousSession(temp.getPreviousSession());
                        session.setNextSession(null);
                        break;
                    }
                    else if (data > temp.getDate() && data <= temp.getNextSession().getDate())
                    {
                        session.setNextSession(temp.getNextSession());
                        session.setPreviousSession(temp);
                        Session nextT = temp.getNextSession();
                        nextT.setPreviousSession(session);
                        temp.setNextSession(session);
                        break;
                    }     
                    else
                    {
                        temp = temp.getNextSession();
                    }
                }
        }

        public Session findSession(DateTime data)
        {
            Session currSession = header.getNextSession();
            while (currSession != null)
            {
                if (currSession.getDate() == data)
                {
                    return currSession;
                }
                else
                {
                    currSession = currSession.getNextSession();
                }

            }
            return null;
        }
        public Session findPreviousSession(DateTime data)
        {
            Session currSession = header.getPreviousSession();
            while (currSession != null)
            {
                if (currSession.getDate() == data)
                {
                    return currSession;
                }
                else
                {
                   currSession= currSession.getPreviousSession();
                }

            }
            return null;
        }

        public bool dellSession(DateTime data)
        {
            Session temp = findSession(data);
            Session secCurrSession = temp.getPreviousSession();
            if (header.getNextSession() != null && header.getPreviousSession() != header)
            {
                if (temp.getNextSession() == null)
                {
                    secCurrSession.setNextSession(null);
                    return true;
                }
                else
                {
                    Session currSession = temp.getNextSession();
                    currSession.setPreviousSession(temp.getPreviousSession());
                    secCurrSession.setNextSession(temp.getNextSession());
                    temp = null;
                    return true;
                }
            }
            else
            return false;
        }

      public string showInformationAboutFilm()
       {
            Session currSession = header.getNextSession();
            string informationAboutMovie="";
            while (currSession != null)
            {
                informationAboutMovie += currSession.getDate()+ "|";
                currSession = currSession.getNextSession();
            }
            return (nameOfMovie +"\n"+ informationAboutMovie.ToString()+"\n");
       }

        public int showTotalDurationOfMovie()
        {
            Session currSession = header.getNextSession();
            int kol=0;
            while(currSession != null)
            {
               kol++;
               currSession = currSession.getNextSession();
            }
            return movieDuration * kol;
        }
    }
}
