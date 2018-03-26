using System;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for clsOUTBoundCall
/// </summary>
public class clsOUTBoundCall
{
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    public clsOUTBoundCall()
    {
        //
        // TODO: Add constructor logic here	//
    }
    #region Properties and Variables
    public string Percentage
    { get; set; }
    public string FromDate
    { get; set; }
    public string ToDate
    { get; set; }
    public string MessageOut
    { get; set; }
    public string Type
    { get; set; }
    public int Reg
    { get; set; }
    public int ProDiv
    { get; set; }
    public int ReturnValue
    { get; set; }
    public DataSet ReturnDataset
    { get; set; }
    #endregion Properties and Variables


    #region Save Insert and Delete
    public bool GetClosedComplain()
    {
        try
        {


            DataSet dsTemp = new DataSet();
            DataSet dsClosedComplain = new DataSet();
            SqlParameter[] sqlParamS ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@FromDate",this.FromDate),
                                     new SqlParameter("@TODate",this.ToDate),
                                     new SqlParameter("@Type",this.Type),
                                     new SqlParameter("@Perc",int.Parse(this.Percentage)),
                                     new SqlParameter("@Reg",Convert.ToInt32(this.Reg)),
                                     new SqlParameter("@ProductDivision_Sno",Convert.ToInt32(this.ProDiv))   
                                  };
            sqlParamS[0].Direction = ParameterDirection.Output;
            sqlParamS[1].Direction = ParameterDirection.ReturnValue;

            if (this.FromDate == "")
            {
                sqlParamS[2].Value = null;
            }
            else
            {
                sqlParamS[2].Value = Convert.ToDateTime(this.FromDate);
            }
            if (this.ToDate == "")
            {
                sqlParamS[3].Value = null;
            }
            else
            {
                sqlParamS[3].Value = Convert.ToDateTime(this.ToDate);
            }
            dsTemp = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspOUTBoundCall", sqlParamS);
            this.ReturnValue = int.Parse(sqlParamS[1].Value.ToString());
            if (int.Parse(sqlParamS[1].Value.ToString()) == -1)
            {
                this.MessageOut = sqlParamS[0].Value.ToString();
                return false;
            }

            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                // Commented By Bhawesh 24 dec 2012
                //DeleteRecord();

                //for (int intCounter = 0; intCounter < dsTemp.Tables[0].Rows.Count - 1; intCounter++)
                //{
                //    SaveRecord(Convert.ToString(dsTemp.Tables[0].Rows[intCounter]["Complaint_RefNo"]),
                //               Convert.ToInt64(dsTemp.Tables[0].Rows[intCounter]["UniqueContact_No"].ToString()),
                //               Convert.ToString(dsTemp.Tables[0].Rows[intCounter]["Language_Code"]),
                //               Convert.ToInt64(dsTemp.Tables[0].Rows[intCounter]["CustomerId"].ToString()));
                //}

                this.ReturnDataset = dsTemp;

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
    private void DeleteRecord()
    {
        try
        {
            SqlParameter[] sqlParamD ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Type","Delete")
                                  };
            sqlParamD[0].Direction = ParameterDirection.Output;
            sqlParamD[1].Direction = ParameterDirection.ReturnValue;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOUTBoundCall", sqlParamD);
            this.ReturnValue = int.Parse(sqlParamD[1].Value.ToString());
            if (int.Parse(sqlParamD[1].Value.ToString()) == -1)
            {
                this.MessageOut = sqlParamD[0].Value.ToString();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void SaveRecord(string strCompRefNo, Int64 intUniqueContactNo, string strLanguageCode, Int64 intCustomerID)
    {
        try
        {


            SqlParameter[] sqlParamI ={
                                     new SqlParameter("@MessageOut",SqlDbType.NVarChar,1000),
                                     new SqlParameter("@Return_Value",SqlDbType.Int),
                                     new SqlParameter("@Complaint_RefNo",strCompRefNo),
                                     new SqlParameter("@UniqueContact_No",intUniqueContactNo),
                                     new SqlParameter("@Language_Code",strLanguageCode),
                                     new SqlParameter("@CustomerID",intCustomerID),   
                                     new SqlParameter("@Type","Insert")

                                  };
            sqlParamI[0].Direction = ParameterDirection.Output;
            sqlParamI[1].Direction = ParameterDirection.ReturnValue;
            objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspOUTBoundCall", sqlParamI);
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
            if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
            {
                this.MessageOut = sqlParamI[0].Value.ToString();

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion Save Insert and Delete

}
