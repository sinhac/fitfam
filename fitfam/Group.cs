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
    class Group
    {
        private string groupName;
        private string description;
        private List<string> tags;
        private string pic;
        private Dictionary<User, bool> members; //true means they are an admin
        private List<Event> eventList;


        public Group()
        {

        }
    }
}