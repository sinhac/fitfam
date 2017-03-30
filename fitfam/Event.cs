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
        private string description;
        private List<string> tags;
        private string location;
        private DateTime time; //figure out recurring events
        private bool publicEvent;
        private User creator;
        private List<User> attending;

        public Event()
        {

        }

    }
}