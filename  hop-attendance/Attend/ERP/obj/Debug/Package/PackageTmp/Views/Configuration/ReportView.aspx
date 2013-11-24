<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ReportView
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="mp_left_menu">
        <% Html.RenderPartial("LeftMenu"); %>
    </div>
    <div class="mp_right_content">
            <div class="page_list_container">
            <div><%--<div> Appreal Management  </div>--%>               
                <%--<input type="button" value="Print Excel" title="Excel"   onclick="excel_btn()" />--%>
            </div>
            <div id="RecordsContainer">
            </div>
        </div>
    </div> 
</asp:Content>