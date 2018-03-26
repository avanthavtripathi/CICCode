using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Net;

public partial class UC_UC_NewReport : System.Web.UI.UserControl
{

    // Job : NewReortJob : daily 10PM :Insert opencomplaints data in TblOpenComplaintReport (day wise at the end of day).
    
    SqlDataAccessLayer objSql = new SqlDataAccessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Loaddata();
        }
    }

    void Loaddata()
    {
        SqlParameter[] sqlParamSrh =
        {
            new SqlParameter("@MessageOut",SqlDbType.NVarChar,200),
        };
        sqlParamSrh[0].Direction = ParameterDirection.Output;

        DataSet ds = objSql.ExecuteDataset(CommandType.StoredProcedure, "NewReport",sqlParamSrh);
        string msg = Convert.ToString(sqlParamSrh[0].Value);
   
        //
        //GvNReport.DataSource = ds;
        //GvNReport.DataBind();
        //GvNReport1.Caption = ds.Tables[1].Rows[0]["Today"].ToString();
        //GvNReport1.Caption = "<span style='background-color:#CCFFCC;font-size:small'>" + GvNReport1.Caption + "</span>";
        //GvNReport1.DataSource = ds.Tables[1];
        //GvNReport1.DataBind();

        ArrayList alst = new ArrayList();
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if(ds.Tables[i].Rows.Count>0)
            alst.Add(i);
        }


        Rep.DataSource = alst;
        Rep.DataBind();

        //foreach (GridViewRow ctr in Rep.Rows)
        //{
        //    GridView gv = ctr.FindControl("GvNReport") as GridView;
        //    GridView gv1 = ctr.FindControl("GvNReport1") as GridView;
        //    if(gv != null)
        //    {
        //    gv.Caption = ds.Tables[0].Rows[0]["Today"].ToString();
        //    gv.Caption = "<span style='background-color:#CCFFCC;font-size:small'>" + gv.Caption + "</span>";
        //    gv.DataSource = ds;
        //    gv.DataBind();
        //    }
        //    if(gv1 != null)
        //    {
        //    gv1.Caption = ds.Tables[1].Rows[0]["Today"].ToString();
        //       gv1.Caption = "<span style='background-color:#CCFFCC;font-size:small'>" + gv1.Caption + "</span>";
        //    gv1.DataSource = ds.Tables[1];
        //    gv1.DataBind();
        //    }
        //}

        GridView gv;
        DataTable dt;
        foreach (DataListItem dl in Rep.Items)
        {
            gv = dl.FindControl("GvNReport") as GridView;           
            if (gv != null)
            {
                gv.DataSource = null;
                if(dl.ItemIndex > 6)
                return;

                dt = ds.Tables[dl.ItemIndex];

                if (dt.Rows.Count > 0)
                {
                    gv.DataSource = dt;
                    gv.DataBind();
                    gv.Caption = dt.Rows[0]["Today"].ToString();
                    gv.Caption = "<span style='background-color:#CCFFCC;font-size:small;width:100%;'>" + gv.Caption + "</span>";

                    if (dl.ItemIndex == 0)
                    {
                        gv.Columns[3].Visible = false;
                    }
                    else if (Request.QueryString["flag"] != null)
                    {
                        if (dl.ItemIndex == Rep.Items.Count - 1)
                        {
                            gv.Columns[0].Visible = false;
                            gv.Columns[1].Visible = false;
                            gv.Columns[2].Visible = false;
                            gv.Columns[4].Visible = false;
                            gv.Columns[5].Visible = false;
                            gv.Columns[6].Visible = false;
                        }
                        else
                        {
                            gv.Columns[0].Visible = false;
                            gv.Columns[1].Visible = false;
                            gv.Columns[2].Visible = false;
                        }

                    }
                    else
                    {
                        gv.Columns[0].Visible = false;
                        gv.Columns[1].Visible = false;
                        gv.Columns[2].Visible = false;
                    }
                }
              


            }

        }

    }
             

    // Note For export this page is excluded from Authorization in we.config
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            // Context.Response.ClearContent();
            Context.Response.ContentType = "application/ms-excel";
            Context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", "NEW_REPORT"));
            Context.Response.Charset = "";
            WebRequest htr = HttpWebRequest.Create(Request.Url.ToString());
            WebResponse htrRes = htr.GetResponse();
            System.IO.Stream ResStr = htrRes.GetResponseStream();
            // convert stream to string
            System.IO.StreamReader reader = new System.IO.StreamReader(ResStr);
            string text = reader.ReadToEnd();
            int ints = text.IndexOf("<table id=\"ctl00_MainConHolder_UC_NewReport1_Rep\"");
            string ss = text.Substring(ints);

           // UTF8Encoding utf8 = new UTF8Encoding();
           
            
            //WebClient myClient = new WebClient();
            //string myPageHTML = null;
            //byte[] requestHTML;
            //// Gets the url of the page
            //string currentPageUrl = Request.Url.ToString();
            //UTF8Encoding utf8 = new UTF8Encoding();
            //requestHTML = myClient.DownloadData(currentPageUrl);
            //myPageHTML = utf8.GetString(requestHTML);
            //int ints = myPageHTML.IndexOf("<table id=\"ctl00_MainConHolder_UC_NewReport1_Rep\"");
            //string ss = myPageHTML.Substring(ints);
            Context.Response.Write(ss);
        }
        catch (Exception ex)
        {
            CommonClass.WriteErrorErrFile(Request.RawUrl.ToString(), ex.StackTrace.ToString() + "-->" + ex.Message.ToString());
        }
    }




}
