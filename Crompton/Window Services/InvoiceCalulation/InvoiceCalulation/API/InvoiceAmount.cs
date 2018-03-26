using System.Data;

namespace InvoiceCalulation.API
{

    public class InvoiceAmount
    {
        /** section one **/
        public string lblLWHU18 { get; set; }
        public string lblLWHU24 { get; set; }
        public string lblLWHU72 { get; set; }
        public string lblLWHU120 { get; set; }
        public string lblLWHU72120 { get; set; }
        public string lblLWHUgreater120 { get; set; }


        /**section two **/
        public string lblLWOHU18 { get; set; }
        public string lblLWOHU24 { get; set; }
        public string lblLWOHU72 { get; set; }
        public string lblLWOHU120 { get; set; }
        public string lblLWOHU72120 { get; set; }
        public string lblLWOHUgreater120 { get; set; }

        // sction three 
        public string lblOWHU18 { get; set; }
        public string lblOWHU24 { get; set; }
        public string lblOWHU72 { get; set; }
        public string lblOWHU120 { get; set; }
        public string lblOWHU72120 { get; set; }
        public string lblOWHUgreater120 { get; set; }
        // secton four 
        public string lblOWOHU18 { get; set; }
        public string lblOWOHU24 { get; set; }
        public string lblOWOHU72 { get; set; }
        public string lblOWOHU120 { get; set; }
        public string lblOWOHU72120 { get; set; }
        public string lblOWOHUgreater120 { get; set; }
        // sction five  
        public string lblLWHARep18 { get; set; }
        public string lblLWHARep24 { get; set; }
        public string lblLWHARep72 { get; set; }
        public string lblLWHARep120 { get; set; }
        public string lblLWHARep72120 { get; set; }
        public string lblLWHARepgreater120 { get; set; }

        // section six
        public string lblLWOHARep18 { get; set; }
        public string lblLWOHARep24 { get; set; }
        public string lblLWOHARep72 { get; set; }
        public string lblLWOHARep120 { get; set; }
        public string lblLWOHARep72120 { get; set; }
        public string lblLWOHARepgreater120 { get; set; }

        // seven 

        public string lblOWHARep18 { get; set; }
        public string lblOWHARep24 { get; set; }
        public string lblOWHARep72 { get; set; }
        public string lblOWHARep120 { get; set; }
        public string lblOWHARep72120 { get; set; }
        public string lblOWHARepgreater120 { get; set; }

        // eight section 
        public string lblOWOHARep18 { get; set; }
        public string lblOWOHARep24 { get; set; }
        public string lblOWOHARep72 { get; set; }
        public string lblOWOHARep120 { get; set; }
        public string lblOWOHARep72120 { get; set; }
        public string lblOWOHARepgreater120 { get; set; }
        // lblRepLoU 
        public string lblRepLoU { get; set; }
        public string lblRepOoA { get; set; }
        public string lblRepOoU { get; set; }
        public InvoiceAmount BindInvoiveAmount(int iscarryforward)
        {
            CommonClass obj = new CommonClass();
            DataTable ds = obj.PaymentMaster(iscarryforward).Tables[0];
            if (ds != null && ds.Rows.Count > 0)
            {

                /** section one **/
                lblLWHU18 = ds.Rows[0]["amount"].ToString();
                lblLWHU24 = ds.Rows[1]["amount"].ToString();
                lblLWHU72 = ds.Rows[2]["amount"].ToString();
                lblLWHU120 = ds.Rows[3]["amount"].ToString();
                lblLWHU72120 = ds.Rows[4]["amount"].ToString();
                lblLWHUgreater120 = ds.Rows[5]["amount"].ToString();
                /**section two **/
                lblLWOHU18 = ds.Rows[6]["amount"].ToString();
                lblLWOHU24 = ds.Rows[7]["amount"].ToString();
                lblLWOHU72 = ds.Rows[8]["amount"].ToString();
                lblLWOHU120 = ds.Rows[9]["amount"].ToString();
                lblLWOHU72120 = ds.Rows[10]["amount"].ToString();
                lblLWOHUgreater120 = ds.Rows[11]["amount"].ToString();
                // sction three 

                lblOWHU18 = ds.Rows[12]["amount"].ToString();
                lblOWHU24 = ds.Rows[13]["amount"].ToString();
                lblOWHU72 = ds.Rows[14]["amount"].ToString();
                lblOWHU120 = ds.Rows[15]["amount"].ToString();
                lblOWHU72120 = ds.Rows[16]["amount"].ToString();
                lblOWHUgreater120 = ds.Rows[17]["amount"].ToString();
                // secton four 
                lblOWOHU18 = ds.Rows[18]["amount"].ToString();
                lblOWOHU24 = ds.Rows[19]["amount"].ToString();
                lblOWOHU72 = ds.Rows[20]["amount"].ToString();
                lblOWOHU120 = ds.Rows[21]["amount"].ToString();
                lblOWOHU72120 = ds.Rows[22]["amount"].ToString();
                lblOWOHUgreater120 = ds.Rows[23]["amount"].ToString();
                // sction five  

                lblLWHARep18 = ds.Rows[24]["amount"].ToString();
                lblLWHARep24 = ds.Rows[25]["amount"].ToString();
                lblLWHARep72 = ds.Rows[26]["amount"].ToString();
                lblLWHARep120 = ds.Rows[27]["amount"].ToString();
                lblLWHARep72120 = ds.Rows[28]["amount"].ToString();
                lblLWHARepgreater120 = ds.Rows[29]["amount"].ToString();

                // section six
                lblLWOHARep18 = ds.Rows[30]["amount"].ToString();
                lblLWOHARep24 = ds.Rows[31]["amount"].ToString();
                lblLWOHARep72 = ds.Rows[32]["amount"].ToString();
                lblLWOHARep120 = ds.Rows[33]["amount"].ToString();
                lblLWOHARep72120 = ds.Rows[34]["amount"].ToString();
                lblLWOHARepgreater120 = ds.Rows[35]["amount"].ToString();

                // seven 
                lblOWHARep18 = ds.Rows[36]["amount"].ToString();
                lblOWHARep24 = ds.Rows[37]["amount"].ToString();
                lblOWHARep72 = ds.Rows[38]["amount"].ToString();
                lblOWHARep120 = ds.Rows[39]["amount"].ToString();
                lblOWHARep72120 = ds.Rows[40]["amount"].ToString();
                lblOWHARepgreater120 = ds.Rows[41]["amount"].ToString();


                // eight section 
                lblOWOHARep18 = ds.Rows[42]["amount"].ToString();
                lblOWOHARep24 = ds.Rows[43]["amount"].ToString();
                lblOWOHARep72 = ds.Rows[44]["amount"].ToString();
                lblOWOHARep120 = ds.Rows[45]["amount"].ToString();
                lblOWOHARep72120 = ds.Rows[46]["amount"].ToString();
                lblOWOHARepgreater120 = ds.Rows[47]["amount"].ToString();

                // lblRepLoU 
                lblRepLoU = ds.Rows[48]["amount"].ToString(); // change to instutional 
                lblRepOoA = ds.Rows[49]["amount"].ToString(); 

                //lblRepOoU

            }
            return this;
        }



    }

}
