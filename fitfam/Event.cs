using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace fitfam
{
   
    class Event
    {
        private string eventName;
        public string EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private List<string> tags;
        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        private DateTime startTime; //figure out recurring events
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        private bool publicEvent;
        public bool PublicEvent
        {
            get { return publicEvent; }
            set { publicEvent = value; }
        }
        private User creator;
        public User Creator
        {
            get { return creator; }
        }
        private List<User> attending;
        public List<User> Attending
        {
            get { return attending; }
        }

        public Event()
        {

        }

    }
}