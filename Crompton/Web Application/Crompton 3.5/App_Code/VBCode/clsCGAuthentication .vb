Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Web.HttpUtility
Imports System.Web.UI.Page


Public Class clsCGAuthentication
    Public Shared Function CGUserAuthentication(ByVal strUserId As String, ByVal strPassWord As String, ByRef strMessage As String) As Boolean

        'Dim objEncr As Object

        Dim ObjUI As New System.Web.UI.Page()
        'objEncr = ObjUI.Server.CreateObject("HRISEncr.encr")
        Dim strEncrpassword As String
        Dim strURlEncode As String
        strEncrpassword = getEncrValue(strPassWord) 'objEncr.getEncrValue(strPassWord) 'Function to encrypt password
        strURlEncode = URLEncode(strEncrpassword)
        'sdfahas




        Dim strUrl As String
        Dim obj As New WebClient()
        Dim objTemp As Stream

        ' 10 dec 11 update url 
        'strUrl = "https://cghr4u.cgl.co.in/login/chkauth.asp?login=" & strUserId & "&pwd=" & strURlEncode
        'objTemp = obj.OpenRead(strUrl)

        Try
            'strUrl = "https://cghr4u.cgglobal.com/login/chkauth.asp?login=" & strUserId & "&pwd=" & strURlEncode
            'objTemp = obj.OpenRead(strUrl)
            strUrl = "http://itapps.cgglobal.com/CGWebService/CGWebService.asmx?EmpCode=" & strUserId & "&EmpPassword=" & strURlEncode
            objTemp = obj.OpenRead(strUrl)
        Catch ex As WebException

            strUrl = "http://cghr4u.cgl.co.in/login/chkauth.asp?login=" & strUserId & "&pwd=" & strURlEncode
            objTemp = obj.OpenRead(strUrl)
        End Try


        Dim objStreamReader As New StreamReader(objTemp)
        Dim strLines As String
        strLines = objStreamReader.ReadToEnd
        If Not InStr(strLines, "Invalid") > 0 Then
            strMessage = "Vaild"
            Return True
        Else
            strMessage = strLines
            Return False
        End If


    End Function
    Private Shared Function URLEncode(ByVal str As String) As String
        Dim strTemp, strChar As String
        strTemp = ""
        strChar = ""
        Dim nTemp, nAsciiVal As Integer

        For nTemp = 1 To Len(str)
            nAsciiVal = Asc(Mid(str, nTemp, 1))
            If ((nAsciiVal < 123) And (nAsciiVal > 96)) Then
                strTemp = strTemp & Chr(nAsciiVal)
            ElseIf ((nAsciiVal < 91) And (nAsciiVal > 64)) Then
                strTemp = strTemp & Chr(nAsciiVal)
            ElseIf ((nAsciiVal < 58) And (nAsciiVal > 47)) Then
                strTemp = strTemp & Chr(nAsciiVal)
            Else
                strChar = Trim(Hex(nAsciiVal))
                If nAsciiVal < 16 Then
                    strTemp = strTemp & "%0" & strChar
                Else
                    strTemp = strTemp & "%" & strChar
                End If
            End If
        Next
        URLEncode = strTemp
    End Function

    'Private Shared Function URLEncode(ByVal str As String) As String
    '    Dim strTemp, strChar As String
    '    strTemp = ""
    '    strChar = ""
    '    Dim nTemp, nAsciiVal As Integer
    '    For nTemp = 1 To Len(str)
    '        nAsciiVal = Asc(Mid(str, nTemp, 1))
    '        If ((nAsciiVal < 123) And (nAsciiVal > 96)) Then
    '            strTemp = strTemp & Chr(nAsciiVal)
    '        ElseIf ((nAsciiVal < 91) And (nAsciiVal > 64)) Then
    '            strTemp = strTemp & Chr(nAsciiVal)
    '        ElseIf ((nAsciiVal < 58) And (nAsciiVal > 47)) Then
    '            strTemp = strTemp & Chr(nAsciiVal)
    '        Else
    '            strChar = Trim(Hex(nAsciiVal))
    '            If nAsciiVal < 16 Then
    '                strTemp = strTemp & "%0" & strChar
    '            Else
    '                strTemp = strTemp & "%" & strChar
    '            End If
    '        End If
    '    Next
    '    URLEncode = strTemp
    'End Function

    Private Shared Function getEncrValue(ByVal paramStr As String) As String
        Dim proStr As String = paramStr
        Dim tempStr As String = ""
        Dim tempCharValue As Integer
        Dim encrConstant As Integer
        encrConstant = 10
        Dim I As Integer
        For I = 1 To Len(proStr)
            ' tempCharValue = Asc(Mid(proStr, I, 1))
            ' tempStr = tempStr & Chr(tempCharValue - encrConstant)
            tempCharValue = Asc(Mid(proStr, I, 1))
            tempStr = tempStr & Chr(tempCharValue + encrConstant)
        Next
        Return tempStr
    End Function

End Class
