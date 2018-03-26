/// <summary>
/// Summary description for BPSecurity
/// </summary>
public static class BPSecurity
{
    public static string ProtectPassword(string str)
    {
        string _result = string.Empty;
        char[] temp = str.ToCharArray();
        foreach (var _singleChar in temp)
        {
            var i = (int)_singleChar;
            i = i - 2;
            _result += (char)i;
        }
        return _result;
    }

    public static string UnprotectPassword(string str)
    {
        string _result = string.Empty;
        char[] temp = str.ToCharArray();
        foreach (var _singleChar in temp)
        {
            var i = (int)_singleChar;
            i = i + 2;
            _result += (char)i;
        }
        return _result;
    }
}




