<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" Title="Sticker Master"
    AutoEventWireup="true" CodeFile="StickerMaster.aspx.cs" Inherits="Admin_StickerMaster" %>
        
<%@ Register src="~/UC/AdminStickerDetails.ascx" tagname="StickerDetails" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
  <%--  <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>--%>

            <script src="../scripts/jquery-1.2.6.js" type="text/javascript"></script>

            <link href="../css/Popup.css" rel="stylesheet" type="text/css" />

            <script type="text/javascript">
                $(document).ready(function() {
                    $("#aBulkupload").click(function() {
                        $("#tblBulkupload").toggle("slow");
                    });
                    $("#aPrimaryFileFormate").click(function() {
                        $("#imgHelpForColumn").attr("src", "../images/PrimaryStickerData.jpg");
                        visiblty();
                    });
                    $("#aClosedPopup").click(function() {
                        visiblty();
                    });

                    var visiblty = function() {
                        $("#dvBlanket").toggle();
                        $("#dvPopup").toggle();
                    }
                });

            </script>

            <table width="100%">
                <tr>
                    <td class="headingred">
                        Sticker Master
                    </td>
                    <td></td>
                    <td align="<%=ConfigurationManager.AppSettings["AjaxLoadingAlign"]%>" style="padding-right: 10px;">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img src="<%=ConfigurationManager.AppSettings["AjaxLoadingImageName"]%>" alt="" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;"  class="bgcolorcomm">
                <tr id="trUploadPrimary" runat="server">
                <td colspan="2">
                <table style="width:100%; border-bottom:solid 1px #396870;">
                <tr>
                <td  colspan="2">
                <b>Sticker Upload</b>
                </td>
                </tr>
                <tr>
                <td>
                        <asp:FileUpload ID="fupPrimaryStikerData" runat="server" CssClass="btn" ToolTip="Select file for upload" />
                        &nbsp;
                        <asp:Button ID="btnuploadPrimaryData" runat="server" CssClass="btn" OnClick="btnuploadPrimaryData_Click"
                            Text="Upload" />
                    </td>
                    <td>
                        <a href="javascript:void(0)" title="Please Make sure xls column name is same as given in pic."
                            id="aPrimaryFileFormate">File Format</a>
                    </td>
                </tr>
                <tr>
                <td>
                        <span style="color:Red">Note :</span><i>Please Click Refresh button after uploading data to view uploaded data in grid.</i></td>
                    <td><a href="../SIMS/BulkUpload/BulkStickerEntry.xls" >Bulk Upload Sticker</a>
                        </td>
                </tr>
                </table>
                </td>
                    
                </tr>
                
                <tr>
                    <td colspan="2">
                         <uc1:StickerDetails ID="uclStickerDetails" runat="server" />
                    </td>
                </tr>
            </table>            
            <table>
            <tr>
            <td>
            
            <div id="dvBlanket" style="display: none;" >
            </div>
            <div id="dvPopup" class="popUpDivUpload" style="display: none;">
                <a href="javascript:void(0);" id="aClosedPopup" class="close">
                    <img alt="" src="../images/PopupClose.png" style="border: 0;" />
                </a>
                <div style="margin-left: 5px; overflow: scroll;">
                    <img id="imgHelpForColumn" title="Column details for upload data" />
                </div>
            </div>
            </td>
            </tr>
            </table>
   <%--     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnuploadPrimaryData" />
        </Triggers>
    </asp:UpdatePanel>--%>
   
</asp:Content>
