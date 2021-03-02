using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nir2
{
    class PTModel
    {
        private Dictionary<string, string> FilenameData = new Dictionary<string, string>(); //пути к файлам с данными для расчета 
        private Dictionary<string, double[,]> Data { get; set; } = new Dictionary<string, double[,]>();   //данные для расчета

        #region inputParam
        private double FlowHighSteam { get; set; }                          //Расход пара высокого давления
        private double PressureHighSteam { get; set; }                      //Давление пара высокого давления
        private double TemperatureHighSteam { get; set; }                   //Температура пара высокого давления
        private double FlowLowSteam { get; set; }                           //Расход пара низкого давления
        private double PressureLowSteam { get; set; }                       //Давление пара низкого давления
        private double TemperatureLowSteam { get; set; }                    //Температура пара низкого давления
        private double PressureCondenser { get; set; }                      //Давление пара в конденсаторе
        #endregion

        #region outputParam
        public double GrossPower { get { return calculateGrossPower(); } }                     //Электрическая мощность ПТ брутто
        public double ConsumptionSteam { get { return calculateConsumptionSteam(); } }         //Расход пара в конденсаторе
        #endregion

        #region controlParam
        private double DefFlowHighSteam { get; } = 63.4;                       //Расход пара высокого давления
        private double DefPressureHighSteam { get; } = 7.51;                   //Давление пара высокого давления
        private double DefTemperatureHighSteam { get; } = 501;                 //Температура пара высокого давления
        private double DefFlowLowSteam { get; } = 15.9;                        //Расход пара низкого давления
        private double DefPressureLowSteam { get; } = 206;                     //Давление пара низкого давления
        private double DefTemperatureLowSteam { get; } = 0.542;                //Температура пара низкого давления
        private double DefPressureCondenser { get; } = 0.1275;                 //Давление пара в конденсаторе
        #endregion

        public PTModel()
        {
            string filenameBase = AppDomain.CurrentDomain.BaseDirectory;

            FilenameData.Add("G(dPin)", filenameBase + @"DataFiles\G(dPin).txt");
            FilenameData.Add("G(Ngtu)", filenameBase + @"DataFiles\G(Ngtu).txt");
            FilenameData.Add("dPinTrackBar", filenameBase + @"DataFiles\N(dPin).txt");
            FilenameData.Add("TnvTrackBar", filenameBase + @"DataFiles\N(Tnv).txt");
            FilenameData.Add("PnvTrackBar", filenameBase + @"DataFiles\N,G(Pnv).txt");
            FilenameData.Add("dPoutTrackBar", filenameBase + @"DataFiles\N,Nu(dPout).txt");
            FilenameData.Add("Nu(dPin)", filenameBase + @"DataFiles\Nu(dPin).txt");
            FilenameData.Add("NgtuTrackBar", filenameBase + @"DataFiles\Nu(Ngtu).txt");
            FilenameData.Add("Nu(Tnv)", filenameBase + @"DataFiles\Nu(Tnv).txt");
            FilenameData.Add("T(dPin)", filenameBase + @"DataFiles\T(dPin).txt");
            FilenameData.Add("T(dPout)", filenameBase + @"DataFiles\T(dPout).txt");
            FilenameData.Add("T(Ngtu)", filenameBase + @"DataFiles\T(Ngtu).txt");
            FilenameData.Add("T(Tnv)", filenameBase + @"DataFiles\T(Tnv).txt");
            FilenameData.Add("tTrackBar", filenameBase + @"DataFiles\t.txt");

            foreach (var x in FilenameData)
                Data.Add(x.Key, FileManager.ReadFromFile(x.Value));
        }

        public void updateParam(string name, int value)
        {
            double newValue = Data[name][0, 0] + value * (Data[name][0, Data[name].GetLength(1) - 1] - Data[name][0, 0]) / 100;
            switch (name)
            {
                case "GvdTrackBar":
                    FlowHighSteam = newValue;
                    break;
                case "TvdTrackBar":
                    PressureHighSteam = newValue;
                    break;
                case "PvdTrackBar":
                    TemperatureHighSteam = newValue;
                    break;
                case "GndTrackBar":
                    FlowLowSteam = newValue;
                    break;
                case "TndTrackBar":
                    PressureLowSteam = newValue;
                    break;
                case "PndTrackBar":
                    TemperatureLowSteam = newValue;
                    break;
                case "PkTrackBar":
                    PressureCondenser = newValue;
                    break;
            }

        }

        private double calculateGrossPower()
        {
            return 1.0;
            /*return DefN * Interpolation(OperatingTime, Data["tTrackBar"]) * Interpolation(StrainGTU, Data["NgtuTrackBar"])
                * Interpolation(OutdoorAirTemperature, Data["TnvTrackBar"]) * Interpolation(OutdoorAirPressure, Data["PnvTrackBar"])
                * Interpolation(PressureLossIn, Data["dPinTrackBar"]) * Interpolation(PressureLossOut, Data["dPoutTrackBar"]);*/
        }

        private double calculateConsumptionSteam()
        {
            return 1.0;
            /*
            return DefNu * Interpolation(OperatingTime, Data["tTrackBar"]) * Interpolation(StrainGTU, Data["NgtuTrackBar"])
                * Interpolation(OutdoorAirTemperature, Data["TnvTrackBar"]) * Interpolation(PressureLossIn, Data["dPinTrackBar"])
                * Interpolation(PressureLossOut, Data["dPoutTrackBar"]);*/
        }

        private double Interpolation(double x, double[,] arr)
        {
            int i = 0;
            while (x > arr[0, i] && i < arr.GetLength(1) - 2) i++;
            return (arr[1, i + 1] - arr[1, i]) / (arr[0, i + 1] - arr[0, i]) * (x - arr[0, i + 1]) + arr[1, i];
        }
    }
}
