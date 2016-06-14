using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ToxCTS.Models;

namespace ToxCTS.DataServices
{
    public class MatrixDataService
    {
        private static string connectionString = Utility.GetMatrixConnectionString();
        private const string ErrorClassName = "MatrixDataService";


        //MY CODE
        private const string SelectChemicals = @"
        SELECT      UserTable3.UserText2 As ChemName, 
                    UserTable3.UserText5 As Storage,
                    UserTable3.UserText6 As Units, 
                    UserTable3.userText8 As CAS
        FROM UserTable3 
        ";



        private const string SelectSponsorContactsQuery = @"
            SELECT Submitters.SubmitterText1 AS SponsorCode,
		           Submitters.SubmitterCode AS ContactCode,
	               Submitters.SubmitterName AS SponsorName,1
	               ISNULL(Submitters.SubmitterText2, '') + ' ' +
	               ISNULL(Submitters.SubmitterText3, '') AS ContactName,
	               ISNULL(Submitters.SubmitterAddress1, '') + 
	               ISNULL(Submitters.SubmitterAddress2, '') AS PostalAddress,
	               Submitters.SubmitterAddress4 AS City,
	               Submitters.SubmitterAddress5 AS State,
	               Submitters.SubmitterPostCode,
	               Submitters.SubmitterAddress3 AS Country,
	               Submitters.SubmitterTelephone AS PhoneNumber,
	               Submitters.SubmitterFax AS Fax,
                   Submitters.SubmitterTelex AS Email
            FROM Submitters
            WHERE Submitters.SubmitterName LIKE @SponsorName
            AND Submitters.RecordStatus = 1
            AND Submitters.SubmitterClass = 'Contact'";

        private const string SelectSponsorName = @"
            SELECT Submitters.SubmitterName AS SponsorName
            FROM Submitters
            WHERE Submitters.SubmitterName LIKE @SponsorName
            AND Submitters.RecordStatus = 1
            AND Submitters.SubmitterClass = 'Contact'
            GROUP BY Submitters.SubmitterName";

        private const string ContactInfoByContactCode = @"
            SELECT Submitters.SubmitterText1 AS SponsorCode,
		           Submitters.SubmitterCode AS ContactCode,
	               Submitters.SubmitterName AS SponsorName,
	               ISNULL(Submitters.SubmitterText2, '') + ' ' +
	               ISNULL(Submitters.SubmitterText3, '') AS ContactName,
	               ISNULL(Submitters.SubmitterAddress1, '') + 
	               ISNULL(Submitters.SubmitterAddress2, '') AS PostalAddress,
	               Submitters.SubmitterAddress4 AS City,
	               Submitters.SubmitterAddress5 AS State,
	               Submitters.SubmitterPostCode,
	               Submitters.SubmitterAddress3 AS Country,
	               Submitters.SubmitterTelephone AS PhoneNumber,
	               Submitters.SubmitterFax AS Fax,
                   Submitters.SubmitterTelex AS Email
            FROM Submitters
            WHERE Submitters.SubmitterCode = @ContactCode
            AND Submitters.RecordStatus = 1
            AND Submitters.SubmitterClass = 'Contact'";



    public static List<Chemical> GetChemicals()
    {
        List<Chemical> chemicals = new List<Chemical>();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(SelectChemicals, connection ))
                {
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Chemical chem = CreateNewChemical(reader);
                        chemicals.Add(chem);
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            Debug.WriteLine(sqlEx.ToString());
        }
        return chemicals;
    }


    public static Chemical CreateNewChemical(SqlDataReader reader)
    {
        Chemical Chem = new Chemical();
        Chem.ChemName = reader[0].ToString().Trim();
        Chem.Storage = reader[1].ToString().Trim();
        Chem.ChemContainer.Unit = reader[2].ToString().Trim();
        Chem.CAS = reader[3].ToString().Trim();
        //do the hazards too
        return Chem;
    }

    public static Double myTryParse(string s) 
    {
        Double d = 0;
        try
        {

        }
        catch
        {

        }
        return d;
    }



    //    public static List<SponsorContact> GetSponsorContacts(string sponsorName)
    //    {
    //        List<SponsorContact> results = new List<SponsorContact>();
    //        sponsorName += "%";
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(connectionString))
    //            {
    //                connection.Open();
    //                using (SqlCommand command = new SqlCommand(SelectSponsorContactsQuery, connection))
    //                {
    //                    command.CommandType = CommandType.Text;
    //                    command.Parameters.Add("@SponsorName", SqlDbType.NVarChar).Value = sponsorName;

    //                    SqlDataReader reader = command.ExecuteReader();
    //                    while (reader.Read())
    //                    {
    //                        SponsorContact sponsor = CreateNewSponsor(reader);
    //                        results.Add(sponsor);
    //                    }
    //                }
    //            }
    //        }
    //        catch (SqlException sqlEx)
    //        {
    //            Debug.WriteLine(sqlEx.ToString());
    //            //ErrorHandler.CreateLogFile(ErrorFormName, "GetSponsorContacts", sqlEx);
    //        }
    //        return results;
    //    }

    //    public static IList GetSponsorNames(string searchString)
    //    {
    //        IList results = new ArrayList();
    //        searchString += "%";
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(connectionString))
    //            {
    //                connection.Open();
    //                using (SqlCommand command = new SqlCommand(SelectSponsorName, connection))
    //                {
    //                    command.CommandType = CommandType.Text;
    //                    command.Parameters.Add("@SponsorName", SqlDbType.NVarChar).Value = searchString;

    //                    SqlDataReader reader = command.ExecuteReader();
    //                    while (reader.Read())
    //                    {
    //                        string sponsorName = reader[0].ToString().Trim();
    //                        results.Add(sponsorName);
    //                    }
    //                }
    //            }
    //        }
    //        catch (SqlException sqlEx)
    //        {
    //            Debug.WriteLine(sqlEx.ToString());
    //            //ErrorHandler.CreateLogFile(ErrorFormName, "GetSponsorContacts", sqlEx);
    //        }
    //        return results;
    //    }

    //    public static SponsorContact GetSponsorByContactCode(string contactCode)
    //    {
    //        SponsorContact result = new SponsorContact();
    //        try
    //        {
    //            using (SqlConnection connection = new SqlConnection(connectionString))
    //            {
    //                connection.Open();
    //                using (SqlCommand command = new SqlCommand(ContactInfoByContactCode, connection))
    //                {
    //                    command.CommandType = CommandType.Text;
    //                    command.Parameters.Add("@ContactCode", SqlDbType.NVarChar).Value = contactCode;

    //                    SqlDataReader reader = command.ExecuteReader();
    //                    while (reader.Read())
    //                    {
    //                        result = CreateNewSponsor(reader);
    //                    }
    //                }
    //            }
    //        }
    //        catch (SqlException sqlEx)
    //        {
    //            Debug.WriteLine(sqlEx.ToString());
    //            //ErrorHandler.CreateLogFile(ErrorFormName, "GetSponsorByContactCode", sqlEx);
    //        }
    //        return result;
    //    }

    //    private static SponsorContact CreateNewSponsor(SqlDataReader reader)
    //    {
    //        SponsorContact sponsor = new SponsorContact();
    //        sponsor.SponsorCode = reader[0].ToString().Trim();
    //        sponsor.ContactCode = reader[1].ToString().Trim();
    //        sponsor.SponsorName = reader[2].ToString().Trim();
    //        sponsor.ContactName = reader[3].ToString().Trim();
    //        sponsor.Address = reader[4].ToString().Trim();
    //        sponsor.City = reader[5].ToString().Trim();
    //        sponsor.State = reader[6].ToString().Trim();
    //        sponsor.ZipCode = reader[7].ToString().Trim();
    //        sponsor.PhoneNumber = reader[9].ToString().Trim();
    //        sponsor.FaxNumber = reader[10].ToString().Trim();
    //        sponsor.Email = reader[11].ToString().Trim();
    //        return sponsor;
    //    }
    }
}