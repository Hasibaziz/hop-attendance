<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="page_header" style="float:left;width:99%">Dept. Wise Attendance</div>
  <div class="clear nav_sub_menu">
    <a href="<%=Url.Content("~/Deptwise/ManagerDept") %>">Manager Information</a>
    <a href="<%=Url.Content("~/Deptwise/NorthtowerDept") %>">North Tower Management</a>
    <%--<a href="<%=Url.Content("~/Deptwise/WelformDept") %>">Welform Management</a>
    <a href="<%=Url.Content("~/Deptwise/AppearlDept") %>">Appreal Management</a>
    <a href="<%=Url.Content("~/Deptwise/BDDept") %>">BD Management</a>  --%>
  </div>