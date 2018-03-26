<%@ Page Language="C#" MasterPageFile="~/MasterPages/CICPage.master" AutoEventWireup="true"
    CodeFile="BranchDashBoard.aspx.cs" Inherits="pages_BranchDashBoard" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainConHolder" runat="Server">
    <asp:Chart ID="MobileSalesChart" runat="server" Width="1000px" Height="600px" ToolTip="Complaint details "
        BorderlineColor="Gray">
        <Series>
            <asp:Series Name="TotalPending" IsValueShownAsLabel="false" ChartType="StackedColumn" LegendText="TotalPending">
            </asp:Series>
            <asp:Series Name="Pending0D" IsValueShownAsLabel="false" ChartType="StackedColumn">
            </asp:Series>
            <asp:Series Name="Pending1D" IsValueShownAsLabel="false" ChartType="StackedColumn">
            </asp:Series>
            <asp:Series Name="Pending2D" IsValueShownAsLabel="false" ChartType="StackedColumn">
            </asp:Series>
            <asp:Series Name="Pending3D" IsValueShownAsLabel="false" ChartType="StackedColumn">
            </asp:Series>
              <asp:Series Name="Pending4D" IsValueShownAsLabel="false" ChartType="StackedColumn">
            </asp:Series>
              <asp:Series Name="PendingGT5D" IsValueShownAsLabel="false" ChartType="StackedColumn">
            </asp:Series>
            <%--Name- you can change the product name here such as type, type2--%>
            <%--IsValueShownAsLabel- you can enable the count to show on each columns and each series--%>
            <%--Each series represents each colour in a column--%>
        </Series>
        <Legends>
            <asp:Legend Name="MobileBrands" Docking="Bottom" Title="Branch Resolution "
                TableStyle="Wide" BorderDashStyle="Solid" BorderColor="#e8eaf1" TitleSeparator="Line"
                TitleFont="TimesNewRoman" TitleSeparatorColor="#e8eaf1">
            </asp:Legend>
            <%--Legends denotes the representing color for each brands--%>
            <%--It will automatically takes the names from the series names and it's associated colors or you can give legend text in series--%>
        </Legends>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisX>
                    <MajorGrid Enabled="false" />
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <br />
    <br />
    <asp:Chart ID="scchart" runat="server" Width="1000px" Height="600px" ToolTip="Complaint details "> <%--BackColor="0, 0, 64" BackGradientStyle="LeftRight"
        BorderlineWidth="1" Height="500px" Palette="None" PaletteCustomColors="Maroon"
        Width="1000px" BorderlineColor="64, 0, 64">--%>
        <Titles>
            <asp:Title ShadowOffset="10" Name="Items" />
        </Titles>
       <%-- <Legends>
            <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                LegendStyle="Row" />
        </Legends>--%>
        
       
            
        <Series>
            <asp:Series Name="TotalPending"  ChartArea="ChartArea1" ChartType="Column">
            </asp:Series>
            <asp:Series ChartArea="ChartArea1"  ChartType="Column" Name="Pending0D">
            </asp:Series>
            <asp:Series ChartArea="ChartArea1"  ChartType="Column" Name="Pending1D">
            </asp:Series>
            <asp:Series ChartArea="ChartArea1"  ChartType="Column" Name="Pending2D">
            </asp:Series>
            <asp:Series ChartArea="ChartArea1"  ChartType="Column" Name="Pending3D">
            </asp:Series>
              <asp:Series ChartArea="ChartArea1"  ChartType="Column" Name="Pending4D">
            </asp:Series>
              <asp:Series ChartArea="ChartArea1"  ChartType="Column" Name="PendingGT5D">
            </asp:Series>
            
        </Series>
        <Legends>
         <asp:Legend Name="charts" Docking="Bottom" Title="Branch Resolution "
                TableStyle="Wide" BorderDashStyle="Solid" BorderColor="#e8eaf1" TitleSeparator="Line"
                TitleFont="TimesNewRoman" TitleSeparatorColor="#e8eaf1">
            </asp:Legend>
        </Legends>
        
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BorderWidth="0">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <br />
    <br />
    <%--   <asp:Chart ID="Chart1" runat="server" BackColor="0, 0, 64" BackGradientStyle="LeftRight"  
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
    </asp:Chart>--%>
</asp:Content>
