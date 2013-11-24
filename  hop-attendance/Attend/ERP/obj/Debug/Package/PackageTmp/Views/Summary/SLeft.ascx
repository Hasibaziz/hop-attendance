<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
 <div class="page_header" style="float:left;width:99%">Attendance Summary</div>
  <div class="clear nav_sub_menu">
    <a href="<%=Url.Content("~/Summary/Managerdaycal") %>">Manager Information</a>
    <a href="<%=Url.Content("~/Summary/Northtowerdaycal") %>">North Tower Management</a>
    <a href="<%=Url.Content("~/Summary/Welformdaycal") %>">Welform Management</a>
    <a href="<%=Url.Content("~/Summary/Appearldaycal") %>">Appreal Management</a>
    <a href="<%=Url.Content("~/Summary/BDEmployeedaycal") %>">BD Management</a>
  </div>
