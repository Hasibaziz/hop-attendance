<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
 <div class="page_header" style="float:left;width:99%">Employee Wise Attendance</div>
  <div class="clear nav_sub_menu">
    <a href="<%=Url.Content("~/EMPWise/ManagerWise") %>">Manager Information</a>
    <a href="<%=Url.Content("~/EMPWise/EmployeeWise") %>">North Tower Management</a>
    <a href="<%=Url.Content("~/EMPWise/WelformWise") %>">Welform Management</a>
    <a href="<%=Url.Content("~/EMPWise/ApprealWise") %>">Appreal Management</a>
    <a href="<%=Url.Content("~/EMPWise/BDEmployeewise") %>">BD Management</a>      
  </div>