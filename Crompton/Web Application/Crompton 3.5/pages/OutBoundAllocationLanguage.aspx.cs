using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class pages_OutBoundAllocationLanguage : System.Web.UI.Page
{
    OutBoundCallingForSMS objOutBoundCallingSMS = new OutBoundCallingForSMS();

    public class LanguageLoad
    {
       public LanguageLoad(int LanguageID, string DisplayText)
        {
            this.languageid = LanguageID;
            this.displaytext = DisplayText;
        }
       int languageid;

       public int LanguageID
       {
           get { return languageid; }
           set { languageid = value; }
       }
       string displaytext;

       public string DisplayText
       {
           get { return displaytext; }
           set { displaytext = value; }
       }
    };



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ArrayList lst = new ArrayList();
            lst.Add(new LanguageLoad(17,"2-Hindi"));
            lst.Add(new LanguageLoad(28,"14-bangali"));
            lst.Add(new LanguageLoad(18,"11-Kannada"));
            lst.Add(new LanguageLoad(20,"4-Telegu"));
            lst.Add(new LanguageLoad(4,"5-English"));
            lst.Add(new LanguageLoad(25,"7-Punjabi"));
            lst.Add(new LanguageLoad(26, "10-Gujarati"));
            lst.Add(new LanguageLoad(27, "6-Malayalam"));

            

            



            rbtnlst.DataSource = lst;
            rbtnlst.DataBind();
          //  ViewState["OutCustList"] = objOutBoundCallingSMS.CustomerList;
        }
    }

    DataTable GetAllocationTable()
    {
        DataSet dsList = ViewState["OutCustList"] as DataSet;
        DataTable dt = new DataTable();
        dt.Columns.Add("SMS_ID");
        dt.Columns.Add("AgentID");
        DataRow dr ;
        foreach(DataRow drow in dsList.Tables[0].Rows)
        {
           dr = dt.NewRow();
           dr[0] = drow[0];
           dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        DataTable DTAllocation = GetAllocationTable();
        CheckBox chk;
        Label LblName;
        ArrayList alst = new ArrayList();
        foreach (DataListItem li in gvCommunication.Items)
        {
            if (li.ItemType == ListItemType.Item || li.ItemType == ListItemType.AlternatingItem)
            {
                chk = li.FindControl("chk") as CheckBox;
                LblName = li.FindControl("LblName") as Label;
                if (chk.Checked)
                {
                    alst.Add(LblName.ToolTip);            
                }
            
            }
        }
        int AgentCount = alst.Count;
        if(AgentCount <= DTAllocation.Rows.Count )
        {
            int MinAllocationcount =   DTAllocation.Rows.Count / AgentCount;
            int cntr = 0;
            int ArrayIndex = 0;
            foreach (DataRow dr in DTAllocation.Rows)
            {
                if (cntr == MinAllocationcount)
                {
                    ArrayIndex++;
                    cntr = 0;
                    if(ArrayIndex+1 > AgentCount)
                        ArrayIndex --;
                }
                dr[1] = alst[ArrayIndex].ToString();
                cntr++; 
            
            }
            objOutBoundCallingSMS.UpdateAllocation(DTAllocation);
            if (string.IsNullOrEmpty(objOutBoundCallingSMS.MessageOut))
            {
                lblMessage.Text = "Allocation completed successfully";
                var groupbyfilter = from row in DTAllocation.AsEnumerable()
                                    group row by row.Field<string>("AgentID") into grp
                                    select new
                                    {
                                        Agent = grp.Key,
                                        AllocatedCount = grp.Count()
                                    };
                GvAllocated.DataSource = groupbyfilter;
                GvAllocated.DataBind();
            }
            else
            {
                lblMessage.Text = "Some Internal error !! ";
            }
            
        }
        else
        {
            lblMessage.Text = "Agents are more then existing complaints.";
        
        }

       
   }

 

    protected void rbtnlst_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = rbtnlst.SelectedValue;
        if (rbtnlst.SelectedItem != null)
        {
            gvCommunication.DataSource = objOutBoundCallingSMS.GetCCAgents(str);
            gvCommunication.DataBind();
            if (gvCommunication.Items.Count > 0)
                btnAllocate.Enabled = true;
            else
                btnAllocate.Enabled = false;
        }

    }
}
