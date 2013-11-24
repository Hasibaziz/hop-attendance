<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Service Name
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="mp_left_menu">
    <% Html.RenderPartial("LeftMenu"); %>
</div>
<div class="mp_right_content">
    <div class="page_list_container">
        <div id="RecordsContainer"></div>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

        $('#RecordsContainer').jtable({
            paging: true,
            pageSize: 10,
            sorting: false,
            defaultSorting: 'Name ASC',
            actions: {
                listAction: '<%=Url.Content("~/Configuration/ServiceNameList") %>',
                deleteAction: '<%=Url.Content("~/Configuration/DeleteServiceName") %>',
                updateAction: '<%=Url.Content("~/Configuration/AddUpdateServiceName") %>',
                createAction: '<%=Url.Content("~/Configuration/AddUpdateServiceName") %>'
            },
            fields: {
                Id: {
                    key: true,
                    create: false,
                    edit: false,
                    list: false
                },
                Servicename: {
                    title: 'Service Name',
                    width: '40%'
                },
                Description: {
                    title: 'Description',
                    width: '50%'
                }
            }
        });
        $('#RecordsContainer').jtable('load');
    });
 
</script>

</asp:Content>
