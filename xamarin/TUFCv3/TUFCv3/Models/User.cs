﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System;
using System.Runtime.CompilerServices;

namespace TUFCv3.Models
{
    class User
    {
        // Private Properties
        private Int64 userId;
        private string email;
        private string password;
        private DateTime createDate;
        private string firstName;
        private string lastName;
        private string phoneMobile;
        private string phoneWork;
        private string phoneHome;
        private string number;
        private string street;
        private string city;
        private string state;
        private string zip;
        private string country;


        // PropertyChanged 
        //  An event handler, that updates bindings (including data on the device's screen)
        //  when a property changes.
        public event PropertyChangedEventHandler PropertyChanged;


        // Public properties
        //  When setting public properties, call the method SetProperty() to update the complementry private variable
        //  which in turn calls the method OnPropertyChanged(), to update xmal page views.   
        public Int64 UserId
        { 
            get { return userId; }
            set { SetProperty(ref userId, value); }    
        }

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }    

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
            
        public DateTime CreateDate
        {
            get { return createDate; }
            set { SetProperty(ref createDate, value); }
        }

        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        public string PhoneMobile
        {
            get { return phoneMobile; }
            set { SetProperty(ref phoneMobile, value); }
        }

        public string PhoneWork
        {
            get { return phoneWork; }
            set { SetProperty(ref phoneWork, value); }
        }

        public string PhoneHome
        {
            get { return phoneHome; }
            set { SetProperty(ref phoneHome, value); }
        }

        public string Number
        {
            get { return number; }
            set { SetProperty(ref number, value); }
        }

        public string Street
        {
            get { return street; }
            set { SetProperty(ref street, value); }
        }

        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        public string Zip
        {
            get { return zip; }
            set { SetProperty(ref zip, value); }
        }

        public string Country
        {
            get { return country; }
            set { SetProperty(ref country, value); }
        }


        // SetProperty()
        //  Update the private property, to match the public property
        //  then invoke the event handler PropertyChanged to update binding (including the screen). 
        // Arguments:
        //  'privateValue' is the private property's current value
        //  'newValue' is the public property's new value
        //  [CallerMemberName] 'propertyName' the calling public property's name.

        bool SetProperty<T>(ref T privateValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if(Object.Equals(privateValue, newValue))       // If the stored and new values are the same
                return false;                               //  return without making any changes.

            privateValue = newValue;                        // Update the private variable's value
                                                            //  to match the public property's new value

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      // Invoke the event handler
                                                                                            //  that updates property bindings

            return true;
        }
    }
}

