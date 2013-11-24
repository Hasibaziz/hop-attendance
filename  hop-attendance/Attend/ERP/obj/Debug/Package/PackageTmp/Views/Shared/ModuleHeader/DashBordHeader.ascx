<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <div class="logo">
    <div style="font-size: 20px; font-weight: bold; color:#009fff">
        E R P</div>
    <div style="font-size: 13px; font-weight: bold; color: White">
       V 2012.0.0.1
    </div>
</div>
<div class="current_user">
    <div style="font-size: 11px; font-weight: bold; color:White; padding-bottom: 2px;">
        Log in Time : 
         <%if (Session["LoginDatetime"] != null)
           { %>
            <% Response.Write(Session["LoginDatetime"].ToString()); %>
        <%}%>
    </div>
    <div style="font-size: 11px; font-weight: bold; color:White;">
        Logged in as: 
        <%if (Session["UserName"] != null)
          { %>
            <% Response.Write(Session["UserName"].ToString()); %> | [<a style="text-decoration:none;color:#009fff" href="<%: Url.Content("~/Account/Logout") %>">Logout</a>]
        <%} %>
        </div>
</div>