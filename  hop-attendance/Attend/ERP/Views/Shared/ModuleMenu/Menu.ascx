<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="menu-bg" style="border-bottom:solid 1px gray;">
    <div id="top-menu" style="float: left; width: 100%">
        <ul class="menu">
            <%--<li><a href="<%=Url.Content("~/Settings/Index") %>" class="last"><span>Settings</span></a></li>--%>
            <li><a href="<%=Url.Content("~/Configuration/Index") %>"><span>FACTORY WISE ALL</span></a></li>
            <li><a href="<%=Url.Content("~/Deptwise/Index") %>"><span>Dept. WISE ALL</span></a></li>
            <li><a href="<%=Url.Content("~/EMPWise/Index") %>"><span>EMPLOYEE Wise ATTENDANCE</span></a></li>
            <li><a href="<%=Url.Content("~/Summary/Index") %>"><span>ATTENDANCE SUMMARY</span></a></li>
           <%--<li><a href="<%=Url.Content("~/Sales/Index") %>"><span>Sales</span></a></li>
            <li><a href="<%=Url.Content("~/Receive/Index") %>"><span>Recieve</span></a></li>
            <li><a href="<%=Url.Content("~/AssignJob/Index") %>"><span>Assign Job</span></a></li>--%>
        </ul>
    </div>
    <div class="clear">
            </div>
</div>