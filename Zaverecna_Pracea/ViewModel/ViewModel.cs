using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Zaverecna_Pracea.Model;

using System.ComponentModel;

namespace Zaverecna_Pracea.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        //Commandy k buttonum a instance modelu
        public Command handleMoneyChange { get; private set; }
        public Command handleDaySimulation { get; private set; }
        public Command handleChangeLimit { get; private set; }
        Data model;
         
        
        
        //construktor
        public ViewModel()
        {
            model = new Data { Zustatek = 0,Limit=500 };
            handleMoneyChange = new Command(MoneyChange);
            handleDaySimulation = new Command(SimulateDay);
            


        }

        //int pro tracking utrácení
        private int dailyloss;

        //Metoda, která se stará o změnu zůstatku
        async private void MoneyChange(object param)
        {
            string str = null;

            if(param.ToString() == "+")
            {
                Balance += Change;
                dailybalance += Change;

                string a = DateTime.Now.AddDays(inc).AddMonths(incmonth).ToString();
                str += a + "Vydělané peníze " + Change +Environment.NewLine;
            }
            else
            {
                Balance -= Change;
                dailybalance -= Change;
                dailyloss += change;
                //Upozornění na denní limit utrácení
                if(dailyloss >= Limit || Change > Limit)
                {
                    await Application.Current.MainPage.DisplayAlert("ALERT", "Pozor na Limit!!!", "OK");
                }
                string a = DateTime.Now.AddDays(inc).AddMonths(incmonth).ToString();
                str += a + "Utracené peníze " + (-1*change) + Environment.NewLine;
            }
            

            
            
            History += str;
        }
        
        //kontrolní proměnné pro SimulateDay a MOneyChange
        private int inc = 0;
        private int incmonth = 0;
        private int dailybalance;

        //Metoda pro simulace dalšího dnu (viz datum)
        private void SimulateDay()
        {
            if(inc == 30)
            {
                inc = 1;
                incmonth++;

            }

            inc++;
            dailybalance = 0;
        }
        // Binding Properties
        public int Balance
        {
            get
            {
                return model.Zustatek;
            }
            set
            {
                model.Zustatek = value;OnPropertyChanged("Balance");
            }
        }
        
        public string History
        {
            get
            {

                return model.Historie;
            }
            set
            {
               model.Historie = value; OnPropertyChanged("History");
            }
            
        }
        public int Limit
        {
            get
            {
                return model.Limit;
            }
            set
            {
                model.Limit = value; OnPropertyChanged("Limit");
            }
        }

        private int change;

        public int Change
        {
            get { return change; }
            set { change = value; OnPropertyChanged("Change"); }
        }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
