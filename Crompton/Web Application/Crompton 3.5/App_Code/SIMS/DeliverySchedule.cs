using System;
using System.Data;
using System.Data.SqlClient;

//// <summary>
/// Description :This module is designed to make Entry for Spare Stock Transfer Log
/// Created Date: 02-02-2010
/// Created By: Suresh Kumar
/// </summary>
/// 
public class DeliverySchedule
{
    SIMSSqlDataAccessLayer objSql = new SIMSSqlDataAccessLayer();
    string strMsg;
    public DeliverySchedule()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Properties and Variables

    public int Part_Delivery_Id
    { get; set; }
    public string PDS_Transaction_No
    { get; set; }
    public string Transaction_No
    { get; set; }
    public string Ordered_Transaction_No
    { get; set; }
    public int Spare_Id
    { get; set; }
    public DateTime Part_Delivery_Date
    { get; set; }
    public int Quantity
    { get; set; }    
    public string EmpCode
    { get; set; }  
    public int ReturnValue
    { get; set; }
    public string Spare_Name
    { get; set; }  
    

    #endregion Properties and Variables

    #region Functions For save data

    public string InsertPartDelivery()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","INSERT_PART_DELIVERY_DATE"),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Ordered_Transaction_No",this.Ordered_Transaction_No),
            new SqlParameter("@Spare_Id",this.Spare_Id),
            new SqlParameter("@Part_Delivery_Date",this.Part_Delivery_Date),
            new SqlParameter("@Quantity",this.Quantity)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDeliverySchedule", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    public string DeletePartDelivery()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","DELETE_PART_DELIVERY"),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@Part_Delivery_Id",this.Part_Delivery_Id)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDeliverySchedule", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }

    public string UpdateFinalPartDelivery()
    {
        SqlParameter[] sqlParamI =
        {
            new SqlParameter("@MessageOut",SqlDbType.VarChar,200),
            new SqlParameter("@Return_Value",SqlDbType.Int),
            new SqlParameter("@Type","UPDATE_FINAL_PART_DELIVERY_DATE"),
            new SqlParameter("@EmpCode",this.EmpCode),
            new SqlParameter("@PDS_Transaction_No",this.PDS_Transaction_No),
            new SqlParameter("@Ordered_Transaction_No",this.Ordered_Transaction_No),
            new SqlParameter("@Part_Delivery_Id",this.Part_Delivery_Id)
        };

        sqlParamI[0].Direction = ParameterDirection.Output;
        sqlParamI[1].Direction = ParameterDirection.ReturnValue;
        objSql.ExecuteNonQuery(CommandType.StoredProcedure, "uspDeliverySchedule", sqlParamI);
        if (int.Parse(sqlParamI[1].Value.ToString()) == -1)
        {
            this.ReturnValue = int.Parse(sqlParamI[1].Value.ToString());
        }
        strMsg = sqlParamI[0].Value.ToString();
        sqlParamI = null;
        return strMsg;
    }
    #endregion Functions For save data


    #region For Reading Values from database

 
    public void SelectSpareDetail()
    {
        DataSet dsPD = new DataSet();
        SqlParameter[] sqlParam =
        {
            new SqlParameter("@Type","SELECT_SPARE_DETAIL"),
            new SqlParameter("@Ordered_Transaction_No",this.Ordered_Transaction_No)
        };
        dsPD = objSql.ExecuteDataset(CommandType.StoredProcedure, "uspDeliverySchedule", sqlParam);
        if (dsPD.Tables[0].Rows.Count > 0)
        {
            Spare_Id = Convert.ToInt32(dsPD.Tables[0].Rows[0]["Spare_Id"]);
            Spare_Name = Convert.ToString(dsPD.Tables[0].Rows[0]["Spare_Name"]);
            Quantity = Convert.ToInt32(dsPD.Tables[0].Rows[0]["Quantity"]);
        }
        dsPD = null;
        sqlParam = null;
    }

    public string GetFinalPDSTransactionNo()
    {
        SqlParameter sqlParam = new SqlParameter("@Type", "GET_FINAL_PDS_TRANSACTION_NO");
        string strReturn= Convert.ToString(objSql.ExecuteScalar(CommandType.StoredProcedure, "uspDeliverySchedule", sqlParam));
        sqlParam = null;
        return strReturn;
    }

    #endregion For Reading Values from database



}
