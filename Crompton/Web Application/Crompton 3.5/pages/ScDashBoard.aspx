<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true" CodeFile="ScDashBoard.aspx.cs" Inherits="pages_ScDashBoard" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" Runat="Server">
    <asp:Chart ID="scchart" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"  
        BorderlineWidth="1" Height="500px" Palette="None" PaletteCustomColors="Maroon"  
        Width="1000px" BorderlineColor="64, 0, 64">  
        <Titles>  
            <asp:Title ShadowOffset="10" Name="Items" />  
        </Titles>  
         <Legends>  
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"  
                LegendStyle="Row" />  
        </Legends>  
        <Series>
            <asp:Series Name="DoughnutSeries" ChartType="Doughnut">
            
            </asp:Series>
        </Series>
        <ChartAreas>
           
            <asp:ChartArea Name="ChartArea1" BorderWidth="0">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <br />
    <br />
    
      <asp:Chart ID="Chart1" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"  
        BorderlineWidth="1" Height="500px" Palette="None" PaletteCustomColors="Maroon"  
        Width="1000px" BorderlineColor="64, 0, 64">  
        <Titles>  
            <asp:Title ShadowOffset="10" Name="Items" />  
        </Titles>  
         <Legends>  
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"  
                LegendStyle="Row" />  
        </Legends>  
        <Series>
            <asp:Series Name="renko" ChartType="Renko">
            
            </asp:Series>
        </Series>
        <ChartAreas>
           
            <asp:ChartArea Name="ChartArea1" BorderWidth="0">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    
</asp:Content>

