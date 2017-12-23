using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregationService.Models
{
    public class ConnectionSettings
    {
        private static ConnectionSettings instance;                     // Ссылка на текущий объект

        public string ConnecnionString = "";

        //--- Конструктор класса (внутренний) -------------------------------------------------------------------------
        private ConnectionSettings()
        {

        }

        //--- Конструктор класса (внешний) ----------------------------------------------------------------------------
        public static ConnectionSettings getInstance()
        {
            if (instance == null)
                instance = new ConnectionSettings();
            return instance;
        }
    }
}
