<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ERP.Models.LoginModel>" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="<%: Url.Content("~/Content/Common.css")%>" rel="stylesheet" type="text/css" />
    <link rel="icon" href="<%= Url.Content("~/Content/images/favicon.ico") %>"/>  
    <script type="text/javascript">
     $(function() {
         blinkeffect("txtBlink");
        })
     function blinkeffect(selector) {
         $(selector).fadeOut('slow', function() {
         $(this).fadeIn('slow', function() {
         blinkeffect(this);
       });
     });
   } 
   </script>
</head>
<body>
    <div class="header_Image">  
     <div class="Textvalue"> 
       <p style="font-size: 2em;">Welcome HopLun Bangladesh Ltd.<br /></p>
       <p>Please Enter Your User Name & Password <br /></p>
       <p>for Login Attendance System.</p>
       <div class="txtBlink" style="background:red">This Site has been Converted to Our New PayRoll System</div> 
       <%--<div class="txtBlink"><font color="red">This Site has been Converted to Our New PayRoll System</font></div> --%>
     </div> 
     
     <div class="Logbox">        
     <% using (Html.BeginForm())%> 
     <%{%>
     <div style="width: 300px; margin: 90px auto 200px auto; border: 1px solid gray; border-bottom-width: 3px; box-shadow:5px 5px gray; border-radius: 4px 4px 4px 4px; background: -moz-linear-gradient(center top , #FFFFFF 0px, #DDDDDD 100%) repeat scroll 0 0 transparent;">
        <%--<div style="background-color: #c5c5c5; height: 28px;">Login</div>--%>
        <div class="page_single_column">                
            <div class="editor-field">
             <%: Html.TextBoxFor(model => model.UserName, new { placeholder = "User Name" })%>
             <%: Html.ValidationMessageFor(model => model.UserName)%>
            </div>           
        </div>
        <div class="page_single_column">            
            <div class="editor-field">
             <%: Html.PasswordFor(model => model.Password, new { placeholder = "Password" })%>
             <input type="submit" value="Submit" title="Submit" /> 
             <%: Html.ValidationMessageFor(model => model.Password)%>
            </div>             
        </div>
       <%-- <div class="page_single_column">
            <input type="submit" value="Submit" title="Submit" />             
        </div>  --%>
        <div class="Footer_login"></div>   
      </div>         
     <%} %>   
     </div>
     </div>          
     <div class="Front_Footer">@ 2012 Hop Lun (BD) Ltd.</div>       
</body>
</html>
