
 function getNumeric_only(strvalue)
 {
    Test = strvalue;
    Values = '';
    var KeyID=0;
    for (i=0,n=Test.length;i<n;i++)
    {
        KeyID=Test.charCodeAt(i);
        if((KeyID>47 && KeyID<58)||(KeyID==8))
        {
        }
        else
        {
            return false;
        }
    }
    return true;
 }
 
 function getNumeric_dot_only(strvalue)
 {
    Test = strvalue;
    Values = '';
    var KeyID=0;
    for (i=0,n=Test.length;i<n;i++)
    {
        KeyID=Test.charCodeAt(i);
        if((KeyID>47 && KeyID<58)||(KeyID==8) ||(KeyID==46))
        {
        }
        else
        {
            return false;
        }
    }    
    var arrText=new Array();
    arrText=strvalue.split(".")
    for(i=0;i<arrText.length;i++)
    {
        if(arrText[i].length>2)
            return false;
    }
    
    return true;
 }
 
 function getAlphabate_only(strvalue)
 {
    Test = strvalue;
    Values = '';
    var KeyID=0;
    for (i=0,n=Test.length;i<n;i++)
    {
        KeyID=Test.charCodeAt(i);
        if((KeyID>64 && KeyID<91)|| (KeyID>96 && KeyID<123)|| (KeyID==32)||(KeyID==8))
        {
        }
        else
        {
            return false;
        }
    }
    return true;
 }
 
 function getAlphabate_dot_comma_only(strvalue)
 {
    Test = strvalue;
    Values = '';
    var KeyID=0;
    for (i=0,n=Test.length;i<n;i++)
    {
        KeyID=Test.charCodeAt(i);
        if((KeyID>64 && KeyID<91)|| (KeyID>96 && KeyID<123)|| (KeyID==32)||(KeyID==8) ||(KeyID==46) ||(KeyID==44))
        {
        }
        else
        {
            return false;
        }
    }
    return true;
 }
 
 
 
 function ShowHideDivAccMouseFix(e,dvStartName,dvId,ttlCnt,left,top)
  {
  var intCnt;
  for(intCnt=1;intCnt<=ttlCnt;intCnt++)
  {
  
       if((dvStartName+intCnt)==dvId)
        {
            document.getElementById(dvId).style.display='block';
            Drag.init(document.getElementById(dvId));
            document.getElementById(dvId).style.left=left;
            document.getElementById(dvId).style.top=top;
        }
        else
        {  
          if(document.getElementById(dvStartName+intCnt))
            document.getElementById(dvStartName+intCnt).style.display='none';
        }
    }
  }
 
 
 
 // For div placement according to mouse coordinate
 // e--> Event 
 //dvId Div id to show
 // total Div count
 function ShowHideDivAccMouse(e,dvStartName,dvId,ttlCnt)
  {
  if( !e ) 
  {
     if( window.event ) 
     {
      //Internet Explorer
         e = window.event;
      }
      else
      {
        //total failure, we have no way of referencing the event
        return;
      }
  }
  if( typeof( e.pageX ) == 'number' ) {
    //most browsers
    var xcoord = e.pageX;
    var ycoord = e.pageY;
  } else if( typeof( e.clientX ) == 'number' ) {
    //Internet Explorer and older browsers
    //other browsers provide this, but follow the pageX/Y branch
    var xcoord = e.clientX;
    var ycoord = e.clientY;
    var badOldBrowser = ( window.navigator.userAgent.indexOf( 'Opera' ) + 1 ) ||
     ( window.ScriptEngine && ScriptEngine().indexOf( 'InScript' ) + 1 ) ||
     ( navigator.vendor == 'KDE' );
    if( !badOldBrowser ) {
      if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) ) {
        //IE 4, 5 & 6 (in non-standards compliant mode)
        xcoord += document.body.scrollLeft;
        ycoord += document.body.scrollTop;
      } else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) ) {
        //IE 6 (in standards compliant mode)
        xcoord += document.documentElement.scrollLeft;
        ycoord += document.documentElement.scrollTop;
      }
    }
  } else {
    //total failure, we have no way of obtaining the mouse coordinates
    return;
  }
  var intCnt;
  for(intCnt=1;intCnt<=ttlCnt;intCnt++)
  {
  
       if((dvStartName+intCnt)==dvId)
        {
            document.getElementById(dvId).style.display='block';
             Drag.init(document.getElementById(dvId));
          //  //below two line for mouse according
          // document.getElementById(dvId).style.left=xcoord+5;
          //  document.getElementById(dvId).style.top=ycoord+5;
            document.getElementById(dvId).style.left=465;
            document.getElementById(dvId).style.top=330;

        }
        else
        {  
          if(document.getElementById(dvStartName+intCnt))
            document.getElementById(dvStartName+intCnt).style.display='none';
        }
    }
  }
  
  // For div placement according to mouse coordinate
 // e--> Event 
 //dvId Div id to show
 // total Div count
 function ShowHideDivMouseCoord(e,dvStartName,dvId,ttlCnt)
  {
  if( !e ) 
  {
     if( window.event ) 
     {
      //Internet Explorer
         e = window.event;
      }
      else
      {
        //total failure, we have no way of referencing the event
        return;
      }
  }
  if( typeof( e.pageX ) == 'number' ) {
    //most browsers
    var xcoord = e.pageX;
    var ycoord = e.pageY;
  } else if( typeof( e.clientX ) == 'number' ) {
    //Internet Explorer and older browsers
    //other browsers provide this, but follow the pageX/Y branch
    var xcoord = e.clientX;
    var ycoord = e.clientY;
    var badOldBrowser = ( window.navigator.userAgent.indexOf( 'Opera' ) + 1 ) ||
     ( window.ScriptEngine && ScriptEngine().indexOf( 'InScript' ) + 1 ) ||
     ( navigator.vendor == 'KDE' );
    if( !badOldBrowser ) {
      if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) ) {
        //IE 4, 5 & 6 (in non-standards compliant mode)
        xcoord += document.body.scrollLeft;
        ycoord += document.body.scrollTop;
      } else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) ) {
        //IE 6 (in standards compliant mode)
        xcoord += document.documentElement.scrollLeft;
        ycoord += document.documentElement.scrollTop;
      }
    }
  } else {
    //total failure, we have no way of obtaining the mouse coordinates
    return;
  }
  var intCnt;
  for(intCnt=1;intCnt<=ttlCnt;intCnt++)
  {
  
       if((dvStartName+intCnt)==dvId)
        {
            document.getElementById(dvId).style.display='block';
             Drag.init(document.getElementById(dvId));
          //  //below two line for mouse according
           document.getElementById(dvId).style.left=xcoord+5;
            document.getElementById(dvId).style.top=ycoord+5;
           

        }
        else
        {  
          if(document.getElementById(dvStartName+intCnt))
            document.getElementById(dvStartName+intCnt).style.display='none';
        }
    }
  }
  

// Date Related functions start

        // Get Month as number and take string monthname;
        function getMonthNum(Str)
        {
	        var month;
	        //Str=Str.toLowerCase();
	        switch(Str)
	        {
		        case "Jan":
		        month ="1";
		        break;
		        case "Feb":
		        month ="2";
		        break;
		        case "Mar":
		        month ="3";
		        break;
		        case "Apr":
		        month ="4";
		        break;
		        case "May":
		        month ="5";
		        break;
		        case "Jun":
		        month ="6";
		        break;
		        case "Jul":
		        month ="7";
		        break;
		        case "Aug":
		        month ="8";
		        break;
		        case "Sep":
		        month ="9";
		        break;
		        case "Oct":
		        month ="10";
		        break;
		        case "Nov":
		        month ="11";
		        break;
		        case "Dec":
		        month ="12";
		        break;
	        }
	         return month;
        }// end of getMonthNum

        // This getMonthChar function takes parameter Str as number 
        // and return Month Name
        function getMonthChar(Str){
	         var month;
	         Str=parseInt(Str);
	         switch(Str){
		        case 1:
		          month ="Jan";
		          break;
		        case 2:
		          month = "Feb";
		          break;
		           case 3:
		          month ="Mar";
		          break;
		           case 4:
		          month ="Apr";
		          break;
		           case 5:
		          month ="May";
		          break;
		           case 6:
		          month ="Jun";
		          break;
		           case 7:
		          month ="Jul";
		          break;
		           case 8:
		          month ="Aug";
		          break;
		           case 9:
		          month ="Sep";
		          break;
		           case 10:
		          month ="Oct";
		          break;
		           case 11:
		          month ="Nov";
		          break;
		           case 12:
		          month ="Dec";
		          break;
	         }
	         return month;
        }
        
        // This DiffDates function takes parameters
        // sday --> ToDate day, smon --> ToDate month, syy --> ToDate year 
        // day -->FromDate day, mon --> FromDate month, yy --> FromDate year 
        // and return Month Name
        
         function DiffDates(sday, smon, syy, day, mon, yy)
         {  
	        var d1;
	        var d2;
	        var j1 = Number(sday);
	        var m1 = getMonthChar(Number(smon));
	        var y1 = Number(syy);
	        var j2 = Number(day);
            var m2 = getMonthChar(Number(mon));
	        var y2 = Number(yy);
	        var sdate = j1 + " " + m1 + " " + y1;
	        var cdate = j2 + " " + m2 + " " + y2;
	        d1 = new Date(sdate);
	        d2 = new Date(cdate);
	        return Math.round((d2-d1)/86400000);
         } //end DiffDates

        // This getDateDifferenceDays function takes parameters
        // ToDate --> To Date ('mm/dd/yyyy'), FromDate --> From date ('mm/dd/yyyy')
        // and return no of days 
        // If > 0 then To Date > From Date
        // If = 0 then To Date = From Date 
        // If < 0 then To Date < From Date
        function getDateDifferenceDays(ToDate,FromDate)
         {
                if(!CheckDateFormatMMDDYYYY(FromDate) || !CheckDateFormatMMDDYYYY(ToDate))
                {
                    return false;;
                }
                ToDate=new Date(ToDate);
                //ToDate = ToDate.toUTCString();
                FromDate= new Date(FromDate);
                //FromDate=FromDate.toUTCString();
                Yearjn = GetYear(ToDate);
                Monthjn = ToDate.getMonth();
	            Dayjn = ToDate.getDate();
	            Modjn = (Date.UTC(Yearjn,Monthjn,Dayjn,0,0,0))/86400000;
	            Yearcur = GetYear(FromDate);
	            Monthcur = FromDate.getMonth();
	            Daycur = FromDate.getDate();
	            Modcur = (Date.UTC(Yearcur,Monthcur,Daycur,0,0,0))/86400000;
                daysago =Math.floor( Modjn - Modcur);
	            return daysago;
          }//end getDateDifferenceDays

            // This GetYear function takes parameters
            // theDate --> Date from which you want year
            // and return Year 
            function GetYear(theDate) 
            {
	            x = theDate.getYear();
	            var y = x % 100;
	            y += (y < 38) ? 2000 : 1900;
	            return y;
            } // End GetYear
            
            // This isFeb function takes parameters
            // month --> month as number, day --> No. of days
            // and return true if day is less than 30 otherwise false 
            function isFeb(month,day)
            {	
	            if(month==2 &&  day>29)
	            {
		            alert("The Feb month does not contain 30 days");		
		            return true;
	            }
	            return false;
            }//end isFeb
        
            // This CheckDateFormatMMDDYYYY function takes parameters
            // val --> value to be check whether it is in mm/dd/yyyy format or not
            // and return true if correct otherwise false 
            function CheckDateFormatMMDDYYYY(val)
             {
               if(val != "")
                {
                    if((val.length>10) ||(val.length<8))
                    {
	                    alert("Enter the valid date in (mm/dd/yyyy) format. ")
	                    return false;
                    }
                    else 
                    {
	                    slash1=val.indexOf("/")
	                    if(slash1==-1)
	                    {
		                    alert("Enter the valid date in (mm/dd/yyyy) format.")
		                    return false;
	                    }	
	                    slash2=val.lastIndexOf("/")
	                    if((slash2==-1)||(slash2==slash1))
	                    {
		                    alert("Enter the valid date in (mm/dd/yyyy) format.")
		                    return false;
	                    }	
                    	
	                    var two=(val.substring(0,slash1));
	                    if ((two>=0)&&(two<=12))
	                    {
		                    mm=two;
	                    }
	                    else
	                    {	
		                    alert("Enter the valid date in (mm/dd/yyyy) format.")
		                    return false;
	                    }
	                    two=(val.substring(slash1+1,slash2));
	                    if ((two>=0)&&(two<=31))
	                    {
		                    dd=two;
	                    }
	                    else
	                    {	
		                    alert("Enter the valid date in (mm/dd/yyyy) format.")
		                    return false;
	                    }
	                    two=(val.substring(slash2+1,slash2+5));
	                    if ((two>=1900)&&(two<=2075))
	                    {
		                    yy=two;
	                    }
	                    else
	                    {	
		                    alert("Enter the valid date in (mm/dd/yyyy) format.")
		                    return false;
	                    }

	                    if(((dd==29)&&(mm==02))||((dd==29)&&(mm==2)))
	                    {
                    	
		                    if(((yy%4)==0)||((yy%400)==0))
		                    {
		                    //valid
		                    }
		                    else
		                    {
		                    alert("Enter the valid date in (mm/dd/yyyy) format.")
		                    return false;
		                    }
	                    }
	                    if (((dd>29)&&(mm==2))||((dd==31)&&((mm==2)||(mm==4)||(mm==6)||(mm==9)||(mm==11))))
	                    {	
		                    alert("Enter the valid date in (mm/dd/yyyy) format.")
		                    return false;
	                    }
                  }	
                return true;
              }	
            return true;
           } //end CheckDateFormatMMDDYYYY           

        
            // This GetAge function takes parameters
            // strDate --> entered date for DOB and it should be in mm/dd/yyyy format 
            // return Age as number of year. 
        function GetAge(strDate)
        {
           if(!CheckDateFormatMMDDYYYY(strDate))
            {
                return false;;
            }
	        birthTime = new Date(strDate);
	        todaysTime = new Date();
	        todaysYear = todaysTime.getYear();
	        if (todaysYear < 2002) todaysYear += 1900;
	        todaysMonth = todaysTime.getMonth();
	        todaysDate = todaysTime.getDate();
	        todaysHour = todaysTime.getHours();
	        todaysMinute = todaysTime.getMinutes();
	        todaysSecond = todaysTime.getSeconds();
	        birthYear = birthTime.getYear();
	        if (birthYear < 2002) birthYear += 1900;
	        birthMonth = birthTime.getMonth();
	        birthDate = birthTime.getDate();
	        birthHour = birthTime.getHours();
	        birthMinute = birthTime.getMinutes();
	        birthSecond = birthTime.getSeconds();

	        var monarr = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

	        // check for leap year
	        if (((todaysYear % 4 == 0) && (todaysYear % 100 != 0)) || (todaysYear % 400 == 0)) monarr[1] = "29";

	        countMonth = monarr[todaysTime.getMonth()];

	        // Doing the subtactions
	        if(todaysMinute > birthMinute)
	        {
		        diffMinute = todaysMinute - birthMinute;
		        calcHour = 0;
	        }
	        else 
	        {
		        diffMinute = todaysMinute + 60 - birthMinute;
		        calcHour = -1;
	        }
	        if(todaysHour > birthHour)
	        {
		        diffHour = todaysHour - birthHour + calcHour;
		        calcDate = 0;
	        }
	        else
	        {
		        diffHour = todaysHour + 24 - birthHour  + calcHour;
		        calcDate = -1;
	        }
	        if(todaysDate > birthDate)
	        {
		        diffDate = todaysDate - birthDate + calcDate;
		        calcMonth = 0;
	        }
	        else
	        {
		        diffDate = todaysDate + countMonth - birthDate  + calcDate;
		        calcMonth = -1;
	        }
	        if(todaysMonth > birthMonth)
	        {
		        diffMonth = todaysMonth - birthMonth + calcMonth;
		        calcYear = 0;
	        }
	        else 
	        {
		        diffMonth = todaysMonth + 12 - birthMonth + calcMonth;
		        calcYear = -1;
	        }
	        diffYear = todaysYear - birthYear + calcYear;

	        if (diffMinute == 60) { diffMinute = 0; diffHour ++; }
	        if (diffHour == 24) { diffHour = 0; diffDate ++; }
	        if (diffDate == countMonth) { diffDate = 0; diffMonth ++; }
	        if (diffMonth == 12) { diffMonth = 0; diffYear ++; }
        	
	        return diffYear;

        }//end GetAge
        
        
        
            // This isGreaterDateFromCurrent function takes parameters
            // strDate --> value to be check whether it is greater than current or not
            // and return true if greater otherwise false 
            function isGreaterDateFromCurrent(strDate)
            {
            	
            	var yy,mm,dd;
	            var present_date = new Date();
	            yy = 1900 + present_date.getYear();
	            if (yy > 3000)
	            {
		            yy = yy - 1900;
	            }
	            
	            mm = present_date.getMonth();
	            dd = present_date.getDate();
	            if(!CheckDateFormatMMDDYYYY(strDate))
            	{
            	    return false;
            	}
            	strDate= new Date(strDate);
                entered_year = GetYear(strDate);
                entered_month = strDate.getMonth();
	            entered_day = strDate.getDate();
            	
	            if (entered_year > yy)
	            {
		            alert("Entered date is greater than current date.");
		            return false;
	            }
	            if (entered_year == yy)
	            {
  	             if (entered_month > mm)
		            {
			            alert("Entered date is greater than current date.");
			            return false;
		            }
            		
		            if (entered_month == mm)
		            {
            		
			            if (entered_day > dd)
			            {
				            alert("Entered date is greater than current date.");
				            return false;
			            }
		            }
	            }
	            return true;
            }// End isGreaterDateFromCurrent

            // This isLessDateFromCurrent function takes parameters
            // strDate --> value to be check whether it is smaller than current or not
            // and return true if smaller otherwise false 
            function isLessDateFromCurrent(strDate)
            {
            	
            	var yy,mm,dd;
	            var present_date = new Date();
	            yy = 1900 + present_date.getYear();
	            if (yy > 3000)
	            {
		            yy = yy - 1900;
	            }
	            mm = present_date.getMonth();
	            dd = present_date.getDate();
	            if(!CheckDateFormatMMDDYYYY(strDate))
            	{
            	    return false;
            	}
            	strDate= new Date(strDate);
                entered_year = GetYear(strDate);
                entered_month = strDate.getMonth();
	            entered_day = strDate.getDate();
            	
	            if (entered_year < yy)
	            {
		            alert("Entered date is less than current date.");
		            return false;
	            }
	            if (entered_year == yy)
	            {
  	             if (entered_month < mm)
		            {
			            alert("Entered date is less than current date.");
			            return false;
		            }
            		
		            if (entered_month == mm)
		            {
            		
			            if (entered_day < dd)
			            {
				            alert("Entered date is less than current date.");
				            return false;
			            }
		            }
	            }
	            return true;
            }//End isLessDateFromCurrent
            
            
             // This isLessEqualDateFromCurrent function takes parameters
            // strDate --> value to be check whether it is smaller than current or not
            //strMsg --> value of message
            // and return true if smaller otherwise false 
            function isLessEqualDateFromCurrent(strDate,strMsg)
            {
            	
            	var yy,mm,dd;
	            var present_date = new Date();
	            yy = 1900 + present_date.getYear();
	            if (yy > 3000)
	            {
		            yy = yy - 1900;
	            }
	            mm = present_date.getMonth();
	            dd = present_date.getDate();
	            if(!CheckDateFormatMMDDYYYY(strDate))
            	{
            	    return false;
            	}
            	strDate= new Date(strDate);
                entered_year = GetYear(strDate);
                entered_month = strDate.getMonth();
	            entered_day = strDate.getDate();
            	
	            if (entered_year < yy)
	            {
		            //alert("Entered date is less than current date.");
		            alert(strMsg);
		            return false;
	            }
	            if (entered_year == yy)
	            {
  	             if (entered_month < mm)
		            {
			            alert(strMsg);
			            return false;
		            }
            		
		            if (entered_month == mm)
		            {
            		
			            if (entered_day <= dd)
			            {
				            alert(strMsg);
				            return false;
			            }
		            }
	            }
	            return true;
            }//End isLessEqualDateFromCurrent
            


            // This isValidDate function takes parameters
            // entered_month --> value of month,entered_day--> value of day
            // entered_year--> vaue of year to be check whether it is valid date or not
            // and return true if valid otherwise false 
            function isValidDate(entered_month,entered_day,entered_year)
            {
            
                if ((entered_year % 4) == 0) 
	            { 
		            var days_in_month = "312931303130313130313031";
 	            }
 	            else 
	            { 
		            var days_in_month = "312831303130313130313031";
 	            } 
	            var months = new Array("January","February","March","April","May","June","July","August","September","October","November","December");
	            if (entered_month != -1)
	            {
	            alert(entered_day + ","+2*entered_month +","+ (parseInt(2*entered_month)+parseInt('2')) + days_in_month.substring(parseInt(2*entered_month),parseInt(2*entered_month)+parseInt('2')));
		            if (entered_day > days_in_month.substring(2*entered_month,2*entered_month+2))
		            {
			            alert ("The Date is entered wrongly (the day field value exceeds the number of days for the month entered).");
			            return false;
		            }
	            }
	            return true;
            }// End isValidDate
        
        
            // This isValidDateNew function takes parameters
            // month --> value of month,day--> value of day
            // year--> vaue of year to be check whether it is valid date or not
            // and return true if valid otherwise false 
           
            function isValidDateNew(month,day,year)
            {
	            if(checkYear(year)==1)
	            {
		            if(month =="2" &&  day>29)
		            {
			            alert("The Feb month does not contain 30 days");
			            return false;
		            }	
	            }
	            if(CheckYear(year)==0)
	            {
		            if(month =="2" &&  day>28)
		            {
			            alert("The Feb month contain Only 28 days");
			            return false;
		            }	
	            }
		            if(month =="4" &&  day>30)
		            {
			            alert("The April month does not contain 31 days");
			            return false;
		            }
		            if(month =="6" &&  day>30)
		            {
			            alert("The June month does not contain 31 days");
			            return false;
		            }
		            if(month =="9" &&  day>30)
		            {
			            alert("The September month does not contain 31 days");
			            return false;
		            }
		            if(month =="11" &&  day>30)
		            {
			            alert("The November month does not contain 31 days");		
			            return false;
		            }
		            return true;
            }// end isValidDateNew
            
            
            // This CheckYear function takes parameters
            // year--> value of year to be check whether it is leap year or not
            // and return 1 if leap year otherwise 0 
            function CheckYear(year) 
            { 
                return (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) ? 1 : 0;
            }//End CheckYear
        
        
    
        
        
 // Date related function end  
  
  // Check for File Uploading start
                     //The isValidUpload function takes parameter
                     //str--> value to be check whether it is correct file to upload or not
                     //return true if this is valid otherwise false
                     function isValidUpload(str)
                     {
                       if(str!="")
                        {                                                           
                                    var fileTypes=["bmp","gif","png","jpg","jpeg"];// valid file types
                                    var source=str;
                                    var ext=source.substring(source.lastIndexOf(".")+1,source.length).toLowerCase();
                                    var c=0;
                                    for (var i=0; i<fileTypes.length; i++) 
                                     {
                                                 if (fileTypes[i]==ext) { c=1; break; }
                                      }
                                      if(c==0)
                                      {
                                              alert("THAT IS NOT A VALID IMAGE\nPlease load an image with an extention of one of the following:\n\n"+fileTypes.join(", "));
                                              return false;
                                       }
                        }   
                        else
                        {
                         alert("Please select [bmp,gif,png,jpg,jpeg] file to upload.");
                         return false;
                        }   
                        return true;     
                     }//End isValidUpload
//for doc file 
            function CheckUploadProfile()
                     {
                      
                       var str;
                       str="";
                       
                        if(document.getElementById('ctl00_MainContentArea_opening_txtProfileName').value=="")
                            {
                                alert("Please enter Profile Name");
                                document.getElementById('ctl00_MainContentArea_opening_txtProfileName').focus();
                                return false;
                            }
                       if(document.getElementById('ctl00_MainContentArea_opening_FileUpProfile').value=="")
                            {
                                alert("Please Upload File");
                                document.getElementById('ctl00_MainContentArea_opening_FileUpProfile').focus();
                                return false;
                            }
                            str=document.getElementById('ctl00_MainContentArea_opening_FileUpProfile').value;
                       if(str!="")
                        {                                                           
                                    var fileTypes=["doc","txt","rtf","docx"];// valid file types
                                    var source=str;
                                    var ext=source.substring(source.lastIndexOf(".")+1,source.length).toLowerCase();
                                    var c=0;
                                    for (var i=0; i<fileTypes.length; i++) 
                                     {
                                                 if (fileTypes[i]==ext) { c=1; break; }
                                      }
                                      if(c==0)
                                      {
                                              alert("That is not a valid document\nPlease load an document with an extention of one of the following:\n\n"+fileTypes.join(", "));
                                              document.getElementById('ctl00_MainContentArea_opening_FileUpProfile').focus();
                                              return false;
                                       }
                        }   
                        else
                        {
                         alert("Please select [doc,txt,rtf,docx] file to upload.");
                         document.getElementById('ctl00_MainContentArea_opening_FileUpProfile').focus();
                         return false;
                        }   
                        return true;     
                     }
                     
                 function funFileUploadNoType(e)
                 {
                    var KeyID; 
                    if(navigator.appName=="Microsoft Internet Explorer") 
                    { 
                        KeyID = e.keyCode;
                    } 
                    else 
                    { 
                        KeyID = e.charCode; 
                    } 
                    
                    if(window.event) 
                    { 
                        if(window.event.keyCode==13) 
                        { 
                            return false; 
                        } 
                    } 
                     return false; 
                   
                 }
                     

// Check for file uploading End
    
        function confirmDelete()
        {
            if(confirm('Are you sure want to delete?'))
            {
                return true;
            }
            {
                return false;
            }
        }



 function numCharsOnly()
        {            
            if(((event.keyCode>=48)&&(event.keyCode<=57))||((event.keyCode>=65)&&(event.keyCode<=90))||((event.keyCode>=97)&&(event.keyCode<=122))||((event.keyCode==13)||(event.keyCode==32))) 
            {
                event.keyCode = event.keyCode;
            }
            else
            { 
                event.keyCode=0;
            }
        }
        
        function checkcount()
        {
            if(document.getElementById('ctl00$MainContentArea$txtDesc').value.length>500)
             {
                return false;
             }
            else
            {
                return  true;
            }
        }
  /* Pop Up Area Start*/
       function funComplaintDetails(BaseLineId)
        {
             var strUrl='../Pages/ComplainRefNo.aspx?BaseLineId=' + BaseLineId;
             compWin=window.open(strUrl,'ComplaintDetails','height=250,width=750,left=20,top=30');
             if (window.focus) {compWin.focus()}
        }
         function funSCPopUp(valScSno)
         {
            if(valScSno!=0)
            {
                compWin= window.open('SCPopUp.aspx?Scno='+ valScSno + '&type=display','SC','height=250,width=900,left=20,top=30');
                 if (window.focus) {compWin.focus()}
            }
         }  
         function funComplaintPopUp(ComplaintNo)
          {
             var strUrl1='RegistrationDetailPopUp.aspx?ComplaintNo='+ComplaintNo;
                    //alert(strUrl);
             window.open(strUrl1,'Registration','height=550,width=950,left=20,top=30');
         }
      
         function funCommonPopUp(BaseLineId)
          {
          
             var strUrl1='../Pages/PopUp.aspx?BaseLineId='+BaseLineId;
                    //alert(strUrl);
             window.open(strUrl1,'Complaint','height=600,width=950,left=20,top=30,scrollbars=1');
        }
        //Add By Binay for WSC-11 jan 2010
         function funCommonPopUpWSC(WSCCustomerId)
          {     
             var strUrl1='../WSC/WSCPopUp.aspx?WSCCustomerId='+WSCCustomerId;
             window.open(strUrl1,'WSCComplaint','height=600,width=950,left=20,top=30,scrollbars=1');
        }
        function funCommonPopUpReport(BaseLineId)
          {
          
             var strUrl1='../pages/PopUp.aspx?BaseLineId='+BaseLineId;
                    //alert(strUrl);
             window.open(strUrl1,'Complaint','height=600,width=950,left=20,top=30,scrollbars=1');
        }
        function funCommLog(compNo,splitNo)
        {
            var strUrl='CommunicationLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            window.open(strUrl,'CommunicationLog','height=650,width=900,left=20,top=30, location=1,scrollbars=1');
        }
        function funHistoryLog(compNo,splitNo)
        {
            var strUrl='HistoryLog.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            window.open(strUrl,'History','height=550,width=900,left=20,top=30');
        }
        
         function funDefectEntry(compNo,splitNo)
        {
            var strUrl='ViewDefect.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;
            window.open(strUrl,'History','height=550,width=900,left=20,top=30, location=1');
        }
        
//         function funCommonPopUp(BaseLineId,CompNo, splitNo )
//          {
//             var strUrl1='PopUp.aspx?BaseLineId='+BaseLineId+ '&CompNo='+ CompNo +'&SplitNo='+ splitNo;
//                    //alert(strUrl);
//             window.open(strUrl1,'Complaint','height=600,width=950,left=20,top=30,scrollbars=1');
//         }
       
       /* POPuP FOR Suspance --- */ 
        function funSuspencePopUp(BaseLineId)
          {
            alert('gaurav');
             var strUrl1='PopUp.aspx?BaseLineId='+BaseLineId+ '&qsSuspence=1';
                    //alert(strUrl);
             window.open(strUrl1,'Complaint','height=600,width=950,left=20,top=30,scrollbars=1');
         }
       /* POPuP FOR Suspance --- */  
         
    /* Pop Up Area End*/
    
    
    function funMISComplaintMTO(Type,BusinessLine_Sno,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno,ResolverType)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&BusinessLine_Sno=' + BusinessLine_Sno + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno + '&ResolverType=' + ResolverType;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    function funMISComplaint(Type,BusinessLine_Sno,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&BusinessLine_Sno=' + BusinessLine_Sno + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    
    function funSummaryReport(Type,BusinessLine_Sno,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&BusinessLine_Sno=' + BusinessLine_Sno + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno + '&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    
    
     function funASCSummaryReport(Type,BusinessLine_Sno,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ASCComplaintMISPopUp.aspx?Type=' + Type + '&BusinessLine_Sno=' + BusinessLine_Sno + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno + '&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    //Added By Binay
    function funSummaryReportMTS(Type, Region_Sno, Branch_Sno, ProductDiv_Sno, Sc_Sno, BusinessLine_Sno, Year, month, Week, PGType)
    {

        var strUrl = '../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno + '&BusinessLine_Sno=' + BusinessLine_Sno + '&Year=' + Year + '&month=' + month + '&Week=' + Week + '&PGType=' + PGType;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    function funSummaryReportMTO_SC(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,Sc_Sno,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&Sc_Sno=' + Sc_Sno + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_CGUser(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CGExec,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&CGExec=' + CGExec + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_CGContractUser(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CGContractEmp,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&CGContractEmp=' + CGContractEmp + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    //For MIS-2 Code add By Binay-29-11-2009
     function funSummaryReportMTS2(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    function funSummaryReportMTO_SC2(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,Sc_Sno,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&Sc_Sno=' + Sc_Sno + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_CGUser2(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CGExec,BusinessLine_Sno)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&CGExec=' + CGExec + '&BusinessLine_Sno=' + BusinessLine_Sno ;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_CGContractUser2(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CGContractEmp,BusinessLine_Sno)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&CGContractEmp=' + CGContractEmp + '&BusinessLine_Sno=' + BusinessLine_Sno;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_ALLUser(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,Sc_Sno,CGExec,CGContractEmp,BusinessLine_Sno)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&Sc_Sno=' + Sc_Sno + '&CGExec=' + CGExec + '&CGContractEmp=' + CGContractEmp + '&BusinessLine_Sno=' + BusinessLine_Sno;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_ALLUserMIS6(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,Sc_Sno,CGExec,CGContractEmp,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUp.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&Sc_Sno=' + Sc_Sno + '&CGExec=' + CGExec + '&CGContractEmp=' + CGContractEmp + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    //End
    //Added By Vijay on 15th Oct, 2010
    function funSummaryReportMTSNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUpNew.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    
   //Added By Bhawesh for MIS-7 PopUp : on 17th march, 2013
    function funSummaryReportMTSWithGroup(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,Sc_Sno,BusinessLine_Sno,Year,month,Week,Gp)
    {
    
        var strUrl='../Reports/ComplaintMISPopUpNew.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&Sc_Sno=' + Sc_Sno + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week+ '&Gp='+Gp;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
        function funSummaryReportMTO_ALLUserMIS6New(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,Sc_Sno,CGExec,CGContractEmp,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUpNew.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&Sc_Sno=' + Sc_Sno + '&CGExec=' + CGExec + '&CGContractEmp=' + CGContractEmp + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_CGUserNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CGExec,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUpNew.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&CGExec=' + CGExec + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
     function funSummaryReportMTO_CGContractUserNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,CGContractEmp,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUpNew.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&CGContractEmp=' + CGContractEmp + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    function funSummaryReportMTO_SCNew(Type,Region_Sno,Branch_Sno,ProductDiv_Sno,ResolverType,Sc_Sno,BusinessLine_Sno,Year,month,Week)
    {
    
        var strUrl='../Reports/ComplaintMISPopUpNew.aspx?Type=' + Type + '&Region_Sno=' + Region_Sno + '&Branch_Sno=' + Branch_Sno + '&ProductDiv_Sno=' + ProductDiv_Sno + '&ResolverType=' + ResolverType + '&Sc_Sno=' + Sc_Sno + '&BusinessLine_Sno=' + BusinessLine_Sno +'&Year=' + Year + '&month=' + month + '&Week=' + Week;
        custWin=   window.open(strUrl,'DrillDown','height=550,width=750,left=20,top=30,scrollbars=1');
                        if (window.focus) {custWin.focus()}
    }
    //End
    // Added By Gaurav Garg For MTO On 11 NOv 09
    
     function funSAPDate(invoiceNo)
    {
        var strUrl='ViewSAPInvoiceData.aspx?InvoiceNo='+ invoiceNo;
           custWin= window.open(strUrl,'History','height=550,width=900,left=20,top=30, location=0,scrollbars=1');
           if (window.focus) {custWin.focus()} 
    }
    
    
    
    function funCommLog_MTO(compNo,splitNo)

        {

            var strUrl='CommunicationLog1.aspx?CompNo='+ compNo + '&SplitNo='+ splitNo;

            window.open(strUrl,'CommunicationLog1','height=650,width=900,left=20,top=30, location=1,scrollbars=1');

        }
