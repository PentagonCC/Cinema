using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursovayaConsole
{
    internal class Session
    {
        private Session previousSession;
        private Session nextSession;
        private int years, months, days, hours, minutes, seconds=0;
        private DateTime data;

        public Session(int years, int months, int days, int hours, int minutes, int seconds)
        {
            nextSession = null;
            previousSession = null;
            this.years = years;
            this.months = months;  
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            this.data = new DateTime(years, months, days, hours, minutes, seconds);
        }
        
        public DateTime getDate()
        {
            return data;
        }
        public void setDate(int years, int months, int days, int hours, int minutes, int seconds)
        {
            this.years = years;
            this.months = months;
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds = 0;
            DateTime data = new DateTime(years, months, days, hours, minutes, seconds);
        }

        public void setNextSession(Session nextSession) 
        {
            this.nextSession = nextSession;
        }

        public Session getNextSession()
        {
            return nextSession;
        }

        public void setPreviousSession(Session previousSession)
        {
            this.previousSession = previousSession;
        }

        public Session getPreviousSession()
        {
            return previousSession;
        }
    }
}
