<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ERP.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Service Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mp_left_menu">
        <% Html.RenderPartial("LeftMenu"); %>
    </div>
    <div class="mp_right_content">
        <div class="page_list_container">
            <div id="RecordsContainer">
            </div>
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
                    listAction: '<%=Url.Content("~/Configuration/ServiceNameDetilsList") %>',
                    deleteAction: '<%=Url.Content("~/Configuration/DeleteServiceNameDetils") %>',
                    updateAction: '<%=Url.Content("~/Configuration/AddUpdateServiceNameDetils") %>',
                    createAction: '<%=Url.Content("~/Configuration/AddUpdateServiceNameDetils") %>'
                },
                fields: {
                    Id: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    Srvicenameid: {
                        title: 'Service Name',
                        width: '15%',
                        options: '<%=Url.Content("~/Configuration/AllServiceNameListItem") %>'
                    },
                    Detailsname: {
                        title: 'Details Name',
                        width: '15%'
                    },
                    Govfee: {
                        title: 'Gov Fee',
                        width: '10%'
                    },
                    Servicefee: {
                        title: 'Service Fee',
                        width: '10%'
                    },
                    Othersfee: {
                        title: 'Others Fee',
                        width: '10%'
                    },
                    Fixedfigure: {
                        title: 'Fixed Figure',
                        width: '10%',
                        type: 'checkbox',
                        values: { 'false': 'Not Fixed', 'true': 'Fixed' },
                        defaultValue: 'true'
                    },
                    Cc: {
                        title: 'Cc',
                        width: '10%'
                    },
                    Sit: {
                        title: 'Sit',
                        width: '10%'
                    }
                }
            });
            $('#RecordsContainer').jtable('load');
        });
 
    </script>
</asp:Content>
