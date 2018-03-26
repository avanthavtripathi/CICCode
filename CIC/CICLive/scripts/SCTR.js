        window.onload = function() {
            Counter = 0;
        }
        function SetCounter() {
            Counter = 0;
        }

        function ChildClick(CheckBox) {
            var HeaderCheckBox = document.getElementById("ctl00_MainConHolder_gvFresh_ctl01_chkHeader");
            TotalChkBx = document.getElementById("ctl00_MainConHolder_gvFresh").rows.length;

            TotalChkBx = TotalChkBx - 2;
            if (CheckBox.checked && Counter < TotalChkBx) {
                Counter++;
            }
            else if (CheckBox.checked && Counter == 0)
            { Counter++; }
            else if (Counter > 0)
            { Counter--; }
            if (Counter < TotalChkBx) {
                HeaderCheckBox.checked = false;
            }
            else if (Counter == TotalChkBx) {
                HeaderCheckBox.checked = true;
            }
        }

        function SelectAllCheckboxes(spanChk) {
            TotalChkBx = document.getElementById("ctl00_MainConHolder_gvFresh").rows.length;
            TotalChkBx = TotalChkBx - 1;
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {//elm[i].click();
                if (elm[i].checked != xState)
                    elm[i].click();
            }
        }

        function CheckRow() {
            alert('Defect Already Exists');
            return false;
        }

        //For Pumps Sr. No. validation
        function fnOpendefectFHPMotor() {
            var varComplaintNo = document.getElementById('ctl00_MainConHolder_HDComp').value;
            var strUrl = '../Pages/Windingdefectforfhpmotor.aspx?SplitComplaintRefNo=' + varComplaintNo;
            window.open(strUrl, 'History', 'height=550,width=900,left=20,scrollbars=1,top=30,Location=0');
        }

        function fnOpendefectPump() {
            var varComplaintNo = document.getElementById('ctl00_MainConHolder_HDComp').value;
            var strUrl = '../Pages/Windingdefect.aspx?Comp_No=' + varComplaintNo;
            window.open(strUrl, 'History', 'height=550,width=850,left=20,scrollbars=1,top=30,Location=0');
        }

        function isChar(Data) {
            varChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var isChar = true;
            var index = 0;
            while ((index < Data.length) && (isChar)) {
                isChar = (varChars.indexOf(Data.charAt(index)) != -1);

                index++;
            }
            if (!isChar) {
                alert("First 4 Characters of Product Serial Number Should be Alphabets. ");
                document.getElementById('ctl00_MainConHolder_txtProductRefNo').focus();
                return false;
            }

            return true;

        }

        function ValidateBatchCodeWithNewLogic(oSrc, args) // BP 28 Jan 14 (live)
        {
            debugger;
            var inputProdSer = document.getElementById('ctl00_MainConHolder_txtProductRefNo').value.toUpperCase();
            var ProdDivSrNo = document.getElementById('ctl00_MainConHolder_hdnProductDvNo').value;
            var BatchNo = $get('ctl00_MainConHolder_txtBatchNo');
            var txtMfgUnit = $get('ctl00_MainConHolder_txtMfgUnit');
            txtMfgUnit.value = '';

            // lblMfgUnit
            var inputBatchYear = new String("0");
            var inputBatchMonth;
            var inputC3code;
            inputBatchYear = inputProdSer.substring(0, 1);
            inputBatchMonth = inputProdSer.substring(1, 2);
            inputC3code = inputProdSer.substring(2, 5);
            var ddlPline = $get('ctl00_MainConHolder_ddlProductLine');
            var inputPline = ddlPline.options[ddlPline.selectedIndex].value

            if (ddlPline.selectedIndex > 0) {

                var AllValidCodes = document.getElementById('ctl00_MainConHolder_HdnValid3Chars').value;
                var arrayValidCode = AllValidCodes.split('|');
                var bolValidCode = new Boolean(false);
                for (vcode in arrayValidCode) {
                    var CodeToCompare = arrayValidCode[vcode];
                    if (CodeToCompare.substring(0, 3) == inputC3code) {
                        var arrayInputCode = CodeToCompare.split('_');
                        if (arrayInputCode[1] != inputPline) {
                            BatchNo.value = "";
                            bolValidCode = false;
                        }
                        else {
                            BatchNo.value = inputProdSer.substring(0, 2);
                            txtMfgUnit.value = arrayInputCode[2];
                            bolValidCode = true;
                            break;
                        }
                    }
                }
                if (bolValidCode == false) {
                    args.IsValid = false;
                    alert("Invalid ProdLine / ProdSrNo");
                    document.getElementById('ctl00_MainConHolder_txtBatchNo').value = "Not a Vaild Serial No";
                }
            }
            else {
                alert("First Select ProductLine");
                ddlPline.focus();
                args.IsValid = false;
            }
        }

        function validateBatchCode(oSrc, args) {

            var inputBatchYear;
            var inputBatchMonth;
            var inputProdSer = "";
            var ProdDivSrNo = "";
            var ddlCat;
            var MgfUnitValidation;
            var txtMfgUnit;
            if (!(oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0)) {
                inputProdSer = document.getElementById('ctl00_MainConHolder_txtProductRefNo').value.toUpperCase();
                ProdDivSrNo = document.getElementById('ctl00_MainConHolder_hdnProductDvNo').value;
                ddlCat = $get('ctl00_MainConHolder_ddlMfgUnit');
                MgfUnitValidation = $get('ctl00_MainConHolder_RequiredFieldValidatorddlMfgUnit');
                txtMfgUnit = $get('ctl00_MainConHolder_txtMfgUnit');
            }
            else {
                inputProdSer = document.getElementById('ctl00_MainConHolder_txtDemoPSerialNo').value.toUpperCase();
                ProdDivSrNo = document.getElementById('ctl00_MainConHolder_hdnDemoProductDiv').value;
            }



            inputBatchYear = inputProdSer.substring(0, 1);
            inputBatchMonth = inputProdSer.substring(1, 2);
            // Second condition in if is added by Ashok for applying new logic to pump division
            if ((((inputBatchYear == 'M' && inputBatchMonth >= 'L') || inputBatchYear > 'M') && (ProdDivSrNo == '13')) ||
            (((inputBatchYear == 'N' && inputBatchMonth >= 'I') || inputBatchYear > 'N') && (ProdDivSrNo == '16'))) {
                document.getElementById('ctl00_MainConHolder_txtProductRefNo').value = inputProdSer.replace(/\s+/g, "");
                while (ddlCat.options.length > 0) {
                    ddlCat.remove(0);
                }
                ValidateBatchCodeWithNewLogic(oSrc, args);
                txtMfgUnit.style.display = 'block';
                ddlCat.style.display = 'none';
                MgfUnitValidation.disabled = true;
            }

            else {
                var varBatchYear;
                if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                    varBatchYear = (document.getElementById('ctl00_MainConHolder_hdntxtDemoPSerialNo1').value);
                }
                else {
                    varBatchYear = (document.getElementById('ctl00_MainConHolder_hdnValidBatch').value);
                }



                var varBatchMonth = 'A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R';
                var currYear = '2001|2002|2003|2004|2005|2006|2007|2008|2009|2010|2011|2012|2013|2014|2015|2016|2017|2018';

                var my_month = new Date();
                var month_name = '1|2|3|4|5|6|7|8|9|10|11|12';
                var aaraymonth = month_name.split('|');
                var arrayBatchYear = varBatchYear.split('|');
                var arrayBatchMonth = varBatchMonth.split('|');
                var arrayyear = currYear.split('|');
                var bolBatchYearExit = new Boolean(false);
                var bolBatch = new Boolean(false);
                var bolMFG = new Boolean(false);
                var inputProdSer1 = "";

                if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                    inputProdSer1 = $get('ctl00_MainConHolder_txtDemoPSerialNo').innerHTML.toUpperCase(); // For Demo
                }
                else {
                    inputProdSer1 = document.getElementById('ctl00_MainConHolder_txtProductRefNo').value.toUpperCase();
                }

                var inputProductMfg;
                var UserInputYear;

                var Currentyear;
                var productDiv;
                if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                    productDiv = $get('ctl00_MainConHolder_lblDemoProductDivision').innerHTML.toUpperCase(); // For Demo
                }
                else {
                    productDiv = $get('ctl00_MainConHolder_lblUnit').innerHTML.toUpperCase();
                }

                ///Product Sr. No. Validation for Pumps
                var strString = inputProdSer
                var strValidChars = "0123456789";
                var strChar;
                var strChar1;
                var strSpace;
                var strNum;
                var blnResult = true;
                if (productDiv == "PUMPS") {
                    strChar = strString.substring(0, 4);
                    strSpace = strString.substring(4, 5);
                    strNum = strString.substring(5, 11);


                    if (isChar(strChar) == false) {
                        return false;
                    }

                    if (strSpace != " ") {
                        alert("Space Required at 5th Position of Product Serial Number ");
                        return false;
                    }
                    if (strNum.length == 1) {
                        strNum = "00000" + strNum;
                    }
                    if (strNum.length == 2) {
                        strNum = "0000" + strNum;
                    }
                    if (strNum.length == 3) {
                        strNum = "000" + strNum;
                    }
                    if (strNum.length == 4) {
                        strNum = "00" + strNum;
                    }
                    if (strNum.length == 5) {
                        strNum = "0" + strNum;
                    }

                    for (i = 0; i < strNum.length; i++) {
                        strChar1 = strNum.charAt(i);
                        if (strValidChars.indexOf(strChar1) == -1) {
                            blnResult = false;

                        }
                    }
                    if (blnResult == false) {
                        alert("Last 6 Characters of Product Serial Number Should be Numbers Only");
                        return false;
                    }
                    else {
                        document.getElementById('ctl00_MainConHolder_txtProductRefNo').value = strChar.toUpperCase() + strSpace + strNum;
                        //return true;
                    }
                }
                //Pumps Porduct Sr. No. Validation Ends Here
                if (productDiv == 'APPLIANCES' || productDiv == 'APPLIANCE') {
                    inputProdSer = inputProdSer.substring(2, 4);
                    inputBatchYear = inputProdSer.substring(0, 1);
                    inputBatchMonth = inputProdSer.substring(1, 2);
                }
                else {
                    inputProdSer = inputProdSer.substring(0, 2);
                    inputBatchYear = inputProdSer.substring(0, 1);
                    inputBatchMonth = inputProdSer.substring(1, 2);
                }


                // Variable for Current index of year  
                var yearIndex = 0;
                var batchyearIndex = 0;
                var monthIndex = 0;
                var batchmonthIndex = 0;

                for (var i = 0; i < arrayBatchYear.length; i++) {
                    if (arrayBatchYear[i] == inputBatchYear) {
                        UserInputYear = arrayBatchYear[i];
                        batchyearIndex = i;
                        break;
                    }
                }


                //Month 
                for (var i = 0; i < arrayBatchMonth.length; i++) {
                    if (arrayBatchMonth[i] == inputBatchMonth) {
                        batchmonthIndex = i;
                        break;
                    }
                }

                for (var i = 0; i < arrayBatchMonth.length; i++) {
                    if (arrayBatchMonth[i] == my_month.getMonth()) {
                        monthIndex = i;
                        break;
                    }
                }

                // loop for finding out current index of year
                for (var i = 0; i < arrayyear.length; i++) {
                    if (arrayyear[i] == my_month.getFullYear()) {
                        yearIndex = i;
                        Currentyear = arrayBatchMonth[i];
                        break;
                    }
                }
                // Comparing the current year index with input by user
                if (batchyearIndex > yearIndex) {
                    bolBatch = true;
                }
                else {
                    bolBatch = false;
                }

                if (bolBatch == false) {
                    if (inputBatchYear == Currentyear) {
                        if (batchmonthIndex > my_month.getMonth()) {
                            bolBatch = true;
                        }
                        else {
                            bolBatch = false;
                        }
                    }

                    else {
                        bolBatch = false;
                    }

                }

                if (bolBatch == false) {
                    // Changes for mfg unit
                    if (productDiv.toUpperCase() == 'Fans'.toUpperCase()) {
                        inputProductMfg = inputProdSer1.substring(2, 3);
                        if (inputProductMfg != '') {
                            bolMFG = getMFGunit(inputProductMfg);
                        }
                    }
                }

                if (bolMFG == false) {

                    if (bolBatch == false) {

                        for (BatchYear in arrayBatchYear) {
                            if (arrayBatchYear[BatchYear] == inputBatchYear) {

                                bolBatchYearExit = true;
                                break;
                            }
                        }
                    }
                    else {
                        args.IsValid = false;
                        if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                            productDiv = $get('ctl00_MainConHolder_txtDemoBatchNO').value = "Not a Vaild Serial No"; // For Demo
                        }
                        else {
                            document.getElementById('ctl00_MainConHolder_txtBatchNo').value = "Not a Vaild Serial No";
                        }
                    }

                    if (bolBatchYearExit == true) {
                        for (BatchMonth in arrayBatchMonth) {
                            if (arrayBatchMonth[BatchMonth] == inputBatchMonth) {
                                args.IsValid = true;
                                if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                                    productDiv = $get('ctl00_MainConHolder_txtDemoBatchNO').value = inputProdSer; // For Demo
                                }
                                else {
                                    document.getElementById('ctl00_MainConHolder_txtBatchNo').value = inputProdSer;
                                }
                                break;
                            }
                            else {
                                args.IsValid = false;
                                if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                                    productDiv = $get('ctl00_MainConHolder_txtDemoBatchNO').value = "Not a Vaild Serial No"; // For Demo
                                }
                                else {
                                    document.getElementById('ctl00_MainConHolder_txtBatchNo').value = "Not a Vaild Serial No";
                                }
                            }

                        }
                    }
                    else {
                        args.IsValid = false
                        if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                            productDiv = $get('ctl00_MainConHolder_txtDemoBatchNO').value = "Not a Vaild Serial No"; // For Demo
                        }
                        else {
                            document.getElementById('ctl00_MainConHolder_txtBatchNo').value = "Not a Vaild Serial No";
                        }
                    }
                }

                else {
                    args.IsValid = false
                    if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoPSerialNo") >= 0) {
                        productDiv = $get('ctl00_MainConHolder_txtDemoBatchNO').value = "put correct mfg unit."; // For Demo
                    }
                    else {
                        document.getElementById('ctl00_MainConHolder_txtBatchNo').value = "put correct mfg unit.";
                    }
                }
            }
        }

        function ClearBatchNo() {
            $get('ctl00_MainConHolder_txtBatchNo').value = "";
        }
        function getMFGunit(strmfg) {
            debugger;
            var ddlCat;
            var varMFG;
            if (document.getElementById("ctl00_MainConHolder_hdnTag") == null) {
                ddlCat = $get('ctl00_MainConHolder_ddlMfgUnit');
                varMFG = (document.getElementById('ctl00_MainConHolder_hdnMfgUnit').value);
            }


            var arrayMFG = varMFG.split('|');
            var bolBatch = new Boolean(true);



            for (i = 0; i < arrayMFG.length; i++) {
                if (arrayMFG[i] == strmfg) {
                    ddlCat.selectedIndex = i;
                    bolBatch = false;
                    break;
                }
            }

            if (arrayMFG.length == 2) {
                if (arrayMFG[0] == 0 && arrayMFG[1] == "") {
                    bolBatch = false;
                }

            }


            if (bolBatch == true) {
                ddlCat.selectedIndex = 0;
            }

            return bolBatch;
        }

        function CheckDefect() {

            var strValidChars = "0123456789";
            var strChar;
            var blnResult = true;
            var ddlCat = $get('ctl00_MainConHolder_ddlDefectCat').value;
            var ddlDef = $get('ctl00_MainConHolder_ddlDefect').value;
            var txtQty = $get('ctl00_MainConHolder_txtDefectQty');
            var strString = txtQty.value;
            if (ddlCat == 0) {
                alert("Select Defect Category");
                return false;
            }
            else if (ddlDef == 0) {
                alert("Select Defect");
                return false;
            }
            else if (txtQty.value == '') {
                alert("Enter Defect Quantity");
                return false;
            }
            else if (txtQty.value != '') {
                for (i = 0; i < strString.length && blnResult == true; i++) {
                    strChar = strString.charAt(i);
                    if (strValidChars.indexOf(strChar) == -1) {
                        blnResult = false;
                        alert("Enter Number Only in Quantity");
                    }
                }
                return blnResult;
            }

            else {
                return true;
            }
        }

        function validateDefectQty(oSrc, args) {

            var txtQty = $get('ctl00_MainConHolder_txtDefectQty');
            var strString = txtQty.value;
            var strValidChars = "0123456789";
            var strChar;

            args.IsValid = true;
            //  test strString consists of valid characters listed above
            for (i = 0; i < strString.length && args.IsValid == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    args.IsValid = false;

                }
                else {
                    args.IsValid = true;
                }
            }

        }

        function IsNumeric()
        //  check for valid numeric strings	
        {
            var txtQty = $get('ctl00_MainConHolder_txtDefectQty');
            var strString = txtQty.value;
            var strValidChars = "0123456789";
            var strChar;
            var blnResult = true;

            if (strString.length == 0) {
                alert("Enter Defect Quantity");
                return false;
            }

            //  test strString consists of valid characters listed above
            for (i = 0; i < strString.length && blnResult == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    blnResult = false;
                    alert("Enter Number Only in Quantity");
                }
            }
            return blnResult;
        }

        function validateDate(oSrc, args) {
            var varActiondate;
            if (oSrc.id.indexOf("ctl00_MainConHolder_txtDemoFirDate") >= 0) {
                varActiondate = (document.getElementById('ctl00_MainConHolder_txtDemoFirDate').value);
            }
            else {
                varActiondate = (document.getElementById('ctl00_MainConHolder_txtFirDate').value);
            }
            var varServerDate = (document.getElementById('ctl00_MainConHolder_hdnGlobalDate').value);
            var arrayAction = varActiondate.split('/');
            var arrayServer = varServerDate.split('/');
            var actionDate = new Date();
            var serverDate = new Date();
            actionDate.setFullYear(arrayAction[2], (arrayAction[0] - 1), arrayAction[1]);
            actionDate.setHours(0, 0, 59, 0);
            serverDate.setFullYear(arrayServer[2], (arrayServer[0] - 1), arrayServer[1]);
            serverDate.setHours(0, 0, 59, 0);
            serverDate.setDate(serverDate.getDate() - 2);
            if (actionDate < serverDate) {
                args.IsValid = false
            }
            else {
                args.IsValid = true
            }

        }

        function validateAppointmentDatedt(oSrc, args) {
            var varActiondate = (document.getElementById('ctl00_MainConHolder_txtAppointMentDate').value);
            var varServerDate = (document.getElementById('ctl00_MainConHolder_hdnGlobalDate').value);
            var arrayAction = varActiondate.split('/');
            var arrayServer = varServerDate.split('/');
            var actionDate = new Date();
            var serverDate = new Date();
            actionDate.setFullYear(arrayAction[2], (arrayAction[0] - 1), arrayAction[1]);
            actionDate.setHours(0, 0, 59, 0);
            serverDate.setFullYear(arrayServer[2], (arrayServer[0] - 1), arrayServer[1]);
            serverDate.setHours(0, 0, 59, 0);
            if (actionDate < serverDate) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function validateInvoiceDate(oSrc, args) {

            var varInvoicedate = (document.getElementById('ctl00_MainConHolder_txtInvoiceDate').value);
            var varServerDate = (document.getElementById('ctl00_MainConHolder_hdnGlobalDate').value);
            var arrayInvoice = varInvoicedate.split('/');
            var arrayServer = varServerDate.split('/');
            var InvoiceDate = new Date();
            var serverDate = new Date();
            InvoiceDate.setFullYear(arrayInvoice[2], (arrayInvoice[0] - 1), arrayInvoice[1]);
            InvoiceDate.setHours(0, 0, 59, 0);
            serverDate.setFullYear(arrayServer[2], (arrayServer[0] - 1), arrayServer[1]);
            serverDate.setHours(0, 0, 59, 0);
            //serverDate.setDate(serverDate.getDate() - 2);
            if (InvoiceDate > serverDate) {
                args.IsValid = false
            }
            else {
                args.IsValid = true
            }

        }
        function validateInitializeDate(oSrc, args) {

            var varActiondate = (document.getElementById('ctl00_MainConHolder_txtInitialActionDate').value);
            var varServerDate = (document.getElementById('ctl00_MainConHolder_hdnInitDate').value);
            var arrayAction = varActiondate.split('/');
            var arrayServer = varServerDate.split('/');
            var actionDate = new Date();
            var serverDate = new Date();
            actionDate.setFullYear(arrayAction[2], (arrayAction[0] - 1), arrayAction[1]);
            actionDate.setHours(0, 0, 59, 0);
            serverDate.setFullYear(arrayServer[2], (arrayServer[0] - 1), arrayServer[1]);
            serverDate.setHours(0, 0, 59, 0);
            serverDate.setDate(serverDate.getDate() - 2);
            if (actionDate < serverDate) {
                args.IsValid = false
            }
            else {
                args.IsValid = true
            }

        }

        function validateInitializeDate1(oSrc, args) {

            var varActiondate = (document.getElementById('ctl00_MainConHolder_txtInitialActionDate').value);
            var varServerCurrentDate = (document.getElementById('ctl00_MainConHolder_hdnGlobalDate').value);

            var arrayAction = varActiondate.split('/');
            var aarayCurrentdate = varServerCurrentDate.split('/');

            var actionDate = new Date();
            var serverDate = new Date();
            var curr_date = new Date();

            actionDate.setFullYear(arrayAction[2], (arrayAction[0] - 1), arrayAction[1]);
            actionDate.setHours(0, 0, 59, 0);
            curr_date.setFullYear(aarayCurrentdate[2], (aarayCurrentdate[0] - 1), aarayCurrentdate[1]);
            curr_date.setHours(0, 0, 59, 0);


            if (actionDate > curr_date) {
                args.IsValid = false
            }
            else {
                args.IsValid = true
            }

        }

        function funUploadPopUp(CRefNo) {
            var strUrl = '../Pages/UploadedFilePopUp.aspx?CompNo=' + CRefNo;
            custWin = window.open(strUrl, 'SCPopup', 'height=550,width=750,left=20,top=30,scrollbars=1');
            if (window.focus) { custWin.focus() }
        }



        function funCustomerMaster(cid, CRefNo) {
            var strUrl = '../Reports/CustomerDetail.aspx?CustNo=' + cid + '&CompNo=' + CRefNo;
            custWin = window.open(strUrl, 'SCPopup', 'height=550,width=750,left=20,top=30,scrollbars=1');
            if (window.focus) { custWin.focus() }
        }

        function funCommLog(compNo, splitNo) {
            var strUrl = 'CommunicationLog.aspx?CompNo=' + compNo + '&SplitNo=' + splitNo;
            logWin = window.open(strUrl, 'CommunicationLog', 'height=550,width=750,left=20,top=30,scrollbars=1');
            if (window.focus) { logWin.focus() }
        }
        function funHistoryLog(compNo, splitNo) {
            var strUrl = 'HistoryLog.aspx?CompNo=' + compNo + '&SplitNo=' + splitNo;
            hisWin = window.open(strUrl, 'History', 'height=550,width=750,left=20,top=30,scrollbars=1');
            if (window.focus) { hisWin.focus() }
        }
        function funPrint(compNo, baseID, DiviSno) {
            if (DiviSno == 15 || DiviSno == 19) {
                var strUrl = 'PrintCallSlipIS.aspx?qsCompNo=' + compNo + '&BaseLineId=' + baseID;
            }
            else {
                var strUrl = 'PrintCallSlipCP.aspx?qsCompNo=' + compNo + '&BaseLineId=' + baseID;
            }
            prWin = window.open(strUrl, 'Print', 'height=650,width=850,left=20,top=30,resizable=1,scrollbars=1');
            if (window.focus) { prWin.focus() }
        }
        function funPrintPopup(DiviSno) {
            if (DiviSno == 15 || DiviSno == 19) {
                var strUrl = 'PrintCallSlipIS.aspx'
            }
            else {
                var strUrl = 'PrintCallSlipCP.aspx'
            }
            prWin = window.open(strUrl, 'Print', 'height=650,width=850,left=20,top=30,resizable=1,scrollbars=1');
            if (window.focus) { prWin.focus() }
        }

        function funSpare() {
            var arrCompNo = document.getElementById('ctl00_MainConHolder_lblActionComplaintRefNo').innerHTML;
            arrCompNo = arrCompNo.split('/');
            var compNo = arrCompNo[0];
            var splitNo = (document.getElementById('ctl00_MainConHolder_hdnActionSplitNo').value);
            var strUrl = '../SIMS/Pages/SpareRequirementComplaint.aspx?CompNo=' + compNo + '&SplitNo=' + splitNo;
            hisWin = window.open(strUrl, 'Spare', 'scrollbars=1');
            if (window.focus) { hisWin.focus() }
        }

        function DisAppDDl() {

            var ddlApp = $get('ctl00_MainConHolder_ddlAppointment');
            var ddlStatusValue = $get('ctl00_MainConHolder_ddlStatus');
            ddlApp.selectedIndex = 0;
            if (ddlStatusValue.value == 2) {
                ddlApp.disabled = false;
            }
            else {
                ddlApp.disabled = true;
            }
        }

        function OpenActivityPop(url) {
            newwindow = window.open(url, 'name', 'height=400,width=700,scrollbars=1,resizable=no,top=1');
            if (window.focus) {
                newwindow.focus()
            }
            return false;
        }

        function OpenSparePop(url) {
            nWindow = window.open(url, 'SpareRequirement', 'height=500,width=800,scrollbars=1,resizable=yes,top=1');
            if (window.focus) {
                nWindow.focus()
            }
            return false;
        }

        var ModalProgress = 'ctl00_MainConHolder_ModalProgress';
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {
            $find(ModalProgress).show();

        }


        function endReq(sender, args) {


            $find(ModalProgress).hide();
        }

        function OpenDiv(idPopup, idBlanket) {
            if (idPopup != undefined && idBlanket != undefined) {
                document.getElementById(idPopup.id).style.display = "block";
                document.getElementById(idBlanket.id).style.display = "block";
            }
        }
        function CloseDiv(idPopup, idBlanket) {
            if (idPopup != undefined && idBlanket != undefined) {
                document.getElementById(idPopup.id).style.display = "none";
                document.getElementById(idBlanket.id).style.display = "none";
                if (idPopup.id.trim() == "dvPopup") {
                    if (document.getElementById("ctl00_MainConHolder_ddlInitAction") != null) document.getElementById("ctl00_MainConHolder_ddlInitAction").value = "0";
                    if (document.getElementById("ctl00_MainConHolder_ddlActionStatus") != null) document.getElementById("ctl00_MainConHolder_ddlActionStatus").value = "0"
                    __doPostBack(null, null);
                }
            }
        }


        function WindingVendorFlag() {
            var ddlAttr = document.getElementById("ctl00_MainConHolder_DdlDefectAttribute");
            if (document.getElementById("ctl00_MainConHolder_DdlDefectAttribute").options[ddlAttr.selectedIndex].text != "N.A." || document.getElementById("ctl00_MainConHolder_ddlDefectCat").value != 3) {
                document.getElementById("ctl00_MainConHolder_trNaWindingUnti").style.display = "none";
            }
            else if (document.getElementById("ctl00_MainConHolder_ddlDefectCat").value == 3 && document.getElementById("ctl00_MainConHolder_DdlDefectAttribute").options[ddlAttr.selectedIndex].text == "N.A.") {
            document.getElementById("ctl00_MainConHolder_trNaWindingUnti").style.display = "";
            }
        }

        function OpenConfirmDiv() {
            OpenDiv(dvConfirmPopup, dvConfirmBlanket);
        }