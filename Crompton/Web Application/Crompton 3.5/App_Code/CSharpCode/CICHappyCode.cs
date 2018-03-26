using System;
/// <summary>
/// Summary description for NewChanges
/// </summary>
public class CICNewChanges
{
    public CICNewChanges()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   // genrate random no
    public static string HappyCode()
    {
        Random r = new Random();
        int randNum = r.Next(10000, 99999);
        string fiveDigitNumber = randNum.ToString();
        return fiveDigitNumber;
    }
}