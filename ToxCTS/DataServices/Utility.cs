using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ToxCTS.DataServices
{
    internal class Utility
    {
        //internal static string GetTPMConnectionString()
        //{
        //    string TPM = @"Toxikon.Web.ProtocolManager.Properties.Settings.TPMConnectionString";
        //    string TPMTest = @"Toxikon.Web.ProtocolManager.Properties.Settings.TPMTestConnectionString";
        //    string result = "";
        //    ConnectionStringSettings settings =
        //        ConfigurationManager.ConnectionStrings[TPM];
        //    if (settings != null)
        //    {
        //        result = settings.ConnectionString;
        //    }
        //    return result;
        //}

        internal static string GetMatrixConnectionString()
        {
            string LIMS = @"ToxCTS.Properties.Settings.LIMSConnectionString";
            string result = "";
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[LIMS];
            if (settings != null)
            {
                result = settings.ConnectionString;
            }
            return result;
        }
    }
}