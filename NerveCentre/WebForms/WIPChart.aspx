<%@ Page Title="KANBAN" Language="C#" MasterPageFile="~/WebForms/BlankForFrames.Master" AutoEventWireup="true" CodeBehind="WIPChart.aspx.cs" Inherits="NerveCentre.WIPChart" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .rectangle {
            height: 20px;
            width: 50px;
        }
    </style>
    <%-- <meta http-equiv="refresh" content="5"; />--%>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-6 text-right">
            <label id="Label1" runat="server" class="text-center" style="color:#800080; font-weight:bolder; font-size:30px; margin-right:-60px">WIP Kanban - </label>
        </div>
        <div class="col-md-6">
             <label id="lblFactory" runat="server" style="color:#800080; font-weight:bolder;font-size:30px;margin-left:40px"></label>
        </div>
    </div>

    <div class="container-fluid" style="font-family: Georgia; padding-left: 15px;">

        <div class="row" style="padding-left:125px">
            <div class="col-md-1">
                <div class="rectangle" style="background-color:#e0400a">
                </div>
            </div>
            <div class="col-md-1" style="margin-left:-60px">
                 <label>SEWING</label>
            </div>
            <div class="col-md-1">
                <div class="rectangle" style="background-color:#056492;">
                </div>
            </div>
            <div class="col-md-1" style="margin-left:-60px">
                 <label>FINISHING</label>
            </div>
        </div>

        <asp:Chart ID="Chart1" runat="server">
            <Series>
                <asp:Series Name="Series2" Label="SEWING" ChartType="StackedColumn"></asp:Series>
                <asp:Series Name="Series1" Label="FINISHING" ChartType="StackedColumn"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
</asp:Content>