<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <div class="logo">
    <div style="font-size: 20px; font-weight: bold; color:#009fff">
        Hop Lun (Bangladesh) Ltd.</div>
    <div style="font-size: 13px; font-weight: bold; color: White">
       V 2012.0.08.25
    </div>
 </div>

 <div class="logo" style="width:200px">
    <div style="font-size: 18px; font-weight: bold; color:#009fff">
        <%if (Session["ModuleName"] != null)
           { %>
            <% Response.Write(Session["ModuleName"].ToString()); %>
        <%}%>
    </div>
 </div>


<div class="current_user">
    <div style="font-size: 11px; font-weight: bold; color:Black; padding-bottom: 2px;">Developed By: Hasib, HopLun IT Department</div>
    <div style="font-size: 11px; font-weight: bold; color:Black; padding-bottom: 2px;">
        Log in Time : 
         <%if (Session["LoginDatetime"] != null)
           { %>
            <% Response.Write(Session["LoginDatetime"].ToString()); %>
        <%}%>
    </div>
    <div style="font-size: 11px; font-weight: bold; color:Black;">
        Logged in as: 
        <%if (Session["UserName"] != null)
          { %>
            <% Response.Write(Session["UserName"].ToString()); %> | [<a style="text-decoration:none;color:#009fff" href="<%: Url.Content("~/Account/Logout") %>">Logout</a>]
        <%} %>
        </div>
        <div><span id="connectedUsers"> Connected Users Info</span></div>   
           <style> #social a:hover {background-color: transparent; opacity: 0.7;} 
                   #social img { -moz-transition: all 0.8s ease-in-out; -webkit-transition: all 0.8s ease-in-out; -o-transition: all 0.8s ease-in-out; -ms-transition: all 0.8s ease-in-out; transition: all 0.8s ease-in-out; } 
                   #social img:hover { -moz-transform: rotate(360deg); -webkit-transform: rotate(360deg); -o-transform: rotate(360deg); -ms-transform: rotate(360deg); transform: rotate(360deg); } 
           </style>
           <div id="social">  <!--Start Email Rss Icon--><!--End Email Rss Icon--> <!--Start Facebook Icon--> 
           <a rel="nofollow" title="Like Our Facebook Page" href="https://www.facebook.com" target="_blank">
             <img border="0" src= "http://4.bp.blogspot.com/-dkmDM3RXcoE/UA6_d28wCyI/AAAAAAAAH8Y/9E3PI3lXueM/s1600/FACEBOOK-48x48.png" style="margin-right:1px;" alt="Icon"/>
           </a> <!--End Facebook Icon--> <!--Start Twitter Icon--> 
           <a rel="nofollow" title="Follow Our Updates On Twitter" href="https://twitter.com/" target="_blank">
             <img border="0" src="http://3.bp.blogspot.com/-TrNf8cdHE6w/UA6_iRAUK_I/AAAAAAAAH88/Jo7RAX207xo/s1600/TWITTER-48x48.png " style="margin-right:1px;" alt="Icon"/>
           </a> <!--End Twitter Icon--> <!--Start Google+ Icon--> <a title="Follow Us On Google+" rel="nofollow" href="https://plus.google.com" target="_blank">
             <img border="0" src="http://2.bp.blogspot.com/-VeOVFTKCvHw/UA6_em6-aOI/AAAAAAAAH8c/Uu4blSzFwLk/s1600/GOOGLE-PLUS-48x48.png" style="margin-right:1px;" alt="Icon"/>
           </a> <!--End Google+ Icon--><br/>
           </div>          
</div>
<script type="text/javascript">
    function PollUsers() {
        $(function () {
            $.getJSON("/Base/UserConnected", function (json) { $('#connectedUsers').text(json.count + " user(s) connected") });
        });
    }

    setInterval(PollUsers, 10000);
</script>