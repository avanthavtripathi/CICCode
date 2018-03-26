using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections;
using System.Data;
using System.Web;
using System.Data.SqlClient;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
 
public class ABSCGWebService
{
	// Add [WebGet] attribute to use HTTP GET
	
	  string strRole;
      public ABSCGWebService()
        {
            #region Common
            if (HttpContext.Current.User.IsInRole("CGAdmin"))
            {
                strRole = "CGAdmin";
            }
            else if (HttpContext.Current.User.IsInRole("CCAdmin"))
            {
                strRole = "CCAdmin";
            }
            else if (HttpContext.Current.User.IsInRole("Super Admin"))
            {
                strRole = "Super Admin";
            }
            else
            {
                strRole = "";
            }
            #endregion Common
        }

       [OperationContract]
       public string[] GetCompletionList(string prefixText, int count)
        {
            ArrayList alst = new ArrayList();
            SqlDataAccessLayer sql = new SqlDataAccessLayer();
            DataSet ds = new DataSet();
            SqlParameter[] sqlParamG =
            {
            new SqlParameter("@Type","SELECT_USERS_BY_ROLES"),
            new SqlParameter("@RoleName",strRole)
            };
            ds = sql.ExecuteDataset(CommandType.StoredProcedure, "uspEditUserAndRoleMaster", sqlParamG);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["Name"].ToString().ToLower().Contains(prefixText.ToLower()) || dr["UserName"].ToString().ToLower().Contains(prefixText.ToLower()))
                    alst.Add(dr[0].ToString());
            }

            string[] intArr = Array.ConvertAll<object, string>(alst.ToArray(), new Converter<object, string>(Convert.ToString));
            return intArr;
        }


}
